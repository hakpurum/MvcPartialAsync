using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Helper.Extensions
{
    public static class MyHelpers
    {
        public enum AjaxMethods
        {
            GET,
            POST
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementId">Oluşacak div elementinin id si</param>
        /// <param name="controller">Verinin çekileceği controller adı</param>
        /// <param name="action">Verinin çekileceği action adı</param>
        /// <param name="queryString">Action a gönderilecek querystring ler</param>
        /// <param name="previewPartialName">Loading in gösterileceği partial adı</param>
        /// <param name="method">İstek tipi</param>
        /// <param name="tempData">Harici tempdata ile gönderilecek script</param>
        /// <returns></returns>
        public static MvcHtmlString AsyncPartial(this HtmlHelper helper,string elementId, string controller, string action, string queryString = "", string previewPartialName = null, AjaxMethods method = AjaxMethods.GET, TempDataDictionary tempData = null)
        {
            var xo = $"x_{elementId}";
            var js = $"function get_{elementId}()" +
                            "{" +
                                    $"var asyncElement= document.getElementById('div-{elementId}'); " +
                                    $"var asyncElementLoading= document.getElementById('div-{elementId}-loading'); " +

                                    "var " + xo + " = new XMLHttpRequest();" +
                                    xo + ".onreadystatechange = function() {" +
                                        "  if (" + xo + ".readyState == XMLHttpRequest.DONE ) {" +
                                            "  if (" + xo + ".status == 200) {" +
                                                 $"asyncElement.innerHTML = " + xo + $".responseText.replace(/data-partial-refresh/ig, 'data-partial-refresh onclick=\"get_{elementId}()\"');" +
                                                  $"asyncElementLoading.style.display='none';" +
                                                  $"asyncElement.style.display='block';" +
                                            "  }" +
                                            "else if (" + xo + ".status == 400) {" +
                                             "      alert('Error 400');" +
                                             "    }" +
                                             "else{" +
                                             "      alert('Generic error');console.log(x_test1);" +
                                             "    }" +
                                         "  }" +
                                    "};" +
                                      $"asyncElementLoading.style.display='block';" +
                                      $"asyncElement.style.display='none';" +
                                    xo + $".open('{method}', '/{controller}/{action}{queryString}', true);" +
                                    xo + ".send();" +
                             "};" +
                             $"get_{elementId}();";

            var stringPartial = helper.Partial(previewPartialName ?? "Global_preview");

            if (tempData != null)
            {
                tempData["script"] += js;
                return MvcHtmlString.Create($"<div  id='div-{elementId}'>" + stringPartial + "</div>" +
                                       $"<div  id='div-{elementId}-loading' style='display:none'>" + stringPartial + "</div>"
                                       );
            }
            else
            {
                return MvcHtmlString.Create($"<div  id='div-{elementId}'>" + stringPartial + "</div>" +
                                      $"<div  id='div-{elementId}-loading' style='display:none'>" + stringPartial + "</div>" +
                                      "<script>" + js + "</script>");
            }

        }
    }
}

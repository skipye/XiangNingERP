using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;

namespace Common
{
    public static class HtmlHelperExtension
    {
        public static string CurrentAction(this HtmlHelper html)
        {
            return (string)html.ViewContext.RouteData.Values["action"];
        }

        public static string CurrentController(this HtmlHelper html)
        {
            return (string)html.ViewContext.RouteData.Values["controller"];
        }

        public static string CurrentArea(this HtmlHelper html)
        {
            return (string)html.ViewContext.RouteData.DataTokens["area"];
        }

       
        /// <summary>
        /// 日期控件
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString DatepickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var model = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
            //20111124 15:46 蔡启明 解决日期控件选定日期后还能修改问题。
            if (model is DateTime)
            {
                return helper.TextBox(ExpressionHelper.GetExpressionText(expression),
                    ((DateTime?)model).Format(),
                    new { @ref = "datepicker", style = "width:100px;float:left;", @readonly = "false", @clase = "Datepicker" });
            }

            DateTime dt = DateTime.Now;
            if (model != null && DateTime.TryParse(model.ToString(), out dt))
            {
                return helper.TextBox(ExpressionHelper.GetExpressionText(expression),
                dt.Format(),
                new { @ref = "datepicker", style = "width:100px", @readonly = "false", @class = "Datepicker" });
            }

            return helper.TextBox(ExpressionHelper.GetExpressionText(expression),
                model,
                new { @ref = "datepicker", style = "width:100px", @readonly = "false", @class = "Datepicker" });
            //var model = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
            ////20111124 15:46 蔡启明 解决日期控件选定日期后还能修改问题。
            //if (model is DateTime)
            //{
            //    return helper.TextBox(ExpressionHelper.GetExpressionText(expression),
            //        ((DateTime?)model).Format("yyyy-MM-dd").Remove(model.ToString().IndexOf(' ')),
            //        new { @ref = "datepicker", style = "width:120px", @readonly = "true" });
            //}

            //return helper.TextBox(ExpressionHelper.GetExpressionText(expression),
            //    model.ToString().Remove(model.ToString().IndexOf(' ')),
            //    new { @ref = "datepicker", style = "width:120px", @readonly = "true" });
        }
    }
}

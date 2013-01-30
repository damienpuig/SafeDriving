using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Web.Mvc;
using Direction.Helpers.Table;

namespace Direction.Helpers
{
    public static class HtmlExtensions
    {
         public static MvcHtmlString GetDisplayName<TModel, TProperty>(
        this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TProperty>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            string value = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));
            return MvcHtmlString.Create(value);
        }

        public static Table<TModel, T> TableFor<TModel, T>(this HtmlHelper<TModel> htmlHelper, IList<T> objectData, string tableId)
        {
            return TableFor<TModel,T>(htmlHelper, objectData, tableId, string.Empty);
        }

        public static Table<TModel, T> TableFor<TModel, T>(this HtmlHelper<TModel> htmlHelper, IList<T> objectData, string tableId, string tableTitle)
        {
            Table<TModel, T> table = new Table<TModel, T>(htmlHelper, objectData, tableId, tableTitle);
            return table;
        }

        public static object AdditionalHtmlAttributes<TModel>(this HtmlHelper<TModel> htmlHelper, object additionalHtmlAttributes)
        {
            return new { AdditionalHtmlAttributes = additionalHtmlAttributes };
        }

        public static object UppercaseHtmlAttributes<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return new { uppercase = "true" };
        }

        public static object Uppercase<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return new { AdditionalHtmlAttributes = new { autocomplete = "off", uppercase = "true" } };
        }

        public static object Disabled<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return new { AdditionalHtmlAttributes = new { autocomplete = "off", disabled = "true" } };
        }
    }
}
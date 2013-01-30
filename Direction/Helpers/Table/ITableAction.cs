using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace Direction.Helpers.Table
{
    public interface ITableAction<T>
    {
        string ActionName { get; set; }
        string ActionIdentifier { get; set; }
        string HiddenInputId { get; set; }
        ActionType.Action ActionType { get; set; }
        Expression<Func<T, object>> ActionExpression { get; set; }
        Expression<Func<T, object>> IsCheckedExpression { get; set; }
        object EvaluateActionExpression(T o);
        object EvaluateIsCheckedExpression(T o);
    }
}
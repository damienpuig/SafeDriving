using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace Direction.Helpers.Table
{
    public class TableActionLink<T> : ITableAction<T>
    {
        public string ActionName { get; set; }

        public string ActionIdentifier { get; set; }

        public string HiddenInputId { get; set; }

        public Expression<Func<T, object>> ActionExpression { get; set; }
        private Func<T, object> _actionExpressionCompiled;

        public Expression<Func<T, object>> IsCheckedExpression { get; set; }
        private Func<T, object> _isCheckedExpression;

        public ActionType.Action ActionType { get; set; }

        public object EvaluateActionExpression(T o)
        {
            if (null == _actionExpressionCompiled)
            {
                _actionExpressionCompiled = ActionExpression.Compile();
            }
            return _actionExpressionCompiled(o);
        }

        public object EvaluateIsCheckedExpression(T o)
        {
            if (null == _isCheckedExpression)
            {
                _isCheckedExpression = IsCheckedExpression.Compile();
            }
            return _isCheckedExpression(o);
        }
    }
}
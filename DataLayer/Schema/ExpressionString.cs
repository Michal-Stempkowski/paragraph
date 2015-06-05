using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Logic;
using NSubstitute.Routing.AutoValues;

namespace DataLayer.Schema
{
    [BoolTranslatable]
    public class ExpressionString : BoolExpandableExpressionImpl<ExpressionString>
    {
        public string VariableName;
        public string Value;
        public OperType OperType;

        public ExpressionString()
        {
            
        }
        public ExpressionString(string variableName, string value, OperType operType)
        {
            VariableName = variableName;
            Value = value;
            OperType = operType;
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            var typedExpr = (ExpressionString)expr;

            var variable = stateManager.GetString(typedExpr.VariableName);

            switch (typedExpr.OperType)
            {
                case OperType.Equal:
                    return variable == typedExpr.Value;
                case OperType.Greater:
                    return String.Compare(variable, typedExpr.Value, StringComparison.Ordinal) > 0;
                case OperType.GreaterEqual:
                    return String.Compare(variable, typedExpr.Value, StringComparison.Ordinal) >= 0;
                case OperType.Lesser:
                    return String.Compare(variable, typedExpr.Value, StringComparison.Ordinal) < 0;
                case OperType.LesserEqual:
                    return String.Compare(variable, typedExpr.Value, StringComparison.Ordinal) <= 0;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

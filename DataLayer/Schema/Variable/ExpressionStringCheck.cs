using System;
using DataLayer.Logic;

namespace DataLayer.Schema.Variable
{
    [BoolTranslatable]
    public class ExpressionStringCheck : BoolExpandableExpressionImpl<ExpressionStringCheck>
    {
        public string VariableName;
        public string Value;
        public OperType OperType;

        public ExpressionStringCheck()
        {
            
        }
        public ExpressionStringCheck(string variableName, string value, OperType operType)
        {
            VariableName = variableName;
            Value = value;
            OperType = operType;
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            var typedExpr = (ExpressionStringCheck)expr;

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

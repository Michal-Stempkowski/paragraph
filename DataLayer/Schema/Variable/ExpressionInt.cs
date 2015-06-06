using System;
using DataLayer.Logic;

namespace DataLayer.Schema.Variable
{
    [BoolTranslatable]
    public class ExpressionInt : BoolExpandableExpressionImpl<ExpressionInt>
    {
        public string VariableName;
        public int Value;
        public OperType OperType;

        public ExpressionInt()
        {
            
        }
        public ExpressionInt(string variableName, int value, OperType operType)
        {
            VariableName = variableName;
            Value = value;
            OperType = operType;
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            var typedExpr = (ExpressionInt) expr;

            var variable = Int32.Parse(stateManager.GetString(typedExpr.VariableName));

            switch (typedExpr.OperType)
            {
                case OperType.Equal:
                    return variable == typedExpr.Value;
                case OperType.Greater:
                    return variable > typedExpr.Value;
                case OperType.GreaterEqual:
                    return variable >= typedExpr.Value;
                case OperType.Lesser:
                    return variable < typedExpr.Value;
                case OperType.LesserEqual:
                    return variable <= typedExpr.Value;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

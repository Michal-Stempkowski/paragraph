using System;
using System.Globalization;
using DataLayer.Logic;

namespace DataLayer.Schema.Variable
{
    [BoolTranslatable]
    public class ExpressionFloat : BoolExpandableExpressionImpl<ExpressionFloat>
    {
        public string VariableName;
        public float Value;
        public OperType OperType;

        public ExpressionFloat()
        {
            
        }
        public ExpressionFloat(string variableName, float value, OperType operType)
        {
            VariableName = variableName;
            Value = value;
            OperType = operType;
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            var epsilon = stateManager.GetFloatEpsilonValue();
            var typedExpr = (ExpressionFloat)expr;

            var variable = float.Parse(
                stateManager.GetString(typedExpr.VariableName), 
                CultureInfo.InvariantCulture.NumberFormat);

            switch (typedExpr.OperType)
            {
                case OperType.Equal:
                    return Math.Abs(variable - typedExpr.Value) < epsilon;
                case OperType.Greater:
                    return variable + epsilon > typedExpr.Value && 
                        Math.Abs(variable - typedExpr.Value) > epsilon;
                case OperType.GreaterEqual:
                    return variable + epsilon > typedExpr.Value;
                case OperType.Lesser:
                    return variable - epsilon < typedExpr.Value && 
                        Math.Abs(variable - typedExpr.Value) > epsilon;
                case OperType.LesserEqual:
                    return variable - epsilon < typedExpr.Value;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

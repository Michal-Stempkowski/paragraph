using System;
using System.Globalization;
using DataLayer.Logic;
using DataLayer.Schema.Validation;

namespace DataLayer.Schema.Variable
{
    [BoolTranslatable]
    public class ExpressionFloatCheck : BoolExpandableExpressionImpl<ExpressionFloatCheck>
    {
        [VariableIdentifier]
        public string VariableName { get; set; }

        public float Value;
        public CheckOperType OperType;

        public ExpressionFloatCheck()
        {
            
        }
        public ExpressionFloatCheck(string variableName, float value, CheckOperType operType)
        {
            VariableName = variableName;
            Value = value;
            OperType = operType;
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            var epsilon = stateManager.GetFloatEpsilonValue();
            var typedExpr = (ExpressionFloatCheck)expr;

            var variable = float.Parse(
                stateManager.GetString(typedExpr.VariableName), 
                CultureInfo.InvariantCulture.NumberFormat);

            switch (typedExpr.OperType)
            {
                case CheckOperType.Equal:
                    return Math.Abs(variable - typedExpr.Value) < epsilon;
                case CheckOperType.Greater:
                    return variable + epsilon > typedExpr.Value && 
                        Math.Abs(variable - typedExpr.Value) > epsilon;
                case CheckOperType.GreaterEqual:
                    return variable + epsilon > typedExpr.Value;
                case CheckOperType.Lesser:
                    return variable - epsilon < typedExpr.Value && 
                        Math.Abs(variable - typedExpr.Value) > epsilon;
                case CheckOperType.LesserEqual:
                    return variable - epsilon < typedExpr.Value;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

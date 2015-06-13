using System;
using DataLayer.Logic;
using DataLayer.Schema.Validation;

namespace DataLayer.Schema.Variable
{
    [BoolTranslatable]
    public class ExpressionIntCheck : BoolExpandableExpressionImpl<ExpressionIntCheck>
    {
        [VariableIdentifier]
        public string VariableName { get; set; }

        public int Value;
        public CheckOperType OperType;

        public ExpressionIntCheck()
        {
            
        }
        public ExpressionIntCheck(string variableName, int value, CheckOperType operType)
        {
            VariableName = variableName;
            Value = value;
            OperType = operType;
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            var typedExpr = (ExpressionIntCheck) expr;

            var variable = Int32.Parse(stateManager.GetString(typedExpr.VariableName));

            switch (typedExpr.OperType)
            {
                case CheckOperType.Equal:
                    return variable == typedExpr.Value;
                case CheckOperType.Greater:
                    return variable > typedExpr.Value;
                case CheckOperType.GreaterEqual:
                    return variable >= typedExpr.Value;
                case CheckOperType.Lesser:
                    return variable < typedExpr.Value;
                case CheckOperType.LesserEqual:
                    return variable <= typedExpr.Value;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

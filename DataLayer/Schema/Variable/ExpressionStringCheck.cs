using System;
using DataLayer.Logic;
using DataLayer.Schema.Validation;

namespace DataLayer.Schema.Variable
{
    [BoolTranslatable]
    public class ExpressionStringCheck : BoolExpandableExpressionImpl<ExpressionStringCheck>
    {
        [VariableIdentifier]
        public string VariableName
        {
            get { return SimpleArgs[0]; }
            set { SimpleArgs[0] = value; }
        }

        public string Value
        {
            get
            {
                return SimpleArgs[1];
            }
            set
            {
                SimpleArgs[1] = value;
            }
        }

        public CheckOperType OperType
        {
            get
            {
                CheckOperType operType;

                Enum.TryParse(SimpleArgs[2], out operType);

                return operType;
            }
            set
            {
                SimpleArgs[2] = value.ToString();
            }
        }

        public ExpressionStringCheck()
        {
            VariableName = "";
            Value = "";
            OperType = CheckOperType.Equal;
        }
        public ExpressionStringCheck(string variableName, string value, CheckOperType operType)
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
                case CheckOperType.Equal:
                    return variable == typedExpr.Value;
                case CheckOperType.Greater:
                    return String.Compare(variable, typedExpr.Value, StringComparison.Ordinal) > 0;
                case CheckOperType.GreaterEqual:
                    return String.Compare(variable, typedExpr.Value, StringComparison.Ordinal) >= 0;
                case CheckOperType.Lesser:
                    return String.Compare(variable, typedExpr.Value, StringComparison.Ordinal) < 0;
                case CheckOperType.LesserEqual:
                    return String.Compare(variable, typedExpr.Value, StringComparison.Ordinal) <= 0;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

using System;
using System.Globalization;
using DataLayer.Logic;
using DataLayer.Schema.Validation;

namespace DataLayer.Schema.Variable
{
    [BoolTranslatable]
    public class ExpressionIntCheck : BoolExpandableExpressionImpl<ExpressionIntCheck>
    {
        [VariableIdentifier]
        public string VariableName
        {
            get { return SimpleArgs[0]; }
            set { SimpleArgs[0] = value; }
        }

        public int Value
        {
            get
            {
                int result;

                int.TryParse(SimpleArgs[1], out result);
                return result;
            }
            set { SimpleArgs[1] = value.ToString(CultureInfo.InvariantCulture); }
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

        public ExpressionIntCheck()
        {
            VariableName = "";
            Value = 0;
            OperType = CheckOperType.Equal;
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

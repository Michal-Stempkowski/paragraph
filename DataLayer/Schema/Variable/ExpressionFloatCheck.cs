﻿using System;
using System.Globalization;
using DataLayer.Core;
using DataLayer.Logic;
using DataLayer.Schema.Validation;

namespace DataLayer.Schema.Variable
{
    [BoolTranslatable]
    public class ExpressionFloatCheck : BoolExpandableExpressionImpl<ExpressionFloatCheck>
    {
        [VariableIdentifier]
        public string VariableName
        {
            get { return SimpleArgs[0]; }
            set { SimpleArgs[0] = value; }
        }

        public float Value
        {
            get
            {
                float result;

                float.TryParse(SimpleArgs[1], out result);
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

        public ExpressionFloatCheck()
        {
            VariableName = "";
            Value = 0f;
            OperType = CheckOperType.Equal;
        }
        public ExpressionFloatCheck(string variableName, float value, CheckOperType operType)
        {
            VariableName = variableName;
            Value = value;
            OperType = operType;
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager, ICoreTranslator translator)
        {
            var epsilon = stateManager.GetFloatEpsilonValue();
            var typedExpr = expr.Convert44<ExpressionFloatCheck>();

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

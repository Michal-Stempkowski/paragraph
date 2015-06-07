using System;
using System.Globalization;
using DataLayer.Logic;

namespace DataLayer.Schema.Variable.Mutable
{
    [BoolTranslatable]
    public class ExpressionFloatModify : BoolExpandableExpressionImpl<ExpressionFloatModify>
    {
        public string VariableName;
        public ExprParam Left;
        public ExprParam Right;
        public ModifyOperType OperType;

        public ExpressionFloatModify()
        {
            
        }

        private float? _getValueFromWorldstate(string name, IStateManager stateManager)
        {
            var current = stateManager.GetString(name);

            return current != null ? 
                float.Parse(current, CultureInfo.InvariantCulture.NumberFormat) : 
                null as float?;
        }

        private float? _getValueFromConstant(string value)
        {
            return float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }

        private float? _getValue(ExprParam param, IStateManager stateManager)
        {
            switch (param.ParamSource)
            {
                case ExprParam.Source.Constant:
                    return _getValueFromConstant(param.Value);
                case ExprParam.Source.WorldState:
                    return _getValueFromWorldstate(param.Value, stateManager);
                default:
                    throw new NotImplementedException();
            }
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            var typedExpr = (ExpressionFloatModify) expr;

            var variable = typedExpr._getValueFromWorldstate(typedExpr.VariableName, stateManager);
            var left = typedExpr._getValue(typedExpr.Left, stateManager);
            var right = typedExpr._getValue(typedExpr.Right, stateManager);

            if (!(variable.HasValue && left.HasValue && right.HasValue))
            {
                return false;
            }

            switch (typedExpr.OperType)
            {
                case ModifyOperType.Add:
                    stateManager.SetString(typedExpr.VariableName, (left + right).Value
                        .ToString(CultureInfo.InvariantCulture.NumberFormat));
                    return true;
                case ModifyOperType.Subtract:
                    stateManager.SetString(typedExpr.VariableName, (left - right).Value
                        .ToString(CultureInfo.InvariantCulture.NumberFormat));
                    return true;
                case ModifyOperType.Multiply:
                    stateManager.SetString(typedExpr.VariableName, (left * right).ToString());
                    return true;
                case ModifyOperType.Divide:
                    stateManager.SetString(typedExpr.VariableName, (left / right).Value
                        .ToString(CultureInfo.InvariantCulture.NumberFormat));
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

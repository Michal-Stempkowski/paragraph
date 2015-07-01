using System;
using DataLayer.Logic;
using DataLayer.Schema.Validation;

namespace DataLayer.Schema.Variable.Mutable
{
    [BoolTranslatable]
    public class ExpressionIntModify : BoolExpandableExpressionImpl<ExpressionIntModify>
    {
        [VariableIdentifier]
        public string VariableName
        {
            get { return SimpleArgs[0]; }
            set { SimpleArgs[0] = value; }
        }

        public ExprParam Left
        {
            get
            {
                ExprParam.Source paramSource;

                Enum.TryParse(SimpleArgs[1], out paramSource);

                return new ExprParam
                {
                    ParamSource = paramSource,
                    Value = SimpleArgs[2]
                };
            }
            set
            {
                SimpleArgs[1] = value.ParamSource.ToString();
                SimpleArgs[2] = value.Value;
            }
        }

        public ExprParam Right
        {
            get
            {
                ExprParam.Source paramSource;

                Enum.TryParse(SimpleArgs[3], out paramSource);

                return new ExprParam
                {
                    ParamSource = paramSource,
                    Value = SimpleArgs[4]
                };
            }
            set
            {
                SimpleArgs[3] = value.ParamSource.ToString();
                SimpleArgs[4] = value.Value;
            }
        }

        public ModifyOperType OperType
        {
            get
            {
                ModifyOperType operType;

                Enum.TryParse(SimpleArgs[5], out operType);

                return operType;
            }
            set
            {
                SimpleArgs[5] = value.ToString();
            }
        }

        public ExpressionIntModify()
        {
            VariableName = "";
            Left = new ExprParam
            {
                ParamSource = ExprParam.Source.Constant,
                Value = "0"
            };
            Right = new ExprParam
            {
                ParamSource = ExprParam.Source.Constant,
                Value = "0"
            };
            OperType = ModifyOperType.Add;
        }

        private int? _getValueFromWorldstate(string name, IStateManager stateManager)
        {
            var current = stateManager.GetString(name);

            return current != null ? Int32.Parse(current) : null as int?;
        }

        private int? _getValueFromConstant(string value)
        {
            return Int32.Parse((value));
        }

        private int? _getValue(ExprParam param, IStateManager stateManager)
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
            var typedExpr = (ExpressionIntModify) expr;

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
                    stateManager.SetString(typedExpr.VariableName, (left + right).ToString());
                    return true;
                case ModifyOperType.Subtract:
                    stateManager.SetString(typedExpr.VariableName, (left - right).ToString());
                    return true;
                case ModifyOperType.Multiply:
                    stateManager.SetString(typedExpr.VariableName, (left * right).ToString());
                    return true;
                case ModifyOperType.Divide:
                    stateManager.SetString(typedExpr.VariableName, (left / right).ToString());
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

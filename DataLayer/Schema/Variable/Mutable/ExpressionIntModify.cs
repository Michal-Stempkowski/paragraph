using System;
using DataLayer.Logic;

namespace DataLayer.Schema.Variable.Mutable
{
    [BoolTranslatable]
    public class ExpressionIntModify : BoolExpandableExpressionImpl<ExpressionIntModify>
    {
        public string VariableName;
        public int Value;
        public ModifyOperType OperType;

        public ExpressionIntModify()
        {
            
        }
        public ExpressionIntModify(string variableName, int value, ModifyOperType operType)
        {
            VariableName = variableName;
            Value = value;
            OperType = operType;
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            var typedExpr = (ExpressionIntModify) expr;

            var current = stateManager.GetString(typedExpr.VariableName);

            if (current == null)
            {
                return false;
            }

            var variable = Int32.Parse(stateManager.GetString(typedExpr.VariableName));

            switch (typedExpr.OperType)
            {
                case ModifyOperType.Add:
                    stateManager.SetString(typedExpr.VariableName, (variable + typedExpr.Value).ToString());
                    return true;
                case ModifyOperType.Subtract:
                    stateManager.SetString(typedExpr.VariableName, (variable - typedExpr.Value).ToString());
                    return true;
                case ModifyOperType.Multiply:
                    stateManager.SetString(typedExpr.VariableName, (variable * typedExpr.Value).ToString());
                    return true;
                case ModifyOperType.Divide:
                    stateManager.SetString(typedExpr.VariableName, (variable / typedExpr.Value).ToString());
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

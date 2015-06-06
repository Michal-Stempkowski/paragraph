using System;
using System.Dynamic;
using DataLayer.Logic;

namespace DataLayer.Schema.Variable.Mutable
{
    [BoolTranslatable]
    
    public class ExpressionAssign : BoolExpandableExpressionImpl<ExpressionAssign>
    {
        public string VariableName;
        public string Value;

        public static ExpressionAssign Create<T>(string variableName, T value)
        {
            return new ExpressionAssign(variableName, value.ToString());
        }

        public ExpressionAssign()
        {
            
        }
        
        public ExpressionAssign(string variableName, string value)
        {
            VariableName = variableName;
            Value = value;
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            var typedExpr = (ExpressionAssign) expr;

            stateManager.SetString(typedExpr.VariableName, typedExpr.Value);

            return true;
        }
    }
}

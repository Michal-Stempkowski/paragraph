using System;
using System.Dynamic;
using DataLayer.Logic;
using DataLayer.Schema.Validation;

namespace DataLayer.Schema.Variable.Mutable
{
    [BoolTranslatable]
    
    public class ExpressionAssign : BoolExpandableExpressionImpl<ExpressionAssign>
    {
        [VariableIdentifier]
        public string VariableName { get; set; }
        [VariableIdentifier]
        public string Value { get; set; }

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

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Logic;
using NSubstitute.Routing.AutoValues;

namespace DataLayer.Schema
{
    [BoolTranslatable]
    public class ExpressionIntEqual : BoolExpandableExpressionImpl<ExpressionIntEqual>
    {
        public string VariableName;
        public int Value;

        public ExpressionIntEqual()
        {
            
        }
        public ExpressionIntEqual(string variableName, int value)
        {
            VariableName = variableName;
            Value = value;
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            var typedExpr = (ExpressionIntEqual) expr;

            var variable = stateManager.GetInt(typedExpr.VariableName);

            return variable != null && variable.Value == typedExpr.Value;
        }
    }
}

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
    public class ExpressionInt : BoolExpandableExpressionImpl<ExpressionInt>
    {
        public string VariableName;
        public int Value;
        public OperType OperType;

        public ExpressionInt()
        {
            
        }
        public ExpressionInt(string variableName, int value, OperType operType)
        {
            VariableName = variableName;
            Value = value;
            OperType = operType;
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            var typedExpr = (ExpressionInt) expr;

            var variable = Int32.Parse(stateManager.GetString(typedExpr.VariableName));

            switch (OperType)
            {
                case OperType.Equal:
                    return variable == typedExpr.Value;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

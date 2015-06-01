using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Logic;

namespace DataLayer.Schema
{
    [BoolTranslatable]
    public class ExpressionVariableExists : BoolExpandableExpressionImpl<ExpressionVariableExists>
    {
        public ExpressionVariableExists()
        {
            
        }

        public ExpressionVariableExists(string name)
        {
            SimpleArgs.Add(name);
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            return stateManager.HasVariable(expr.SimpleArgs[0]);
        }
    }
}

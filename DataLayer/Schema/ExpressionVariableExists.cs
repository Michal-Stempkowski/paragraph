using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Logic;

namespace DataLayer.Schema
{
    public class ExpressionVariableExists : BoolExpandableExpression
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
            return base.TranslateToBool(expr, stateManager);
        }
    }
}

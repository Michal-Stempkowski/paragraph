using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Logic;

namespace DataLayer.Schema
{
    [BoolTranslatable]
    public class ExpressionAnd : BoolExpandableExpressionImpl<ExpressionAnd>
    {
        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            return expr.Args
                .OrderBy(x => x.Key)
                .Select(x => x.Value)
                .Aggregate(true, (currentState, x) => currentState && 
                    x.TranslateToBool(x, stateManager));
        }
    }
}

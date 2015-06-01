using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Logic;

namespace DataLayer.Schema
{
    [BoolTranslatable]
    public class ExpressionTrue : BoolExpandableExpressionImpl<ExpressionTrue>
    {
        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            return true;
        }
    }
}

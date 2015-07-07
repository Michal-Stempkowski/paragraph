using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Core;
using DataLayer.Logic;

namespace DataLayer.Schema
{
    [BoolTranslatable]
    public class ExpressionNot : BoolExpandableExpressionImpl<ExpressionNot>
    {
        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager, ICoreTranslator translator)
        {
            return !expr.Args
                .OrderBy(x => x.Key)
                .Select(x => x.Value.TranslateToBool(x.Value, stateManager, translator))
                .First();
        }
    }
}

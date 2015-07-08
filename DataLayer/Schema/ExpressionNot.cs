using System.Linq;
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
                .Select(x => translator.ExpandToBool(x.Value, stateManager))
                .First();
        }
    }
}

using System.Linq;
using DataLayer.Core;
using DataLayer.Logic;

namespace DataLayer.Schema
{
    [BoolTranslatable]
    public class ExpressionOr : BoolExpandableExpressionImpl<ExpressionOr>
    {
        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager, ICoreTranslator translator)
        {
            return expr.Args
                .OrderBy(x => x.Key)
                .Select(x => x.Value)
                .Aggregate(false, (currentState, x) => currentState ||
                    translator.ExpandToBool(x, stateManager));
        }
    }
}

using DataLayer.Core;
using DataLayer.Logic;

namespace DataLayer.Schema
{
    [BoolTranslatable]
    public class ExpressionTrue : BoolExpandableExpressionImpl<ExpressionTrue>
    {
        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager, ICoreTranslator translator)
        {
            return true;
        }
    }
}

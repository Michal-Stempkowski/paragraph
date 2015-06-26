using DataLayer.Logic;

namespace DataLayer.Schema.Variable
{
    [BoolTranslatable]
    public class ExpressionVariableExists : BoolExpandableExpressionImpl<ExpressionVariableExists>
    {
        public ExpressionVariableExists()
        {
            
        }

        public ExpressionVariableExists(string name)
        {
            VariableName = name;
            //SimpleArgs.Add(name);
        }

        public string VariableName { get; set; }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            var typedExpr = expr as ExpressionVariableExists;
            return stateManager.HasVariable(typedExpr.VariableName);
            //return stateManager.HasVariable(expr.SimpleArgs[0]);
        }
    }
}

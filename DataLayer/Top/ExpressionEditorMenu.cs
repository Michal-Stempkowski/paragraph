using System.Collections.Generic;
using DataLayer.Core;
using DataLayer.Schema;

namespace DataLayer.Top
{
    public class ExpressionEditorMenu : IExpressionEditorMenu
    {
        private readonly ICoreTranslator _coreTranslator;

        public ExpressionEditorMenu(ICoreTranslator coreTranslator)
        {
            _coreTranslator = coreTranslator;
        }

        public T CreateInstance<T>(BoolExpandableExpression templ = null) where T : BoolExpandableExpression
        {
            return _coreTranslator.CreateInstance<T>(templ);
        }

        public List<string> ExpressionNames
        {
            get { return new List<string>()
            {
                "ExpressionTrue", 
                "ExpressionFalse", 
                "ExpressionOr",
                "ExpressionAnd",
                "ExpressionVariableExists",
                "ExpressionIntCheck",
                "ExpressionStringCheck",
                "ExpressionFloatCheck",
                "ExpressionNot",
                "ExpressionAssign",
                "ExpressionIntModify",
                "ExpressionFloatModify"
            };}
        }

        public BoolExpandableExpression CreateInstanceByName(string name)
        {
            return _coreTranslator.CreateInstanceByName(name);
        }
    }
}
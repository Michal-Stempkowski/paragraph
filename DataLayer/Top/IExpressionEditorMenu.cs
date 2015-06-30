using System.Collections.Generic;
using DataLayer.Schema;

namespace DataLayer.Top
{
    public interface IExpressionEditorMenu
    {
        T CreateInstance<T>(BoolExpandableExpression templ = null)
            where T : BoolExpandableExpression;

        List<string> ExpressionNames { get; }
        BoolExpandableExpression CreateInstanceByName(string name);
    }
}
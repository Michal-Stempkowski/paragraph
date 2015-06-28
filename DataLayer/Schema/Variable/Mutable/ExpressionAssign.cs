﻿using System;
using System.Dynamic;
using System.Linq;
using DataLayer.Logic;
using DataLayer.Schema.Validation;

namespace DataLayer.Schema.Variable.Mutable
{
    [BoolTranslatable]
    
    public class ExpressionAssign : BoolExpandableExpressionImpl<ExpressionAssign>
    {
        [VariableIdentifier]
        public string VariableName
        {
            get { return SimpleArgs[0]; }
            set { SimpleArgs[0] = value; }
        }

        [VariableIdentifier]
        public string Value
        {
            get { return SimpleArgs[1]; }
            set { SimpleArgs[1] = value; }
        }

        public static ExpressionAssign Create<T>(string variableName, T value)
        {
            return new ExpressionAssign(variableName, value.ToString());
        }

        public ExpressionAssign()
        {
            
        }
        
        public ExpressionAssign(string variableName, string value)
        {
            VariableName = variableName;
            Value = value;
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            var typedExpr = (ExpressionAssign) expr;

            stateManager.SetString(typedExpr.VariableName, typedExpr.Value);

            return true;
        }
    }
}

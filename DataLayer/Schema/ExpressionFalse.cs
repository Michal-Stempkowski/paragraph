﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Core;
using DataLayer.Logic;

namespace DataLayer.Schema
{
    [BoolTranslatable]
    public class ExpressionFalse : BoolExpandableExpressionImpl<ExpressionFalse>
    {
        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager, ICoreTranslator translator)
        {
            return false;
        }
    }
}

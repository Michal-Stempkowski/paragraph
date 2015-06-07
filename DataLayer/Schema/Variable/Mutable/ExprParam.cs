using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Schema.Variable.Mutable
{
    public class ExprParam
    {
        public enum Source
        {
            Constant,
            WorldState
        }

        public Source ParamSource { get; set; }
        public string Value { get; set; }
    }
}

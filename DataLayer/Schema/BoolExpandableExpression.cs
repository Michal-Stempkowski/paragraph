using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Schema
{
    public class BoolExpandableExpression : ISchema
    {
        protected BoolExpandableExpression()
        {
            Name = this.GetType().Name;
            Args = new List<BoolExpandableExpression>();
            SimpleArgs = new List<string>();
        }

        public string Name { get; set; }
        public List<BoolExpandableExpression> Args { get; set; }
        public List<string> SimpleArgs { get; set; }
    }

}

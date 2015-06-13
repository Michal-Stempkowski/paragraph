using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Schema
{
    public class DecisionSchema : ISchema
    {
        public string Description { get; set; }
        public string Destination { get; set; }
        public BoolExpandableExpression VisibilityRequirements { get; set; }
        public BoolExpandableExpression Effect { get; set; }
    }
}

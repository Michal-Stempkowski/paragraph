using System.Collections.Generic;

namespace DataLayer.Schema
{
    public class RoomSchema : ISchema
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DecisionSchema> Decisions { get; set; }
        public BoolExpandableExpression Effect { get; set; }
    }
}
using System.Collections.Generic;

namespace DataLayer.Logic
{
    public class Entity : IEntity
    {
        public string Description { get; set; }

        public List<IDecision> Decisions { get; set; }
    }
}
using System.Collections.Generic;

namespace DataLayer.Logic
{
    public interface IEntity
    {
        string Description { get; }
        List<IDecision> Decisions { get; }
    }
}
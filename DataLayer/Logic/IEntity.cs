using System.Collections.Generic;

namespace DataLayer.Logic
{
    public interface IEntity
    {
        string Description { get; }
        IList<IDecision> Decisions { get; }
    }
}
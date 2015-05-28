using System.Collections.Generic;

namespace DataLayer
{
    public interface IRoom
    {
        string Description { get; }
        IList<IDecision> Decisions { get; }
    }
}
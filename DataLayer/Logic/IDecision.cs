using System;

namespace DataLayer.Logic
{
    public interface IDecision
    {
        bool IsVisible { get; set; }
        string Description { get; set; }
        string Destination { get; set; }
        Action<IStateManager> NoEffect { get; }
        Action<IStateManager> Effect { get; set; }
    }
}
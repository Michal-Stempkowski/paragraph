using System;

namespace DataLayer
{
    public class Decision : IDecision
    {
        public Decision()
        {
            IsVisible = true;
            Description = "";
            Destination = "";
            Effect = NoEffect;
        }

        public bool IsVisible { get; set; }

        public string Description { get; set; }

        public string Destination { get; set; }

        public Action<IStateManager> NoEffect
        {
            get { return x => { }; }
        }

        public Action<IStateManager> Effect { get; set; }
    }
}
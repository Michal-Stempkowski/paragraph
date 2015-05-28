namespace DataLayer
{
    public interface IRoomDataProvider
    {
        void PerformRoomTransition(string decision, IStateManager stateManager);
        IRoom CurrentRoom { get; }
    }
}
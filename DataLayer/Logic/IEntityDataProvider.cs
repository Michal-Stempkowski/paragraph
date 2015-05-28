namespace DataLayer.Logic
{
    public interface IEntityDataProvider
    {
        bool PerformEntityTransition(string entityPath, IStateManager stateManager);
        IEntity CurrentEntity { get; }
    }
}
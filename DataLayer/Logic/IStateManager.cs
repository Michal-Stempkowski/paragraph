namespace DataLayer.Logic
{
    public interface IStateManager
    {
        bool HasVariable(string name);
        string GetString(string variableName);
    }
}
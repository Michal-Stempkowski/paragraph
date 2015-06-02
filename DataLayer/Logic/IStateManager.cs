namespace DataLayer.Logic
{
    public interface IStateManager
    {
        bool HasVariable(string name);
        int? GetInt(string variableName);
    }
}
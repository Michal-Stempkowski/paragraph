namespace DataLayer.Logic
{
    public interface IStateManager
    {
        bool HasVariable(string name);
        string GetString(string variableName);
        float GetFloatEpsilonValue();
        void SetString(string variableName, string value);
        string GetCurrentEntity();
        string SetCurrentEntity(string path);
    }
}
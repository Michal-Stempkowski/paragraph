namespace DataLayer.Top
{
    public interface IMainMenu
    {
        void CreateNewGame(string source, string destination);
        EntityMenu StartGame(string destination);
    }
}
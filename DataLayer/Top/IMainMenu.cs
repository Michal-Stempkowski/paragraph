namespace DataLayer.Top
{
    public interface IMainMenu
    {
        void CreateNewGame(string source, string destination);
        IEntityMenu StartGame(string destination);
    }
}
public interface IDataSerializer
{
    public GameData Load();
    public void Save(GameData data);
}

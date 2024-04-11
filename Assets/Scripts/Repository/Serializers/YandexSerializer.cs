using UnityEngine;

public class YandexSerializer : IDataSerializer
{
    public YandexSerializer()
    {
        YandexReceiver.OnDataLoaded.AddListener(LoadFrom);
    }

    public GameData Load()
    {
        YandexSender.Instance.LoadData();

        return null;
    }

    public void LoadFrom(string data)
    {
        try
        {
            GameData gameData = JsonUtility.FromJson<GameData>(data);

            if (gameData == null || data == null || data == "{}" || data == "")
            {
                DataContext.Instance.SetGameData(null);
                return;
            }

            if (gameData.Wallet.CoinCount == "")
            {
                DataContext.Instance.SetGameData(null);
                return;
            }

            DataContext.Instance.SetGameData(gameData);

            Debug.Log("Load data complete");
        }
        catch
        {
            Debug.Log("Load data error");

            DataContext.Instance.SetGameData(null);
        }
    }

    public void Save(GameData gameData)
    {
        try
        {
            string json = JsonUtility.ToJson(gameData, true);
            YandexSender.Instance.SaveData(json);

            //Debug.Log("Save data complete");
        }
        catch { Debug.Log("Save data error"); }
    }
}

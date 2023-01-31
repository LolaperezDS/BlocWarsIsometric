using UnityEngine;
using System.IO;
using SaveData;

public static class SaveManager
{
    private static string filepath = Application.streamingAssetsPath + "/saves/";

    public static void Save(BoardStatement data, string savename)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        using (var writer = new StreamWriter(filepath + savename))
        {
            writer.WriteLine(jsonData);
        }
    }
    public static BoardStatement Load(string savename)
    {
        string loadedData = "";
        using (var reader = new StreamReader(filepath + savename))
        {
            loadedData = reader.ReadToEnd();
        }
        if (string.IsNullOrEmpty(loadedData))
        {
            return Defaults();
        }
        return JsonUtility.FromJson<BoardStatement>(loadedData);
    }
    public static BoardStatement Defaults()
    {
        string loadedData = "";
        using (var reader = new StreamReader(filepath + "defaultData.json"))
        {
            loadedData = reader.ReadToEnd();
        }
        return JsonUtility.FromJson<BoardStatement>(loadedData);
    }
}
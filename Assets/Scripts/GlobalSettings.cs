using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public static class GlobalSettings
{
    public static string NickName = "Default_Ocherednyara";
    public static string HostIP = "localhost";
    public static int HostPort = 1337;

    public static float MusicVolume = 0.5f;
    public static float FXVolume = 0.5f;

    private static string PATH_TO_SETTINGS_FILE = Application.streamingAssetsPath + "/settings.txt";

    public static void Setup()
    {
        string settingsRaw;
        using (StreamReader sr = new StreamReader(PATH_TO_SETTINGS_FILE))
        {
            settingsRaw = sr.ReadToEnd();
        }
        List<string> splittedData = settingsRaw.Split('\n').ToList();

        // Обрезание 3 строк (см файл настроек)
        splittedData = splittedData.GetRange(3, splittedData.Count - 3);

        Debug.Log(settingsRaw);
        Debug.Log(splittedData[0]);

        // Применение
        QualitySettings.vSyncCount = Convert.ToInt32(splittedData[0].Split(';')[1]);
        Application.targetFrameRate = Convert.ToInt32(splittedData[1].Split(';')[1]);
        Screen.fullScreen = Convert.ToBoolean(Convert.ToInt32(splittedData[2].Split(';')[1]));
    }
}

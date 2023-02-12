using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveData;

public class DotOfEnter : MonoBehaviour
{
    void Start()
    {
        if (GlobalSettings.IsOnline)
        {
            Client client = new Client();
            PlayerData pd = new PlayerData(GlobalSettings.NickName);
            PlayerInitialization boardInit = client.Connect(pd);
            Wrapper.DeserializeOnlineBoardStatement(JsonUtility.FromJson<BoardStatement>(boardInit.BoardData));
            Player.Setup((PlayerInstance)boardInit.playerOrder, GlobalSettings.NickName);

        }
        else
        {
            // Load and Deserialize board info
            Wrapper.LoadSnapshot(GlobalSettings.ChosenSave);
            // Setup all players to local

            // !TODO!

            // Всё, можно играть
        }
    }
}

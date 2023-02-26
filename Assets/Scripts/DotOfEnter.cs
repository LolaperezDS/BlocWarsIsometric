using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveData;

public class DotOfEnter : MonoBehaviour
{
    private Client client;
    void Start()
    {
        GlobalSettings.Setup();
        client = GetComponent<Client>();
        string boardInit = client.Connect(GlobalSettings.NickName);
        Debug.Log(boardInit);
        Wrapper.DeserializeOnlineBoardStatement(JsonUtility.FromJson<BoardStatement>(boardInit.Split(';')[1]));
        Player.Setup((PlayerInstance)System.Convert.ToInt32(boardInit.Split(';')[0]), GlobalSettings.NickName);
    }
}

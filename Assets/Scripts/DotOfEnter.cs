using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotOfEnter : MonoBehaviour
{
    void Start()
    {
        if (GlobalSettings.IsOnline)
        {
            // Connect to server
            // Get and deserialize board info
            // Setup Managers
            // Setup Players to online
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

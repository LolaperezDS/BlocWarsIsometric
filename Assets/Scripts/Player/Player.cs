using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player
{

    private static PlayerInstance playersColor;
    public static PlayerInstance PlayersColor => playersColor;

    public static string Name;

    public static void Setup(PlayerInstance playersColor, string name)
    {
        Player.playersColor = playersColor;
        Player.Name = name;
    }
}

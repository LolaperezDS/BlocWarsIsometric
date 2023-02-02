using System;
using System.Collections.Generic;

public enum PlayerInstance
{
    Red,
    Blue,
    Green,
    Yellow,
    Empty
}

public static class PlayerInstanceMethods
{
    private static int _playerNumber = -1;

    public static List<PlayerInstance> GetPlayers()
    {
        if (_playerNumber <= 0)
            throw new Exception("Player Number does not initialized or not initialized properly");

        return (new List<PlayerInstance>(new[]
                {PlayerInstance.Red, PlayerInstance.Blue, PlayerInstance.Green, PlayerInstance.Yellow}))
            .GetRange(0, _playerNumber);
    }

    public static void Setup(int numberOfPlayers)
    {
        _playerNumber = numberOfPlayers;
    }
}
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
    public  static List<PlayerInstance> GetPlayers()
    {
        return new List<PlayerInstance> ( new [] {PlayerInstance.Red, PlayerInstance.Blue, PlayerInstance.Green, PlayerInstance.Yellow});
    }
}
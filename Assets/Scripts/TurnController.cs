using System.Collections.Generic;

public static class TurnController
{
    private static PlayerInstance _player;
    public static PlayerInstance PlayerInstance => _player;

    public static void SwitchTurn()
    {
        List<PlayerInstance> players = PlayerInstanceMethods.GetPlayers(); 
        int index = players.FindIndex((player) => player == PlayerInstance);
        _player = players[(index + 1) % players.Count];
        
        
    }

    public static void UnsafeSetTurn(PlayerInstance playerInstance)
    {
        _player = playerInstance;
    }
}
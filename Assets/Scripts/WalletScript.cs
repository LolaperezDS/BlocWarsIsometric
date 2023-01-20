using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletScript : MonoBehaviour
{
    private static List<ProduceValue> _wallets;
    public static List<ProduceValue> Wallets => _wallets;

    public static bool PossibilityToSpend(PlayerInstance playerInstance, ProduceValue cost)
    {
        ProduceValue delta;
        int playersIndex = PlayerInstanceMethods.GetPlayers().FindIndex((player) => player == playerInstance);
        
        delta = Wallets[playersIndex] - cost;
        if (delta.gold >= 0 && delta.actions >= 0) return true;
        return false;
    }
}

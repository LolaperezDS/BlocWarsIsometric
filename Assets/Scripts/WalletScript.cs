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

    public static void Spend(PlayerInstance playerInstance, ProduceValue cost)
    {
        int playersIndex = PlayerInstanceMethods.GetPlayers().FindIndex((player) => player == playerInstance);
        Wallets[playersIndex] -= cost;
    }

    public static void CollectProfit(PlayerInstance playerInstance)
    {
        int playersIndex = PlayerInstanceMethods.GetPlayers().FindIndex((player) => player == playerInstance);
        Wallets[playersIndex] += BuildingManager.GetOverallPlayerProduce(playerInstance);
    }

    public static void Setup(List<ProduceValue> newWallets)
    {
        _wallets = newWallets;
    }
}
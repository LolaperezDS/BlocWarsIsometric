using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoOut : MonoBehaviour
{
    [SerializeField] private Text text;
    private string toOut;

    void Update()
    {
        toOut = "Now turn: " + TurnController.CurrentPlayersTurn.ToString() + "\n\n";
        toOut += "Your color: " + Player.PlayersColor.ToString() + "\n";
        toOut += "Your Gold: " + WalletScript.Wallets[(int)Player.PlayersColor].gold + "\n";
        toOut += "Your Actions: " + WalletScript.Wallets[(int)Player.PlayersColor].actions;
        text.text = toOut;
    }
}

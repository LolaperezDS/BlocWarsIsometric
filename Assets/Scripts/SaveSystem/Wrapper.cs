using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveData;

public static class Wrapper
{
    public static void SaveSnapshot(string fileName) => WrapHandler(fileName);
    public static void SaveSnapshot() => WrapHandler(Random.Range(1, 100000).ToString() + ".json");

    private static void WrapHandler(string fileName)
    {
        BoardStatement boardStatement = new BoardStatement();

        // tiles
        if (TileManager.Tiles == null || TileManager.Tiles[0] == null)
        {
            Debug.LogError("Incorrect tiles topology");
            throw new System.ArgumentNullException();
        }

        boardStatement.Tiles = new TileStatement[TileManager.Tiles.Count, TileManager.Tiles[0].Count];

        for (int i = 0; i < TileManager.Tiles.Count; i++)
        {
            for (int j = 0; j < TileManager.Tiles[0].Count; j++)
            {
                boardStatement.Tiles[i, j] = TileManager.Tiles[i][j].Statement();
            }
        }

        // buildings
        if (BuildingManager.Buildings == null || BuildingManager.Buildings[0] == null)
        {
            Debug.LogError("Incorrect Buildings topology");
            throw new System.ArgumentNullException();
        }

        boardStatement.Buildings = new BuildingStatement[BuildingManager.Buildings.Count, BuildingManager.Buildings[0].Count];

        for (int i = 0; i < BuildingManager.Buildings.Count; i++)
        {
            for (int j = 0; j < BuildingManager.Buildings[0].Count; j++)
            {
                boardStatement.Buildings[i, j] = BuildingManager.Buildings[i][j].Statement();
            }
        }

        // other
        boardStatement.CurrentTurn = TurnController.CurrentPlayersTurn;
        boardStatement.PlayersAndWallets = new (PlayerInstance, ProduceValue)[PlayerInstanceMethods.GetPlayers().Count];
        for (int i = 0; i < PlayerInstanceMethods.GetPlayers().Count; i++)
        {
            boardStatement.PlayersAndWallets[i] = (PlayerInstanceMethods.GetPlayers()[i], WalletScript.Wallets[i]);
        }

        SaveManager.Save(boardStatement, fileName);
    }
}

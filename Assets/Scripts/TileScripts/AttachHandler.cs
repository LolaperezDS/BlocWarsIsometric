using UnityEngine;

public static class AttachHandler
{
    private static GameObject observer;
    private static void Setup()
    {
        observer = GameObject.FindGameObjectWithTag("Observer");
    }

    public static bool Attach(PlayerInstance player, Vector2Int id)
    {
        if (observer == null) Setup();
        if (!TileManager.CanBeAttached(player, id)) return false;
        if (!WalletScript.PossibilityToSpend(player, ProduceValue.OneAction)) return false;

        if (!TileManager.GetTileFromId(id).AttachTile(player)) return false;
        WalletScript.Spend(player, ProduceValue.OneAction);

        return true;
    }
}

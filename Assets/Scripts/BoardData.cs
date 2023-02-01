namespace SaveData
{
    [System.Serializable]
    public class BoardStatement
    {
        public (PlayerInstance, ProduceValue)[] PlayersAndWallets;

        public PlayerInstance CurrentTurn;

        public BuildingStatement[] Buildings;
        public TileStatement[,] Tiles;
    }
}


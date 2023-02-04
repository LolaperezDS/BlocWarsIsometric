namespace SaveData
{
    [System.Serializable]
    public class BoardStatement
    {
        public PlayerInstance[] Players;
        public ProduceValue[] Wallets;

        public PlayerInstance CurrentTurn;

        public BuildingStatement[] Buildings;
        public TileStatement[] Tiles;
    }
}


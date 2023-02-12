[System.Serializable]
public struct PlayerInitialization
{
    public string BoardData;
    public int playerOrder;

    public PlayerInitialization(int playerOrder, string BoardData)
    {
        this.BoardData = BoardData;
        this.playerOrder = playerOrder;
    }
}


[System.Serializable]
public struct PlayerData
{
    public string NickName;

    public PlayerData(string name)
    {
        NickName = name;
    }
}

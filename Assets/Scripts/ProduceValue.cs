[System.Serializable]
public struct ProduceValue
{
    public int gold;
    public int actions;

    public static ProduceValue operator +(ProduceValue a, ProduceValue b) =>
        new ProduceValue(a.gold + b.gold, a.actions + b.actions);

    public static ProduceValue operator +(ProduceValue a) => a;
    public static ProduceValue operator -(ProduceValue a) => new ProduceValue(-a.gold, -a.actions);

    public static ProduceValue operator -(ProduceValue a, ProduceValue b) =>
        new ProduceValue(a.gold - b.gold, a.actions - b.actions);

    public ProduceValue(int gold = 0, int actions = 0)
    {
        this.gold = gold;
        this.actions = actions;
    }

    public static ProduceValue zero => new ProduceValue(0, 0);
    public static ProduceValue OneAction => new ProduceValue(0, 1);
    public static ProduceValue OneGold => new ProduceValue(1, 0);
}
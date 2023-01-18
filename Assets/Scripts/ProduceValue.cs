using UnityEngine;

[System.Serializable]
public struct ProduceValue
{
    public int gold;
    public int actions;
    
    public static ProduceValue operator +(ProduceValue a, ProduceValue b) => new ProduceValue(a.gold + b.gold, a.actions + b.actions);
    public static ProduceValue operator +(ProduceValue a) => a;
    public static ProduceValue operator -(ProduceValue a) => new ProduceValue(-a.gold, -a.actions);
    public static ProduceValue operator -(ProduceValue a, ProduceValue b) => new ProduceValue(a.gold - b.gold, a.actions - b.actions);
    
    public ProduceValue(int gold, int actions)
    {
        this.gold = gold;
        this.actions = actions;
    }
}

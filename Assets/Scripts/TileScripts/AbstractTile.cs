using UnityEngine;

public abstract class AbstractTile : MonoBehaviour
{
    [SerializeField] protected PlayerInstance player = PlayerInstance.Empty;
    public PlayerInstance PlayerInstance => player;

    [SerializeField] protected Vector2Int id;
    public Vector2Int Id => id;
    public bool Selected { get; protected set; } = false;

    [SerializeField] protected TileType tileType;
    public TileType TileType => tileType;

    public abstract bool AttachTile(PlayerInstance playerInstance);

    public void SetUp(TileStatement tileStatement)
    {
        this.player = tileStatement.player;
        this.id = tileStatement.id;
        this.tileType = tileStatement.tileType;
    }

    public TileStatement Statement() => new TileStatement(this.Id, this.TileType, this.PlayerInstance);
}
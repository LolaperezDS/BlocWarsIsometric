public class AttachableTile : AbstractTile
{
    public override bool AttachTile(PlayerInstance playerInstance)
    {
        this.player = playerInstance;
        return true;
    }
}
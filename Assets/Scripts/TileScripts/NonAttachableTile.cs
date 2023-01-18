public class NonAttachableTile : AbstractTile
{
    public override bool AttachTile(PlayerInstance playerInstance)
    {
        return false;
    }
}
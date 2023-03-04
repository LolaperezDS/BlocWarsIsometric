using UnityEngine;

[RequireComponent(typeof(AttachAnimation))]
public class AttachableTile : AbstractTile
{
    public override bool AttachTile(PlayerInstance playerInstance)
    {
        GetComponent<AttachAnimation>().ActivateEffect();
        this.player = playerInstance;
        return true;
    }
}
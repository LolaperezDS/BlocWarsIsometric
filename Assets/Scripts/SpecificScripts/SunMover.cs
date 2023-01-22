using UnityEngine;

public class SunMover : SliderPerformer
{
    public override void Fulfill(float x)
    {
        transform.rotation = Quaternion.Euler(x * 360, -30f, 0f);
    }
}

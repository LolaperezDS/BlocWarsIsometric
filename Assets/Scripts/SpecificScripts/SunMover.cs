public class SunMover : SliderPerformer
{
    private TimesOfDay timeHandler;
    private float lastX = -1;

    private void Start()
    {
        timeHandler = GetComponent<TimesOfDay>();
    }
    public override void Fulfill(float x)
    {
        if (lastX != x) timeHandler.SetTime(x);
        lastX = x;
    }
}

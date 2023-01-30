using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DayTimes
{
    Morning,
    Noon,
    Evening,
    MidNight
}

public class TimesOfDay : MonoBehaviour
{
    [SerializeField] private DayTimes startTime;
    private DayTimes lastTime;

    [SerializeField] private float MaxSunIntensity;
    [SerializeField] private AnimationCurve ColorSunR;
    [SerializeField] private AnimationCurve ColorSunG;
    [SerializeField] private AnimationCurve ColorSunB;
    [SerializeField] private AnimationCurve ColorSunI;

    [SerializeField] private AnimationCurve ColorAmbientR;
    [SerializeField] private AnimationCurve ColorAmbientG;
    [SerializeField] private AnimationCurve ColorAmbientB;

    [SerializeField] private float MaxMoonIntensity;
    [SerializeField] private AnimationCurve ColorMoonI;
    [SerializeField] private Color ColorMoon;

    [SerializeField] private Light dayLight;
    [SerializeField] private Light nightLight;

    [Range(0.1f, 1f)]
    [SerializeField] private float CloudFactor = 1f;


    private void Start()
    {
        lastTime = startTime;
        SetTime(startTime);
    }

    private void Update()
    {
        if (lastTime != startTime) SetTime(startTime);
        lastTime = startTime;
    }
    public void SetTime(float t) => SetCurrentTime(t);
    public void SetTime(DayTimes dt) => SetCurrentTime(RangeFromDayTimes(dt));

    private void SetCurrentTime(float t)
    {
        RenderSettings.ambientLight = new Color(ColorAmbientR.Evaluate(t) * cloudFactor, ColorAmbientG.Evaluate(t) * cloudFactor, ColorAmbientB.Evaluate(t) * cloudFactor);

        dayLight.color = new Color(ColorSunR.Evaluate(t) * cloudFactor, ColorSunG.Evaluate(t) * cloudFactor, ColorSunB.Evaluate(t) * cloudFactor);

        dayLight.intensity = MaxSunIntensity * ColorSunI.Evaluate(t);

        nightLight.color = new Color(ColorMoon.r * cloudFactor, ColorMoon.g * cloudFactor, ColorMoon.b * cloudFactor);

        nightLight.intensity = MaxMoonIntensity * ColorMoonI.Evaluate(t);

        dayLight.gameObject.transform.eulerAngles = new Vector3(360 * t - 120, 50, 0);
        nightLight.gameObject.transform.eulerAngles = new Vector3(360 * t - 300, 50, 0);
    }

    private float RangeFromDayTimes(DayTimes dayTime)
    {
        switch (dayTime){
            case DayTimes.Morning:
                return 0.35f;
            case DayTimes.Noon:
                return 0.55f;
            case DayTimes.Evening:
                return 0.75f;
            case DayTimes.MidNight:
                return 0.05f;
            default:
                throw new System.Exception();
        }
    }
}

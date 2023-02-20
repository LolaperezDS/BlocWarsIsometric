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

public enum WeatherEffects
{
    Default,
    GodRays,
    Rain,
    Ashes
}

public class WeatherHandler : MonoBehaviour
{

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

    [SerializeField] private AnimationCurve SaturationCurve;

    [SerializeField] private Material SaturationMat;
    [SerializeField] private Light dayLight;
    [SerializeField] private Light nightLight;

    [Range(0.1f, 1f)]
    [SerializeField] private float cloudFactor;


    // Handle Vars
    [SerializeField] private float changeAnimationTime = 0.5f;
    [SerializeField] private DayTimes currentTime;
    public DayTimes CurrentTime => currentTime;
    private float currentContiniousTime;

    // Debug Vars
    [SerializeField] bool debugTimeBoom = false;
    [SerializeField] DayTimes targetTime;
    [SerializeField] bool debugCloudBoom = false;
    [SerializeField] float CloudFactorNew;


    private void Start()
    {
        currentContiniousTime = WeatherHandler.RangeFromDayTimes(currentTime);
    }
    private void Update()
    {
        if (debugTimeBoom)
        {
            debugTimeBoom = false;
            StartCoroutine(ContiniousChangeTime(targetTime));
        }
        if (debugCloudBoom)
        {
            debugCloudBoom = false;
            StartCoroutine(ContiniousChangeClouds(CloudFactorNew));
        }
    }

    private void SetTimeRaw(float t)
    {
        currentContiniousTime = t;

        RenderSettings.ambientLight = new Color(ColorAmbientR.Evaluate(t) * cloudFactor, ColorAmbientG.Evaluate(t) * cloudFactor, ColorAmbientB.Evaluate(t) * cloudFactor);

        dayLight.color = new Color(ColorSunR.Evaluate(t) * cloudFactor, ColorSunG.Evaluate(t) * cloudFactor, ColorSunB.Evaluate(t) * cloudFactor);

        dayLight.intensity = MaxSunIntensity * ColorSunI.Evaluate(t);

        nightLight.color = new Color(ColorMoon.r * cloudFactor, ColorMoon.g * cloudFactor, ColorMoon.b * cloudFactor);

        nightLight.intensity = MaxMoonIntensity * ColorMoonI.Evaluate(t);

        dayLight.gameObject.transform.eulerAngles = new Vector3(360 * t - 120, 50, 0);
        nightLight.gameObject.transform.eulerAngles = new Vector3(360 * t - 300, 50, 0);

        SaturationMat.SetFloat("_Saturation", SaturationCurve.Evaluate(t) * cloudFactor);
    }

    public static float RangeFromDayTimes(DayTimes dayTime)
    {
        switch (dayTime)
        {
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

    public IEnumerator ContiniousChangeTime(DayTimes dayTime)
    {
        // time
        float deltaTime = WeatherHandler.RangeFromDayTimes(dayTime) - WeatherHandler.RangeFromDayTimes(currentTime);
        if (deltaTime < 0) deltaTime += 1;
        float speedOfChange = deltaTime / changeAnimationTime;
        float flag = WeatherHandler.RangeFromDayTimes(currentTime);
        while (true)
        {
            flag += speedOfChange * Time.deltaTime;
            if (flag > 1) flag -= 1;

            SetTimeRaw(flag);

            if (flag + 0.02 >= WeatherHandler.RangeFromDayTimes(dayTime) && flag - 0.02 <= WeatherHandler.RangeFromDayTimes(dayTime))
            {
                SetTimeRaw(WeatherHandler.RangeFromDayTimes(dayTime));
                break;
            }

            yield return null;
        }
        currentTime = dayTime;
    }

    public IEnumerator ContiniousChangeClouds(float cloudsFactor)
    {
        float deltaFactor = cloudsFactor - this.cloudFactor;
        float speedOfChange = deltaFactor / (changeAnimationTime * 5);
        float flag = this.cloudFactor;
        while (true)
        {
            this.cloudFactor += speedOfChange * Time.deltaTime;

            SetTimeRaw(currentContiniousTime);

            if (speedOfChange < 0 && cloudsFactor > this.cloudFactor || speedOfChange > 0 && cloudsFactor < this.cloudFactor)
            {
                break;
            }

            yield return null;
        }
        this.cloudFactor = cloudsFactor;
        SetTimeRaw(currentContiniousTime);
    }
}

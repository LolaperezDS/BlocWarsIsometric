using UnityEngine;

public class Rain : WeatherEffectAbs
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float cloudFactorOnActivate;
    public static float cloudFactorOnDeactivate = 1f;

    public override void ActivateEffect()
    {
        StartCoroutine(GetComponent<WeatherHandler>().ContiniousChangeClouds(cloudFactorOnActivate));
        if (_particleSystem != null && !_particleSystem.isPlaying)
        {
            _particleSystem.Play();
        }
    }

    public override void DeactivateEffect()
    {
        StartCoroutine(GetComponent<WeatherHandler>().ContiniousChangeClouds(cloudFactorOnDeactivate));
        if (_particleSystem != null && _particleSystem.isPlaying)
        {
            _particleSystem.Stop();
            _particleSystem.Clear();
        }
    }
}

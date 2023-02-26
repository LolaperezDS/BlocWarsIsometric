using UnityEngine;
using System.Collections;

public class Rain : WeatherEffectAbs
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float cloudFactorOnActivate;
    public static float cloudFactorOnDeactivate = 1f;

    public override void ActivateEffect()
    {
        IsActive = true;
        Invoke(nameof(LightningBoltFactoryMethod), Random.Range(6f, 25f));

        StartCoroutine(GetComponent<WeatherHandler>().ContiniousChangeClouds(cloudFactorOnActivate));
        if (_particleSystem != null && !_particleSystem.isPlaying)
        {
            _particleSystem.Play();
        }
    }

    public override void DeactivateEffect()
    {
        IsActive = false;
        StartCoroutine(GetComponent<WeatherHandler>().ContiniousChangeClouds(cloudFactorOnDeactivate));
        if (_particleSystem != null && _particleSystem.isPlaying)
        {
            _particleSystem.Stop();
            _particleSystem.Clear();
        }
    }

    private void LightningBoltFactoryMethod()
    {
        if (IsActive)
        {
            GetComponent<LightningBoltController>().LightningBolt();
            Invoke(nameof(LightningBoltFactoryMethod), Random.Range(6f, 25f));
        }
    }
}

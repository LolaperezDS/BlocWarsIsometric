using System.Collections;
using UnityEngine;

public class LightningBoltController : MonoBehaviour
{
    [SerializeField] private Light lightningBoltLight;
    [SerializeField] private float maxIntensityLightning;
    [SerializeField] private float minIntensityLightning;
    [SerializeField] private float maxdistance;

    [Range(1.05f, 2f)]
    [SerializeField] private float speedOfFadeLight;

    [SerializeField] private GameObject lightningSoundPrefab;
    private GameObject mainCam;

    // Debug var
    [SerializeField] private bool makeBoom = false;

    private void Start()
    {
        mainCam = Camera.main.gameObject;
    }

    private void Update()
    {
        if (makeBoom)
        {
            makeBoom = false;
            LightningBolt();
        }
    }

    public void LightningBolt()
    {
        LightShoot();
        Invoke(nameof(InstanceSoundPrefab), Random.value * 2);
    }

    private void InstanceSoundPrefab()
    {
        Instantiate(lightningSoundPrefab, mainCam.transform.position + new Vector3(Random.value, 0, Random.value).normalized * Random.value * maxdistance, Quaternion.identity);
    }

    private void LightShoot()
    {
        StopCoroutine(nameof(FadeLight));

        float startIntensityLight = Mathf.Lerp(minIntensityLightning, maxIntensityLightning, Random.value);
        Vector3 rotationLightning = new Vector3(Mathf.Lerp(30, 90, Random.value), Mathf.Lerp(0, 360, Random.value), 0);

        lightningBoltLight.transform.eulerAngles = rotationLightning;
        lightningBoltLight.intensity = startIntensityLight;

        StartCoroutine(FadeLight());
    }

    private IEnumerator FadeLight()
    {
        while (lightningBoltLight.intensity > 0.1)
        {
            lightningBoltLight.intensity /= speedOfFadeLight;
            yield return null;
        }
    }
}

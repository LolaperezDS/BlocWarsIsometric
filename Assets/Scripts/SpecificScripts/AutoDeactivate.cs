using UnityEngine;

[RequireComponent(typeof(Light))]
public class AutoDeactivate : MonoBehaviour
{
    private Light light;

    private void Start() => light = GetComponent<Light>();
    void Update()
    {
        if (light.intensity <= 0.1) light.enabled = false;
        else light.enabled = true;

        if (light.enabled)
        {
            if (light.intensity <= 0.3) light.shadows = LightShadows.None;
            else light.shadows = LightShadows.Hard;
        }
    }
}

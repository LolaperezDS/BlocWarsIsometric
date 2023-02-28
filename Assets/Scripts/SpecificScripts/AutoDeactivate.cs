using UnityEngine;


public class AutoDeactivate : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<Light>().intensity <= 0.1) GetComponent<Light>().enabled = false;
        else GetComponent<Light>().enabled = true;

        if (GetComponent<Light>().enabled)
        {
            if (GetComponent<Light>().intensity <= 0.3) GetComponent<Light>().shadows = LightShadows.None;
            else GetComponent<Light>().shadows = LightShadows.Hard;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class InfoFPS : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = "FPS: " + (int)(1 / Time.deltaTime);
    }
}

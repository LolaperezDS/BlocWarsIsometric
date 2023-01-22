using UnityEngine.UI;
using UnityEngine;

public class SettingSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private SliderPerformer performer;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        performer.Fulfill(slider.value);
    }
}

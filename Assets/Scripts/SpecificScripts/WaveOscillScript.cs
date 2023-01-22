using UnityEngine;

public class WaveOscillScript : MonoBehaviour
{
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
    [SerializeField] private Vector3 direction;
    private Vector3 origin;
    private float time = 0;
    private float period;

    void Start()
    {
        origin = transform.position;
        period = 1 / frequency;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > period) time -= period;
        transform.position = origin + direction.normalized * amplitude * Mathf.Sin(2 * Mathf.PI * frequency * time);
    }
}

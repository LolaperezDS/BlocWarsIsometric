using UnityEngine;

public class SmokeEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokePrefab;
    private const float timeBeforeDeactivating = 3f;


    public void SmokeImpact()
    {
        Invoke(nameof(DoSmokeEffect), 0f);
    }

    private void DoSmokeEffect()
    {
        smokePrefab.Clear();
        smokePrefab.Play();

        Invoke(nameof(DeactivateEffect), timeBeforeDeactivating);
    }


    private void DeactivateEffect()
    {
        smokePrefab.Stop();
        smokePrefab.Clear();
    }
}

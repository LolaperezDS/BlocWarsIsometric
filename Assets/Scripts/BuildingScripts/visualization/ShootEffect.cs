using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    [SerializeField] float timeBeforeImpact;
    [SerializeField] GameObject effectPrefab;
    GameObject destination;
    
    public void FireImpact(GameObject destination)
    {
        this.destination = destination;
        Invoke(nameof(DoEffect), timeBeforeImpact);
    }

    private void DoEffect()
    {
        if (effectPrefab != null && destination != null)
        {
            Instantiate(effectPrefab, destination.transform.position, Quaternion.FromToRotation(this.transform.position, destination.transform.position));
        }
    }
}

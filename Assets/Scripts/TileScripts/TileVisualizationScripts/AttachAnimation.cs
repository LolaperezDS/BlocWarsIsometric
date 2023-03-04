using UnityEngine;

[RequireComponent(typeof(AbstractTile))]
public class AttachAnimation : MonoBehaviour
{
    [SerializeField] private GameObject attachEffectPrefab;

    public void ActivateEffect()
    {
        Instantiate(attachEffectPrefab, GetComponent<AbstractTile>().transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
    }
}

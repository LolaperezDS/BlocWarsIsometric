using UnityEngine;

public class AutoDeleteScript : MonoBehaviour
{
    [SerializeField] private float timeBeforeDeleting;
    void Start() => Invoke(nameof(AutoDelete), timeBeforeDeleting);
    private void AutoDelete() => Destroy(this.gameObject);
}

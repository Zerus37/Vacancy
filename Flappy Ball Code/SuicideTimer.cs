using UnityEngine;

public class SuicideTimer : MonoBehaviour
{
    [SerializeField] private float timer = 3f;
    void Start()
    {
        Destroy(this.gameObject, timer);
    }
}

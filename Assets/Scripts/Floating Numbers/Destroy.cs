using UnityEngine;

public class Destroy : MonoBehaviour
{
    private PoolManager poolManager;

    [SerializeField] private const float ttl = 0.5f;
    private float timer = 0;

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= ttl) {
            poolManager.Despawn(this.gameObject);
        }
    }

    public void Setup(PoolManager poolManager) {
        if (this.poolManager == null)
           this.poolManager = poolManager;

        timer = 0;
    }
}

using UnityEngine;
using Random = UnityEngine.Random;

using TMPro;

public class test : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private TextMeshProUGUI text;
    private Vector2 position;

    [SerializeField] private PoolManager poolManager;
    
    private void Start() {
        //poolManager.Preload(prefab, 10);
    }

    public void Spawn() {
        //GameObject A = Instantiate(prefab, gameObject.transform.position, transform.rotation);

        GameObject A = poolManager.Spawn(prefab, Vector2.zero, transform.rotation);

        A.GetComponent<Destroy>().Setup(poolManager);

        text = A.GetComponent<TextMeshProUGUI>();

        position = Vector2.zero;
        position.x += Random.Range(-100f, 100f);
        position.y += Random.Range(-100f, 100f);

        text.rectTransform.anchoredPosition = position;
        text.transform.SetParent(gameObject.transform, false);
    }
}

using UnityEngine;
using Random = UnityEngine.Random;

using TMPro;

public class FloatingNumbersSpawner : MonoBehaviour
{
    [SerializeField] private GameObject numberPrefab;
    private TextMeshProUGUI text;
    private Vector2 position;
    private string textToDisplay;

    [SerializeField] private PoolManager poolManager;
    
    private void Start() {
        //poolManager.Preload(prefab, 10);//! ?
        AddClickerControllerEvents();
    }

    public void Spawn() {
        GameObject obj = poolManager.Spawn(numberPrefab, Vector2.zero, transform.rotation);

        obj.GetComponent<Destroy>().Setup(poolManager);

        text = obj.GetComponent<TextMeshProUGUI>();
        
        text.SetText(textToDisplay);

        position = Vector2.zero;
        position.x += Random.Range(-100f, 100f); //!
        position.y += Random.Range(-100f, 100f);

        text.rectTransform.anchoredPosition = position;
        text.transform.SetParent(gameObject.transform, false);
    }

    private void OnRefreshCurrentStats_Clicker(uint clickPower, uint PPS, float bonusProbability) {
        textToDisplay = clickPower.ToString();
    }

    #region Event subscriptions
    private void AddClickerControllerEvents() {
        ClickerController.OnRefreshCurrentStats += OnRefreshCurrentStats_Clicker;
    }
    #endregion
}

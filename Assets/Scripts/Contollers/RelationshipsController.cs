using UnityEngine;
using UnityEngine.Events;

public class RelationshipsController : MonoBehaviour
{
    [SerializeField] private ClickerController clickerController;
    
    private int relationshipsIndex = 0;
    private int placeIndex = 0;
    
    private uint upgradePricePlace = 50;
    private uint upgradePriceRelationships = 50;
    
    public delegate void OnRefreshPricesDelegate(uint b, uint c);
	public static event OnRefreshPricesDelegate OnRefreshPrices;

    public UnityEvent OnBuy_Place;
    public UnityEvent OnBuy_Relationships;

    public delegate void OnUpgradeDelegate(int a, int b);
	public static event OnUpgradeDelegate OnUpgrade;

    private void Start() {
        RefreshUIPrices();
    }

    #region Button methods
    public void BuyPlace() {
        if (clickerController.Buy(upgradePricePlace)) {
            placeIndex++;

            upgradePricePlace *= 1;

            OnBuy_Place?.Invoke();
            if (OnUpgrade != null) OnUpgrade(relationshipsIndex, placeIndex);
            RefreshUIPrices();
        }
        else {
            Debug.Log("-");
        }
    }

    public void BuyRelationships() {
        if (clickerController.Buy(upgradePriceRelationships)) {
            relationshipsIndex++;

            upgradePriceRelationships *= 1;

            OnBuy_Relationships?.Invoke();
            if (OnUpgrade != null) OnUpgrade(relationshipsIndex, placeIndex);
            RefreshUIPrices();
        }
        else {
            Debug.Log("=");
        }
    }
    #endregion

    #region UI methods
    private void RefreshUIPrices() {
        if (OnRefreshPrices != null) OnRefreshPrices(upgradePricePlace, upgradePriceRelationships);
    }
    #endregion
}

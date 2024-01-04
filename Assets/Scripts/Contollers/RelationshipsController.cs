using System;
using UnityEngine;
using UnityEngine.Events;

public class RelationshipsController : MonoBehaviour
{
    [SerializeField] private ClickerController clickerController;
    
    private int relationshipsIndex = 0;
    private int placeIndex = 0;
    
    private uint upgradePricePlace = 2;
    private uint upgradePriceRelationships = 3;

    [SerializeField] private TransitionStatesSO transitionStates;
    [SerializeField] private PlacesSO places;
    [SerializeField] private StorySO relationshipSpeeches;

    public delegate void OnRefreshPricesDelegate(uint placePrice, uint relationshipsPrice);
	public static event OnRefreshPricesDelegate OnRefreshPrices;

    public UnityEvent OnBuy_Place;
    public UnityEvent OnBuy_Relationships;

    public delegate void OnUpgradeDelegate(int relationshipsIndex, int placeIndex);
	public static event OnUpgradeDelegate OnUpgrade;

    public delegate void OnUpgradeDelegate_Place(int placeIndex);
	public static event OnUpgradeDelegate_Place OnUpgrade_Place;

    public delegate void OnUpgradeDelegate_Relationship(int relationshipsIndex);
	public static event OnUpgradeDelegate_Relationship OnUpgrade_Relationship;

    public delegate void OnFailDelegate_Place(SpeechSO failSpeech);
	public static event OnFailDelegate_Place OnFail_Place;

    public delegate void OnFailDelegate_Relationship(SpeechSO failSpeech);
	public static event OnFailDelegate_Relationship OnFail_Relationship;

    private void Start() {
        RefreshUIPrices();
    }

    #region Button methods
    public void BuyPlace() {
        if(isEndOfPlaces()) return;

        foreach (TransitionCondition_Place condition in transitionStates.transitionsPlace) {
            if (placeIndex == condition.placeIndex) {
                if (relationshipsIndex < condition.minCharacterIndex) {
                    if (OnFail_Place != null) OnFail_Place(condition.fail);
                    return;
                }
            }
        }

        if (clickerController.Buy(upgradePricePlace)) {
            placeIndex++;

            upgradePricePlace *= 1; //!

            OnBuy_Place?.Invoke();
            if (OnUpgrade_Place != null) OnUpgrade_Place(placeIndex);
            if (OnUpgrade != null) OnUpgrade(relationshipsIndex, placeIndex);
            RefreshUIPrices();
        }
    }

    public void BuyRelationships() {
        if(isEndOfRelationships()) return;

        foreach (TransitionCondition_Character condition in transitionStates.transitionsCharacter) {
            if (relationshipsIndex == condition.characterIndex) {
                if (placeIndex < condition.minPlaceIndex) {
                    if (OnFail_Relationship != null) OnFail_Relationship(condition.fail);
                    return;
                }
            }
        }

        if (clickerController.Buy(upgradePriceRelationships)) {
            relationshipsIndex++;

            upgradePriceRelationships *= 1; //!

            OnBuy_Relationships?.Invoke();
            if (OnUpgrade_Relationship != null) OnUpgrade_Relationship(relationshipsIndex);
            if (OnUpgrade != null) OnUpgrade(relationshipsIndex, placeIndex);
            RefreshUIPrices();
        }
    }
    #endregion

    #region End of updates methods
    private bool isEndOfPlaces() {
        if (placeIndex == places.sprites.Count - 1) return true;
        return false;
    }

    private bool isEndOfRelationships() {
        if (relationshipsIndex == relationshipSpeeches.speeches.Count - 1) return true;
        return false;
    }
    #endregion

    #region UI methods
    private void RefreshUIPrices() {
        if (OnRefreshPrices != null) OnRefreshPrices(upgradePricePlace, upgradePriceRelationships);
    }
    #endregion
}
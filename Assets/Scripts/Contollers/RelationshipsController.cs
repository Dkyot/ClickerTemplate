using System;
using UnityEngine;
using UnityEngine.Events;

public class RelationshipsController : MonoBehaviour
{
    [SerializeField] private ClickerController clickerController;
    
    private int relationshipsIndex = 0;
    private int placeIndex = 0;
    
    private uint upgradePricePlace = 30;
    private uint upgradePriceRelationships = 30;

    [SerializeField] private TransitionStatesSO transitionStates;
    
    [SerializeField] private RelationshipUpgradeMessagesSO relationshipUpgradeMessages;


    [SerializeField] private PlacesSO places;
    [SerializeField] private CharacterSO characters;



    public delegate void OnRefreshPricesDelegate(uint placePrice, uint relationshipsPrice);
	public static event OnRefreshPricesDelegate OnRefreshPrices;

    public UnityEvent OnBuy_Place;
    public UnityEvent OnBuy_Relationships;

    public delegate void OnUpgradeDelegate(int relationshipsIndex, int placeIndex);
	public static event OnUpgradeDelegate OnUpgrade;

    public delegate void OnFailDelegate(String failMessage);
	public static event OnFailDelegate OnFailBuy;

    public delegate void OnUpgrade_RelationshipDelegate(String relationshipMessage);
	public static event OnUpgrade_RelationshipDelegate OnUpgrade_Relationship;

    private void Start() {
        RefreshUIPrices();
    }

    #region Button methods
    public void BuyPlace() {
        if (placeIndex == places.sprites.Count - 1) return;

        //! проверка условия
        if (transitionStates != null) {
            foreach (TransitionCondition_Place condition in transitionStates.transitionsPlace) {
                if (placeIndex == condition.placeIndex) { // если у данного места есть условие на уровень персонажа
                    if (relationshipsIndex < condition.minCharacterIndex) { // если уровень не соответствует
                        //Debug.Log(condition.failMessage.message);
                        if (OnFailBuy != null) OnFailBuy(condition.failMessage.message);
                        return;
                    }
                }
            }
        }

        if (clickerController.Buy(upgradePricePlace)) {
            placeIndex++;

            upgradePricePlace *= 2;

            OnBuy_Place?.Invoke();
            if (OnUpgrade != null) OnUpgrade(relationshipsIndex, placeIndex);
            RefreshUIPrices();
        }
        else {
            Debug.Log("-");
        }
    }

    public void BuyRelationships() {
        if (relationshipsIndex == characters.sprites.Count - 1) return;

        //! проверка условия
        if (transitionStates != null) {
            foreach (TransitionCondition_Character condition in transitionStates.transitionsCharacter) {
                if (relationshipsIndex == condition.characterIndex) { // если у данного персонажа есть условие на место
                    if (placeIndex < condition.minPlaceIndex) { // если место не соответствует
                        //Debug.Log(condition.failMessage.message);
                        if (OnFailBuy != null) OnFailBuy(condition.failMessage.message);
                        return;
                    }
                }
            }
        }

        if (clickerController.Buy(upgradePriceRelationships)) {
            relationshipsIndex++;

            upgradePriceRelationships *= 2;

            OnBuy_Relationships?.Invoke();
            if (OnUpgrade != null) OnUpgrade(relationshipsIndex, placeIndex);

            if (OnUpgrade_Relationship != null) OnUpgrade_Relationship(relationshipUpgradeMessages.upgradeMessages[relationshipsIndex - 1].message);
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

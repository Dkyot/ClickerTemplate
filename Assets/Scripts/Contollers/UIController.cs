using UnityEngine;

using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Score text")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Clicker prices text")]
    [SerializeField] private TextMeshProUGUI clickPowerPriceText;
    [SerializeField] private TextMeshProUGUI PPSPriceText;
    [SerializeField] private TextMeshProUGUI BonusProbabilityPriceText;

    [Header("Clicker stats text")]
    [SerializeField] private TextMeshProUGUI clickPowerStatsText;
    [SerializeField] private TextMeshProUGUI PPSStatsText;
    [SerializeField] private TextMeshProUGUI BonusProbabilityStatsText;

    [Header("Relationships prices text")]
    [SerializeField] private TextMeshProUGUI placePriceText;
    [SerializeField] private TextMeshProUGUI relationshipsPriceText;

    private void Awake() {
        AddClickerControllerEvents();
        AddRelationshipsControllerEvents();
    }

    #region Events methods
    private void OnClick() {

    }

    private void OnBonusTrigger() {

    }

    private void OnCooldownReset() {

    }

    private void OnRefreshScore(ulong a) {
        scoreText.text = a.ToString();
    }

    private void OnRefreshPrices_Clicker(uint clickPower, uint PPS, uint bonusProbability) {
        clickPowerPriceText.text = clickPower.ToString();
        PPSPriceText.text = PPS.ToString();
        BonusProbabilityPriceText.text = bonusProbability.ToString();
    }

    private void OnRefreshCurrentStats_Clicker(uint clickPower, uint PPS, uint bonusProbability) {
        clickPowerStatsText.text = clickPower.ToString();
        PPSStatsText.text = PPS.ToString();
        BonusProbabilityStatsText.text = bonusProbability.ToString() + "%";
    }

    private void OnRefreshPrices_Relationships(uint placePrice, uint relationshipsPrice) {
        placePriceText.text = placePrice.ToString();
        relationshipsPriceText.text = relationshipsPrice.ToString();
    }

    private void OnIncreaseScore() {

    }

    private void OnDecreaseScore() {

    }

    private void OnBuy_ClickPower() {

    }

    private void OnBuy_PPS() {

    }

    private void OnBuy_BonusProbability() {

    }

    private void OnUnsuccessfulBuy() {

    }
    #endregion

    #region Event subscriptions
    private void AddClickerControllerEvents() {
        ClickerController.OnRefreshScore += OnRefreshScore;
        ClickerController.OnRefreshPrices += OnRefreshPrices_Clicker;
        ClickerController.OnRefreshCurrentStats += OnRefreshCurrentStats_Clicker;
    }

    private void AddRelationshipsControllerEvents() {
        RelationshipsController.OnRefreshPrices += OnRefreshPrices_Relationships;
    }
    #endregion
}

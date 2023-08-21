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

    private void OnRefreshPrices_Clicker(uint b, uint c, uint d) {
        clickPowerPriceText.text = b.ToString();
        PPSPriceText.text = c.ToString();
        BonusProbabilityPriceText.text = d.ToString();
    }

    private void OnRefreshCurrentStats_Clicker(uint b, uint c, float d) {
        clickPowerStatsText.text = b.ToString();
        PPSStatsText.text = c.ToString();
        BonusProbabilityStatsText.text = d.ToString();
    }

    private void OnRefreshPrices_Relationships(uint b, uint c) {
        placePriceText.text = b.ToString();
        relationshipsPriceText.text = c.ToString();
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

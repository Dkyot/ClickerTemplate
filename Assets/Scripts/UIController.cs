using UnityEngine;

using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI clickPowerPriceText;
    [SerializeField] private TextMeshProUGUI PPSPriceText;
    [SerializeField] private TextMeshProUGUI BonusProbabilityPriceText;

    private void Awake() {
        ClickerController.OnRefreshScore += OnRefreshScore;
    }

    #region Events methods
    private void OnClick() {

    }

    private void OnBonusTrigger() {

    }

    private void OnCooldownReset() {

    }

    private void OnRefreshScore(ulong a, uint b, uint c, uint d) {
        scoreText.text = a.ToString();
        clickPowerPriceText.text = b.ToString();
        PPSPriceText.text = c.ToString();
        BonusProbabilityPriceText.text = d.ToString();
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
}

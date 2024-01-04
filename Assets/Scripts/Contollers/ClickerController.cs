using UnityEngine;

using Random = UnityEngine.Random;
using UnityEngine.Events;

public class ClickerController : MonoBehaviour
{
    private ulong score = 0;
    private uint clickPower = 1;
    private uint PPS = 0;
    private uint bonusProbability = 0;

    private uint upgradePricePPS = 2; //!
    private uint upgradePriceClickPower = 3;
    private uint upgradePriceBonusProbability = 30;

    private float timer = 0;
    private const float cooldown = 1;

    [SerializeField] private TemporaryBonusesController bonusesController;

    public UnityEvent OnClick;
    public UnityEvent OnBonusTrigger;

    public UnityEvent OnCooldownReset;

    public delegate void OnRefreshScoreDelegate(ulong score);
	public static event OnRefreshScoreDelegate OnRefreshScore;

    public delegate void OnRefreshPricesDelegate(uint clickPower, uint PPS, uint bonusProbability);
	public static event OnRefreshPricesDelegate OnRefreshPrices;

    public delegate void OnRefreshCurrentStatsDelegate(ulong clickPower, uint PPS, uint bonusProbability);
	public static event OnRefreshCurrentStatsDelegate OnRefreshCurrentStats;

    public UnityEvent OnIncreaseScore;
    public UnityEvent OnDecreaseScore;
    
    public UnityEvent OnBuy_ClickPower;
    public UnityEvent OnBuy_PPS;
    public UnityEvent OnBuy_BonusProbability;
    public UnityEvent OnUnsuccessfulBuy;

    private void Start() {
        AddBonusEvents();

        RefreshUIScore();
        RefreshUIPrices();
    }

    private void Update() {
        CooldownUpdate();
    }

    public bool Buy(uint cost) {
        if (score >= cost) {
            score -= cost;

            OnDecreaseScore?.Invoke();
            RefreshUIScore();

            return true;
        }
        OnUnsuccessfulBuy?.Invoke();
        return false;
    }

    #region Button methods
    public void Click() {
        score += clickPower * bonusesController.GetClickPowerBonus();

        OnClick?.Invoke();
        OnIncreaseScore?.Invoke();

        RandomBonus();
        RefreshUIScore();
    }

    public void BuyClickPower() {
        if (score >= upgradePriceClickPower) {
            score -= upgradePriceClickPower;

            OnDecreaseScore?.Invoke();

            IncreaseClickPower();

            upgradePriceClickPower *= 2; //!

            RefreshUIPrices();
            RefreshUIScore();
        }
        OnUnsuccessfulBuy?.Invoke();
    }

    public void BuyPPS() {
        if (score >= upgradePricePPS) {
            score -= upgradePricePPS;

            OnDecreaseScore?.Invoke();

            IncreasePPS();

            upgradePricePPS *= 3; //!

            RefreshUIPrices();
            RefreshUIScore();
        }
        OnUnsuccessfulBuy?.Invoke();
    }

    public void BuyBonusProbability() {
        if (score >= upgradePriceBonusProbability && bonusProbability < 100) {
            score -= upgradePriceBonusProbability;

            OnDecreaseScore?.Invoke();

            IncreaseBonusProbability();

            upgradePriceBonusProbability *= 1; //!

            RefreshUIPrices();
            RefreshUIScore();
        }
        OnUnsuccessfulBuy?.Invoke();
    }
    #endregion
    
    #region Auxiliary methods
    private void CooldownUpdate() {
        timer += Time.deltaTime;
        if (timer >= cooldown) {
            score += PPS * bonusesController.GetPPSBonus();

            OnCooldownReset?.Invoke();

            RefreshUIScore();
            timer = 0;
        }
    }

    private void RandomBonus() {
        if (Random.Range(0f, 1f) <= bonusProbability) {
            score += clickPower * 50; //!

            OnBonusTrigger?.Invoke();
            OnIncreaseScore?.Invoke();
        }
    }

    private void IncreaseClickPower() {
        clickPower++;
        OnBuy_ClickPower?.Invoke();
    }

    private void IncreasePPS() {
        PPS++;
        OnBuy_PPS?.Invoke();
    }

    private void IncreaseBonusProbability() {
        bonusProbability += 1;
        OnBuy_BonusProbability?.Invoke();
    }
    #endregion

    #region UI methods
    private void RefreshUIScore() {
        if (OnRefreshScore != null) OnRefreshScore(score);
    }

    private void RefreshUIPrices() {
        if (OnRefreshPrices != null) OnRefreshPrices(upgradePriceClickPower, upgradePricePPS, upgradePriceBonusProbability);
        if (OnRefreshCurrentStats != null) OnRefreshCurrentStats(
            clickPower * bonusesController.GetClickPowerBonus(), 
            PPS * bonusesController.GetPPSBonus(), 
            bonusProbability);
    }
    #endregion

    #region Event subscriptions
    private void AddBonusEvents() {
        TemporaryBonusesController.OnEndBonus += RefreshUIPrices;
        TemporaryBonusesController.OnStartBonus += RefreshUIPrices;
    }
    #endregion
}

using UnityEngine;

using Random = UnityEngine.Random;
using UnityEngine.Events;

public class ClickerController : MonoBehaviour
{
    private ulong score = 0;
    private uint clickPower = 1;
    private uint PPS = 0;
    private float bonusProbability = 0.01f;

    private uint upgradePricePPS = 2;
    private uint upgradePriceClickPower = 3;
    private uint upgradePriceBonusProbability = 30;

    private float timer = 0;
    private float cooldown = 1;

    public UnityEvent OnClick;
    public UnityEvent OnBonusTrigger;

    public UnityEvent OnCooldownReset;

    public delegate void OnRefreshScoreDelegate(ulong a);
	public static event OnRefreshScoreDelegate OnRefreshScore;

    public delegate void OnRefreshPricesDelegate(uint b, uint c, uint d);
	public static event OnRefreshPricesDelegate OnRefreshPrices;

    public delegate void OnRefreshCurrentStatsDelegate(uint b, uint c, float d);
	public static event OnRefreshCurrentStatsDelegate OnRefreshCurrentStats;

    public UnityEvent OnIncreaseScore;
    public UnityEvent OnDecreaseScore;
    
    public UnityEvent OnBuy_ClickPower;
    public UnityEvent OnBuy_PPS;
    public UnityEvent OnBuy_BonusProbability;
    public UnityEvent OnUnsuccessfulBuy;

    private void Start() {
        RefreshUIScore();
        RefreshUIPrices();
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= cooldown) {
            score += PPS;

            OnCooldownReset?.Invoke();

            RefreshUIScore();
            timer = 0;
        }
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
        score += clickPower;

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

            upgradePriceClickPower *= 2;

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

            upgradePricePPS *= 3;

            RefreshUIPrices();
            RefreshUIScore();
        }
        OnUnsuccessfulBuy?.Invoke();
    }

    public void BuyBonusProbability() {
        if (score >= upgradePriceBonusProbability) {
            score -= upgradePriceBonusProbability;

            OnDecreaseScore?.Invoke();

            IncreaseBonusProbability();

            upgradePriceBonusProbability *= 2;

            RefreshUIPrices();
            RefreshUIScore();
        }
        OnUnsuccessfulBuy?.Invoke();
    }
    #endregion
    
    #region Auxiliary methods
    private void RandomBonus() {
        if (Random.Range(0f, 1f) <= bonusProbability) {
            score += 500;

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
        bonusProbability += 0.01f;

        OnBuy_BonusProbability?.Invoke();
    }
    #endregion

    #region UI methods
    private void RefreshUIScore() {
        if (OnRefreshScore != null) OnRefreshScore(score);
    }

    private void RefreshUIPrices() {
        if (OnRefreshPrices != null) OnRefreshPrices(upgradePriceClickPower, upgradePricePPS, upgradePriceBonusProbability);
        if (OnRefreshCurrentStats != null) OnRefreshCurrentStats(clickPower, PPS, bonusProbability);
    }
    #endregion
}

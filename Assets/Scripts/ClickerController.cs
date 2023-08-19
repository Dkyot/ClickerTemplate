using UnityEngine;

using Random = UnityEngine.Random;
using UnityEngine.Events;

public class ClickerController : MonoBehaviour
{
    private ulong score = 0;
    private uint clickPower = 1;
    private uint PPS = 0;
    private float bonusProbability = 0.01f;

    private uint upgradePricePPS = 4;
    private uint upgradePriceClickPower = 5;
    private uint upgradePriceBonusProbability = 20;

    private float timer = 0;
    private float cooldown = 1;



    public UnityEvent OnClick;
    public UnityEvent OnBonusTrigger;

    public UnityEvent OnCooldownReset;

    public delegate void OnRefreshScoreDelegate(ulong a, uint b, uint c, uint d);
	public static event OnRefreshScoreDelegate OnRefreshScore;

    public UnityEvent OnIncreaseScore;
    public UnityEvent OnDecreaseScore;
    
    public UnityEvent OnBuy_ClickPower;
    public UnityEvent OnBuy_PPS;
    public UnityEvent OnBuy_BonusProbability;
    public UnityEvent OnUnsuccessfulBuy;



    private void Start() {
        RefreshUI();
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= cooldown) {
            score += PPS;

            OnCooldownReset?.Invoke();

            RefreshUI();
            timer = 0;
        }
    }

    #region Button methods
    public void Click() {
        score += clickPower;

        OnClick?.Invoke();
        OnIncreaseScore?.Invoke();

        RandomBonus();
        RefreshUI();
    }

    public void BuyClickPower() {
        if (score >= upgradePriceClickPower) {
            score -= upgradePriceClickPower;

            OnDecreaseScore?.Invoke();

            IncreaseClickPower();

            upgradePriceClickPower *= 2;

            RefreshUI();
        }
        OnUnsuccessfulBuy?.Invoke();
    }

    public void BuyPPS() {
        if (score >= upgradePricePPS) {
            score -= upgradePricePPS;

            OnDecreaseScore?.Invoke();

            IncreasePPS();

            upgradePricePPS *= 3;

            RefreshUI();
        }
        OnUnsuccessfulBuy?.Invoke();
    }

    public void BuyBonusProbability() {
        if (score >= upgradePriceBonusProbability) {
            score -= upgradePriceBonusProbability;

            OnDecreaseScore?.Invoke();

            IncreaseBonusProbability();

            upgradePriceBonusProbability *= 2;

            RefreshUI();
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

    private void RefreshUI() {
        OnRefreshScore(score, upgradePriceClickPower, upgradePricePPS, upgradePriceBonusProbability);
    }
    #endregion
}

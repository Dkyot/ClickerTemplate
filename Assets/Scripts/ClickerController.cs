using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using TMPro;
using Random = UnityEngine.Random;

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

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI clickPowerPriceText;
    [SerializeField] private TextMeshProUGUI PPSPriceText;
    [SerializeField] private TextMeshProUGUI BonusProbabilityPriceText;

    private void Start() {
        RefreshUI();
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= cooldown) {
            score += PPS;
            RefreshUI();
            timer = 0;
        }
    }

    public void Click() {
        score += clickPower;
        RandomBonus();
        RefreshUI();
    }

    private void RandomBonus() {
        if (Random.Range(0f, 1f) <= bonusProbability)
            score += 500;
    }

    public void BuyClickPower() {
        if (score >= upgradePriceClickPower) {
            score -= upgradePriceClickPower;
            IncreaseClickPower();

            upgradePriceClickPower *= 2;

            RefreshUI();
        }
    }

    public void BuyPPS() {
        if (score >= upgradePricePPS) {
            score -= upgradePricePPS;
            IncreasePPS();

            upgradePricePPS *= 3;

            RefreshUI();
        }
    }

    public void BuyBonusProbability() {
        if (score >= upgradePriceBonusProbability) {
            score -= upgradePriceBonusProbability;
            IncreaseBonusProbability();

            upgradePriceBonusProbability *= 2;

            RefreshUI();
        }
    }

    private void IncreaseClickPower() {
        clickPower++;
    }

    private void IncreasePPS() {
        PPS++;
    }

    private void IncreaseBonusProbability() {
        bonusProbability += 0.01f;
    }

    private void RefreshUI() {
        scoreText.text = score.ToString();
        clickPowerPriceText.text = upgradePriceClickPower.ToString();
        PPSPriceText.text = upgradePricePPS.ToString();
        BonusProbabilityPriceText.text = upgradePriceBonusProbability.ToString();
    }
}

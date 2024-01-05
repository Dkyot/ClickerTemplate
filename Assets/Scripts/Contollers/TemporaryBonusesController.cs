using UnityEngine;

public class TemporaryBonusesController : MonoBehaviour
{
    [SerializeField] private ulong clickPowerMultiplier = 10;
    [SerializeField] private bool clickPowerBonusIsActive = false;

    [SerializeField] private uint PPSMultiplier = 10;
    [SerializeField] private bool PPSBonusIsActive = false;

    // adv speech

    public delegate void OnStartBonusDelegate();
	public static event OnStartBonusDelegate OnStartBonus;

    public delegate void OnEndBonusDelegate();
	public static event OnEndBonusDelegate OnEndBonus;

    public ulong GetClickPowerBonus() {
        return clickPowerBonusIsActive ? clickPowerMultiplier : 1;
    }

    public uint GetPPSBonus() {
        return PPSBonusIsActive ? PPSMultiplier : 1;
    }

    public void StartClickPowerBonus() {
        clickPowerBonusIsActive = true;
        if (OnStartBonus != null) OnStartBonus();
    }

    public void StartPPSBonus() {
        PPSBonusIsActive = true;
        if (OnStartBonus != null) OnStartBonus();
    }
    
    public void EndClickPowerBonus() {
        clickPowerBonusIsActive = false;
        if (OnEndBonus != null) OnEndBonus();
    }

    public void EndPPSBonus() {
        PPSBonusIsActive = false;
        if (OnEndBonus != null) OnEndBonus();
    }
}
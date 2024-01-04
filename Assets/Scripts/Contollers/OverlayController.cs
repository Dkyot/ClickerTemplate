using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class OverlayController : MonoBehaviour
{
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject mainUI;

    [SerializeField] private StorySO speeches;
    [SerializeField] private StorySO relationshipSpeeches;
    
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI massege;

    private bool finalSpeechShown = false;

    public delegate void OnCloseOverlayDelegate();
	public static event OnCloseOverlayDelegate OnCloseOverlay;

    public delegate void OnCloseEndSpeechDelegate();
	public static event OnCloseEndSpeechDelegate OnCloseEndSpeech;

    private int currentMessegeIndex;
    private SpeechSO currentSpeech;

    private void Awake() {
        AddRelationshipsControllerEvents();

        characterImage = characterImage.GetComponent<Image>();

        SetStartSpeech();
    }

    #region Set message methods
    private void SetStartSpeech() {
        currentMessegeIndex = 0;
        currentSpeech = speeches.speeches[0];
        SetSpeechMessage(currentMessegeIndex);
    }

    private void SetSpeechMessage(int messageIndex) {
        characterImage.sprite = currentSpeech.characterSprites[messageIndex];
        massege.text = currentSpeech.messages[messageIndex].message;
    }

    private void SetSpeech(SpeechSO speech) {
        currentSpeech = speech;
        currentMessegeIndex = 0;
        SetSpeechMessage(currentMessegeIndex);
    }
    #endregion

    #region Show message methods
    private void ShowFailPlaceMessage(SpeechSO speech) {
        ShowOverlay();

        SetSpeech(speech);
    }

    private void ShowFailRelationshipMessage(SpeechSO speech) {
        ShowOverlay();

        SetSpeech(speech);
    }

    private void ShowUpgradePlaceMessage(int placeIndex) {
        currentMessegeIndex = 0;
        currentSpeech = speeches.speeches[placeIndex];
        SetSpeechMessage(currentMessegeIndex);

        ShowOverlay();
    }

    private void ShowUpgradeRelationshipMessage(int placeIndex) {
        currentMessegeIndex = 0;
        currentSpeech = relationshipSpeeches.speeches[placeIndex];
        SetSpeechMessage(currentMessegeIndex);

        ShowOverlay();
    }

    private void ShowGameOverSpeech(SpeechSO speech) {
        ShowOverlay();

        SetSpeech(speech);
        finalSpeechShown = true;
    }

    private void ShowEndOfRelationshipsSpeech(SpeechSO speech) {
        ShowOverlay();

        SetSpeech(speech);
    }
    #endregion

    public void ContinueButton() {
        int lastMessagesIndex = currentSpeech.messages.Count - 1;

        if (currentMessegeIndex < lastMessagesIndex) {
            currentMessegeIndex++;
            SetSpeechMessage(currentMessegeIndex);
        }
        else {
            HideOverlay();
            if (OnCloseOverlay != null) OnCloseOverlay();
            if (OnCloseEndSpeech != null && finalSpeechShown) OnCloseEndSpeech();
        }
    }

    private void ShowOverlay() {
        mainUI.SetActive(false);
        overlay.SetActive(true);
    }

    private void HideOverlay() {
        mainUI.SetActive(true);
        overlay.SetActive(false);
    }

    #region Event subscriptions
    private void AddRelationshipsControllerEvents() {
        RelationshipsController.OnUpgrade_Place += ShowUpgradePlaceMessage;
        RelationshipsController.OnUpgrade_Relationship += ShowUpgradeRelationshipMessage;

        RelationshipsController.OnFail_Place += ShowFailPlaceMessage;
        RelationshipsController.OnFail_Relationship += ShowFailRelationshipMessage;

        RelationshipsController.OnGameOver += ShowGameOverSpeech;
        //RelationshipsController.OnEndOfPlaces += ShowFinalSpeech;
        RelationshipsController.OnEndOfRelationships += ShowEndOfRelationshipsSpeech;
    }
    #endregion
}
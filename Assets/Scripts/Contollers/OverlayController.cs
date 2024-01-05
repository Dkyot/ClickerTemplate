using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class OverlayController : MonoBehaviour
{
    // main UI elements
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject mainUI;
  
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI massege;

    private bool finalSpeechShown = false;

    // overlay events
    public delegate void OnCloseOverlayDelegate();
	public static event OnCloseOverlayDelegate OnCloseOverlay;

    public delegate void OnCloseEndSpeechDelegate();
	public static event OnCloseEndSpeechDelegate OnCloseEndSpeech;

    // current speech
    private int currentMessegeIndex;
    private SpeechSO currentSpeech;

    private void Awake() {
        AddRelationshipsControllerEvents();

        characterImage = characterImage.GetComponent<Image>();
    }

    #region Set message methods
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
    private void ShowSpeech(SpeechSO speech) {
        SetSpeech(speech);
        ShowOverlay();
    }

    private void ShowGameOverSpeech(SpeechSO speech) {
        ShowSpeech(speech);
        finalSpeechShown = true;
    }
    #endregion

    #region UI methods
    private void ShowOverlay() {
        mainUI.SetActive(false);
        overlay.SetActive(true);
    }

    private void HideOverlay() {
        mainUI.SetActive(true);
        overlay.SetActive(false);
    }
    
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
    #endregion

    #region Event subscriptions
    private void AddRelationshipsControllerEvents() {
        RelationshipsController.OnStartGame += ShowSpeech;
        RelationshipsController.OnGameOver += ShowGameOverSpeech;
        //RelationshipsController.OnEndOfPlaces += ShowFinalSpeech;
        RelationshipsController.OnEndOfRelationships += ShowSpeech;

        RelationshipsController.OnUpgrade_Place += ShowSpeech;
        RelationshipsController.OnUpgrade_Relationship += ShowSpeech;

        RelationshipsController.OnFail_Place += ShowSpeech;
        RelationshipsController.OnFail_Relationship += ShowSpeech;
    }
    #endregion
}
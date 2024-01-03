using UnityEngine;

using TMPro;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class OverlayController : MonoBehaviour
{
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject mainUI;

    [SerializeField] private StorySO speeches;
    [SerializeField] private StorySO relationshipSpeeches;
    
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI massege;

    private int currentMessegeIndex;
    private SpeechSO currentSpeech;

    
    private void Awake() {
        AddRelationshipsControllerEvents();

        characterImage = characterImage.GetComponent<Image>();

        SetStartSpeech();
    }

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


    // private void ChangeOverlayMessage(int characterIndex, int placeIndex) {
    //     // ShowOverlay();

    //     // currentMessegeIndex = 0;
    //     // currentSpeechIndex = characterIndex;

    //     // SetMessage();

    //     //! при переходе должна быть реплика перехода
    // }








    private void ShowFailPlaceMessage(SpeechSO speech) {
        ShowOverlay();

        SetSpeech(speech);
    }

    private void ShowFailRelationshipMessage(SpeechSO speech) {
        ShowOverlay();

        SetSpeech(speech);
    }






    // // при улучшении отношений показывать другой текст
    // private void ShowUpgradeRelationshipMessage(SpeechSO speech) {
    //     ShowOverlay();

    //     SetSpeech(speech);
    // }


    private void foow(int placeIndex) {//Debug.Log(placeIndex);
        currentMessegeIndex = 0;
        currentSpeech = speeches.speeches[placeIndex];
        SetSpeechMessage(currentMessegeIndex);

        ShowOverlay();
    }

    private void vvvoow(int placeIndex) {//Relationship
        currentMessegeIndex = 0;
        currentSpeech = relationshipSpeeches.speeches[placeIndex];
        SetSpeechMessage(currentMessegeIndex);

        ShowOverlay();
    }







    public void ContinueButton() {
        int lastMessagesIndex = currentSpeech.messages.Count - 1;

        if (currentMessegeIndex < lastMessagesIndex) {
            currentMessegeIndex++;
            SetSpeechMessage(currentMessegeIndex);
        }
        else
            HideOverlay();
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
        RelationshipsController.OnUpgrade_Place += foow;
        RelationshipsController.OnUpgrade_Relationship += vvvoow;

        RelationshipsController.OnFail_Place += ShowFailPlaceMessage;
        RelationshipsController.OnFail_Relationship += ShowFailRelationshipMessage;
        
        //RelationshipsController.OnUpgrade_Relationship += ShowUpgradeRelationshipMessage;
    }
    #endregion
}

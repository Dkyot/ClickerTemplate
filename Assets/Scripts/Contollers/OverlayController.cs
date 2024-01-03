using UnityEngine;

using TMPro;
using UnityEngine.UI;
using System;

public class OverlayController : MonoBehaviour
{
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject mainUI;

    [SerializeField] private PlacesSO places;

    [SerializeField] private CharacterSO characters;
    
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI massege;
    
    private void Awake() {
        AddRelationshipsControllerEvents();

        characterImage = characterImage.GetComponent<Image>();
        characterImage.sprite = characters.sprites[0];
        massege.text = places.messages[0].message;
    }

    private void ChangeOverlaySprites(int characterIndex, int placeIndex) {
        mainUI.SetActive(false);
        overlay.SetActive(true);

        if (characterIndex < characters.sprites.Count)
            characterImage.sprite = characters.sprites[characterIndex];
        if (placeIndex < places.sprites.Count)
            massege.text = places.messages[placeIndex].message;
    }

    private void ShowFailMessage(String failMessage) {
        mainUI.SetActive(false);
        overlay.SetActive(true);

        massege.text = failMessage;
    }

    // при улучшении отношений показывать другой текст
    private void ShowUpgradeRelationshipMessage(String upgradeMessage) {
        mainUI.SetActive(false);
        overlay.SetActive(true);

        massege.text = upgradeMessage;
    }

    #region Event subscriptions
    private void AddRelationshipsControllerEvents() {
        RelationshipsController.OnUpgrade += ChangeOverlaySprites;
        RelationshipsController.OnFailBuy += ShowFailMessage;
        RelationshipsController.OnUpgrade_Relationship += ShowUpgradeRelationshipMessage;
    }
    #endregion
}

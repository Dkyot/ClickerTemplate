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

    private void ChangeOverlaySprites(int a, int b) {
        mainUI.SetActive(false);
        overlay.SetActive(true);

        if (a < characters.sprites.Count)
            characterImage.sprite = characters.sprites[a];
        if (b < places.sprites.Count)
            massege.text = places.messages[b].message;
    }

    private void ShowFailMessage(String a) {
        mainUI.SetActive(false);
        overlay.SetActive(true);

        massege.text = a;
    }

    // при улучшении отношений показывать другой текст
    private void ShowUpgradeRelationshipMessage(String a) {
        mainUI.SetActive(false);
        overlay.SetActive(true);

        massege.text = a;
    }

    #region Event subscriptions
    private void AddRelationshipsControllerEvents() {
        RelationshipsController.OnUpgrade += ChangeOverlaySprites;
        RelationshipsController.OnFailBuy += ShowFailMessage;
        RelationshipsController.OnUpgrade_Relationship += ShowUpgradeRelationshipMessage;
    }
    #endregion
}

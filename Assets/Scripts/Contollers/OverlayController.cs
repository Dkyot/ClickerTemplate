using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class OverlayController : MonoBehaviour
{
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject mainUI;

    [SerializeField] private Image characterSprite;
    [SerializeField] private TextMeshProUGUI messageText;
    
    private void Awake() {
        AddRelationshipsControllerEvents();
    }

    private void ChangeOverlaySprites(int a, int b) {
        mainUI.SetActive(false);
        overlay.SetActive(true);
    }

    #region Event subscriptions
    private void AddRelationshipsControllerEvents() {
        RelationshipsController.OnUpgrade += ChangeOverlaySprites;
    }
    #endregion
}

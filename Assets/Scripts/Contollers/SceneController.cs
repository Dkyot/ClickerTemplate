using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private PlacesSO places;

    [SerializeField] private Image backgroundImage;

    private void Awake() {
        AddRelationshipsControllerEvents();

        backgroundImage = backgroundImage.GetComponent<Image>();
        SetFirstBackground();
    }

    #region UI methods
    private void SetFirstBackground() {
        var firstPlace = places.sprites[0];
        backgroundImage.sprite = firstPlace;
    }

    private void ChangeBackgroundImage(int characterIndex, int placeIndex) {
        if (placeIndex < places.sprites.Count)
            backgroundImage.sprite = places.sprites[placeIndex];
    }
    #endregion

    #region Event subscriptions
    private void AddRelationshipsControllerEvents() {
        RelationshipsController.OnUpgrade += ChangeBackgroundImage;
    }
    #endregion
}

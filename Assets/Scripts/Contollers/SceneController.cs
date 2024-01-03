using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private PlacesSO places;

    [SerializeField] private Image backgroundImage;

    private void Awake() {
        AddRelationshipsControllerEvents();

        backgroundImage = backgroundImage.GetComponent<Image>();
        backgroundImage.sprite = places.sprites[0];
    }

    private void ChangeBackgroundImage(int characterIndex, int placeIndex) {
        if (placeIndex < places.sprites.Count)
            backgroundImage.sprite = places.sprites[placeIndex];
    }

    #region Event subscriptions
    private void AddRelationshipsControllerEvents() {
        RelationshipsController.OnUpgrade += ChangeBackgroundImage;
    }
    #endregion
}

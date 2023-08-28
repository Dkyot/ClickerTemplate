using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    [SerializeField] private PlacesSO places;

    [SerializeField] private Image backgroundImage;

    private void Awake() {
        AddRelationshipsControllerEvents();

        backgroundImage = backgroundImage.GetComponent<Image>();

        backgroundImage.sprite = places.sprites[0];
    }

    private void ChangeBackgroundImage(int a, int b) {
        if (b < places.sprites.Count)
            backgroundImage.sprite = places.sprites[b];
    }

    #region Event subscriptions
    private void AddRelationshipsControllerEvents() {
        RelationshipsController.OnUpgrade += ChangeBackgroundImage;
    }
    #endregion
}

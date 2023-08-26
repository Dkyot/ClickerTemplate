using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    [SerializeField] private PlacesSO places;
    [SerializeField] private CharacterSO characters;

    [SerializeField] private Image characterImage;
    [SerializeField] private Image backgroundImage;

    [SerializeField] private TextMeshProUGUI massege;

    private void Awake() {
        AddRelationshipsControllerEvents();

        characterImage = characterImage.GetComponent<Image>();
        backgroundImage = backgroundImage.GetComponent<Image>();

        characterImage.sprite = characters.sprites[0];
        backgroundImage.sprite = places.sprites[0];
        massege.text = places.massege[0];
    }

    private void ChangeBackgroundImage(int a, int b) {
        if (a < characters.sprites.Count)
            characterImage.sprite = characters.sprites[a];
        if (b < places.sprites.Count) {
            backgroundImage.sprite = places.sprites[b];
            massege.text = places.massege[b];
        }
    }

    #region Event subscriptions
    private void AddRelationshipsControllerEvents() {
        RelationshipsController.OnUpgrade += ChangeBackgroundImage;
    }
    #endregion
}

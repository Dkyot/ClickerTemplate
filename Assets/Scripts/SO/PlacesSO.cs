using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Place_", menuName = "SO_Places")]
public class PlacesSO : ScriptableObject
{
    public List<Sprite> sprites;
    public List<MessageSO> messages;
}

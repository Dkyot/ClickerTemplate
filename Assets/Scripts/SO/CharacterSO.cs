using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Characters_", menuName = "SO_Characters")]
public class CharacterSO : ScriptableObject
{
    public List<Sprite> sprites;
}

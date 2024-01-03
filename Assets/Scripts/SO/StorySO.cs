using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story_", menuName = "SO_Stories")]
public class StorySO : ScriptableObject
{
    public List<SpeechSO> speeches;
}

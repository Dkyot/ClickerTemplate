using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Speech_", menuName = "SO_Speeches")]
public class SpeechSO : ScriptableObject
{
    public List<Sprite> characterSprites;
    public List<MessageSO> messages;
}

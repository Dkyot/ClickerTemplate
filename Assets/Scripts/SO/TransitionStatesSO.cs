using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransitionState_", menuName = "SO_TransitionStates")]
public class TransitionStatesSO : ScriptableObject
{
    public List<TransitionCondition_Place> transitionsPlace;
    public List<TransitionCondition_Character> transitionsCharacter;
}

[Serializable]
public class TransitionCondition_Place {
    public int placeIndex;
    public int minCharacterIndex;
    public SpeechSO fail;
}

[Serializable]
public class TransitionCondition_Character {
    public int characterIndex;
    public int minPlaceIndex;
    public SpeechSO fail;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeMessages_", menuName = "SO_UpgradeMessages")]
public class RelationshipUpgradeMessagesSO : ScriptableObject
{
    public List<MessageSO> upgradeMessages;
}

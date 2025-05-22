using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/StageData")]
public class StageData : ScriptableObject
{
    public string stageName;
    public float timeLimit = 180f;
}
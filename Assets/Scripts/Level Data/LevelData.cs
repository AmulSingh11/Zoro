using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Puzzle/LevelData")]
public class LevelData : ScriptableObject
{
    public Texture[] cylinderTextures;
    public Vector3[] initialRotations;
    public Vector3[] correctRotations;
    public Vector3[] correctPlacements;
    public int[] groupIds; // Array to specify group IDs for each cylinder (-1 for no group)
    public float timeLimit;
    public string Question;
}
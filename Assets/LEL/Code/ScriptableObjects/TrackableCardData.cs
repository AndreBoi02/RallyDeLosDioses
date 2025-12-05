using UnityEngine;

[CreateAssetMenu(fileName = "TrackableCardData", menuName = "Scriptable Objects/TrackableCardData")]
public class TrackableCardData : ScriptableObject{
    [Header("Card's Identifier")]
    public string referenceImageName;

    [Header("Clues")]
    [TextArea] public string[] clues;
}

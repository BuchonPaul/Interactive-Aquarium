using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "ScriptableObjects/FishData", order = 1)]
public class FishData : ScriptableObject
{
    public string fishName;
    public string description;
    public Sprite clearSprite;
    public Sprite hideSprite;
    public Sprite siz;
    public Sprite wei;
    public Sprite spe;
    public Sprite col;
}
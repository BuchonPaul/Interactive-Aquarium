using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "ScriptableObjects/FishData", order = 1)]
public class FishData : ScriptableObject
{
    public string fishName;
    public string description;
    public string lsv;
    public Sprite clearSprite;
    public Sprite hideSprite;
    public Sprite clearDesc;
    public Sprite hideDesc;
    public Sprite photo;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public GameObject[] fishs;
    public TextMeshProUGUI descText;
    public string debugText = "testManager";
    public Fish fishToFind;
    public int fishCount = 0;

    public delegate void updateCardEventHandler(int fishId);
    public static event updateCardEventHandler UpdateCardEvent;

    void Awake()
    {
        _instance = this;
    }
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }
    }

    private void OnEnable()
    {
        Fish.FishSelectedEvent += OnFishSelected;
        RayCastingScript.FishCatchedEvent += OnFishCatched;
        FootInteraction.FishWalkedEvent += OnFishCatched;
    }

    private void OnDisable()
    {
        Fish.FishSelectedEvent -= OnFishSelected;
        RayCastingScript.FishCatchedEvent -= OnFishCatched;
        FootInteraction.FishWalkedEvent -= OnFishCatched;
    }

    private void OnFishSelected(Fish fish)
    {
        descText.text = fish.description;
        fishToFind = fish;
    }
    private void OnFishCatched(Fish fish)
    {
        Debug.Log(fish.fishId);
        if(fishToFind != null)
        {
            if (fish.fishId == fishToFind.fishId)
            {
                Debug.Log("Catched:" + fish.fishId);
                fishCount += 1;
                fishToFind = null;
                UpdateCardEvent.Invoke(fish.fishId);
            }
            Debug.Log(fishs.Length);
            if (fishs.Length == fishCount)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }

    }
}

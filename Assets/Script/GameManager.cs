using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public PhotoCapture CameraphotoCapture;
    public PhotoCapture QuizzphotoCapture;

    private static GameManager _instance;
    public GameObject[] fishs;

    public FishBehavior fishToFind;
    public string fishToFindName;
    public int fishCount = 0;

    public delegate void updateCardEventHandler(int fishId);
    public static event updateCardEventHandler UpdateCardEvent;

    public Image RightPage;

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
        FishBehavior.FishSelectedEvent += OnFishSelected;
        OrthoRayCastingScript.FishCatchedEvent += OnFishCatched;
        FootInteraction.FishWalkedEvent += OnFishCatched;
    }

    private void OnDisable()
    {
        FishBehavior.FishSelectedEvent -= OnFishSelected;
        OrthoRayCastingScript.FishCatchedEvent -= OnFishCatched;
        FootInteraction.FishWalkedEvent -= OnFishCatched;
    }

    private void OnFishSelected(FishBehavior fish, bool found)
    {
        if (found)
        {
            fishToFind = null;
            fishToFindName = null;
            RightPage.sprite = fish.fishData.clearDesc;
        }
        else
        {
            fishToFind = fish;
            fishToFindName = fish.fishData.name;
            RightPage.sprite = fish.fishData.hideDesc;
        }
    }
    private void OnFishCatched(FishBehavior fish)
    {
        Debug.Log(fish.fishId);
        if (fishToFind != null)
        {
            CameraphotoCapture.RemovePhoto();
            QuizzphotoCapture.RemovePhoto();

            if (fish.fishId == fishToFind.fishId)
            {
                StartCoroutine(CameraphotoCapture.CapturePhoto(fishToFindName, true, fish));
                StartCoroutine(QuizzphotoCapture.CapturePhoto(fishToFindName, true, fish));

                Debug.Log("Catched:" + fish.fishId);
                fishCount += 1;
                fishToFind = null;
                fishToFindName = null;
                UpdateCardEvent.Invoke(fish.fishId);
                RightPage.sprite = fish.fishData.clearDesc;
            }
            else
            {
                StartCoroutine(CameraphotoCapture.CapturePhoto(fishToFindName, false, fish));
            }
            Debug.Log(fishs.Length);
            if (fishs.Length == fishCount)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

    }
}

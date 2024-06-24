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

    public GameObject leo;
    public GameObject prof;
    public Image RightPage;

    public AudioClip wrongClip;
    public AudioClip selectClip;
    public AudioClip goodClip;
    public AudioClip ambientClip;
    private AudioSource audioSource;

    void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        leo.SetActive(false);
        prof.SetActive(false);
        audioSource = gameObject.AddComponent<AudioSource>();
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
        audioSource.PlayOneShot(selectClip);
        if (found)
        {
            fishToFind = null;
            fishToFindName = null;
            RightPage.sprite = fish.fishData.clearDesc;
            leo.SetActive(false);
            prof.SetActive(true);
        }
        else
        {
            fishToFind = fish;
            fishToFindName = fish.fishData.name;
            RightPage.sprite = fish.fishData.hideDesc;
            leo.SetActive(true);
            prof.SetActive(false);
        }
    }
    private void OnFishCatched(FishBehavior fish)
    {
        Debug.Log(fish.fishId);
        if (fishToFind != null)
        {
            //StartCoroutine(CameraphotoCapture.RemovePhoto());
            CameraphotoCapture.RemovePhoto();
            //QuizzphotoCapture.RemovePhoto();

            if (fish.fishId == fishToFind.fishId)
            {
                StartCoroutine(CameraphotoCapture.CapturePhoto(fishToFindName, true, fish));
                StartCoroutine(QuizzphotoCapture.CapturePhoto(fishToFindName, true, fish));
                leo.SetActive(false);
                prof.SetActive(true);
                Debug.Log("Catched:" + fish.fishId);
                fishCount += 1;
                fishToFind = null;
                fishToFindName = null;
                UpdateCardEvent.Invoke(fish.fishId);
                RightPage.sprite = fish.fishData.clearDesc;
            }
            else
            {
                audioSource.PlayOneShot(wrongClip);
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

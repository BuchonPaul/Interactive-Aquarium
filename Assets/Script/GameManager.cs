using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    /*    [Header("Aquarium")]
        [Header("Photo Taker")]
        [SerializeField] private Image photoDisplayArea;
        [SerializeField] private GameObject photoFrame;
        [Header("Flash Effect")]
        [SerializeField] private GameObject cameraFlash;
        [SerializeField] private float flashTime;
        [Header("Photo Fade Effect")]
        [SerializeField] private Animator animator;


        [Header("Quizz")]
        [Header("Photo Taker")]
        [SerializeField] private Image QuizzPhotoDisplayArea;
        [SerializeField] private GameObject QuizzPhotoFrame;
        [Header("Photo Fade Effect")]
        [SerializeField] private Animator QuizzAnimator;*/
    public PhotoCapture CameraphotoCapture;
    public PhotoCapture QuizzphotoCapture;

    private static GameManager _instance;
    public GameObject[] fishs;
    public TextMeshProUGUI descText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI who;
    public string debugText = "testManager";
    public FishBehavior fishToFind;
    public int fishCount = 0;

    public delegate void updateCardEventHandler(int fishId);
    public static event updateCardEventHandler UpdateCardEvent;

    public Button sizeButt;
    public Button weigButt;
    public Button speeButt;
    public Button coloButt;

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

    private void OnFishSelected(FishBehavior fish)
    {
        FishData fisdata = fish.fishData;
        descText.text = fisdata.description;
        nameText.text = fisdata.fishName;
        who.text = "Qui suis-je ?";
        sizeButt.GetComponent<Image>().sprite = fisdata.siz;
        weigButt.GetComponent<Image>().sprite = fisdata.wei;
        speeButt.GetComponent<Image>().sprite = fisdata.spe;
        coloButt.GetComponent<Image>().sprite = fisdata.col;
        fishToFind = fish;
    }
    private void OnFishCatched(FishBehavior fish)
    {
        Debug.Log(fish.fishId);
        if(fishToFind != null)
        {
            CameraphotoCapture.RemovePhoto();
            QuizzphotoCapture.RemovePhoto();

            if (fish.fishId == fishToFind.fishId)
            {
                StartCoroutine(CameraphotoCapture.CapturePhoto(fish, true));
                StartCoroutine(QuizzphotoCapture.CapturePhoto(fish, true));

                Debug.Log("Catched:" + fish.fishId);
                fishCount += 1;
                fishToFind = null;
                UpdateCardEvent.Invoke(fish.fishId);
            }
            else
            {
                StartCoroutine(CameraphotoCapture.CapturePhoto(fish, false));
            }
            Debug.Log(fishs.Length);
            if (fishs.Length == fishCount)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

    }
}

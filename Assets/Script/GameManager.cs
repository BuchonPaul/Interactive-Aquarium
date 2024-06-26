using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Script de GameManager :
 * Il gère le fonctionnement global du jeu et fait la liaison entre le bassin et l'écran de menu.
 */
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

    public GameObject returnObj;
    public GameObject leo;
    public GameObject prof;
    public Image RightPage;

    public AudioClip wrongClip;
    public AudioClip selectClip;
    public AudioClip goodClip;
    private AudioSource audioSource;

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

    // Ces méthodes écoutent les événements des différents composants du code et leur associent une fonction.
    private void OnEnable()
    {
        FishBehavior.FishSelectedEvent += OnFishSelected; // Détecte le clic sur un bouton de sélection de poisson
        OrthoRayCastingScript.FishCatchedEvent += OnFishCatched; // Détecte un clic sur un poisson du bassin par le RayCast
        FootInteraction.FishWalkedEvent += OnFishCatched; // Détecte la collision entre un pied et un poisson
    }

    private void OnDisable()
    {
        FishBehavior.FishSelectedEvent -= OnFishSelected;
        OrthoRayCastingScript.FishCatchedEvent -= OnFishCatched;
        FootInteraction.FishWalkedEvent -= OnFishCatched;
    }

    // Cette fonction s'exécute quand un poisson est sélectionné dans le menu.
    private void OnFishSelected(FishBehavior fish, bool found)
    {
        audioSource.PlayOneShot(selectClip); // On joue un son de clic
        if (found) // Si le poisson est déjà trouvé
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

    // Cette fonction s'exécute s'il y a une interaction entre l'utilisateur et le bassin.
    private void OnFishCatched(FishBehavior fish)
    {
        if (fishToFind != null) // Si un poisson est en train d'être cherché
        {
            CameraphotoCapture.RemovePhoto(); // On retire l'overlay photo du bassin

            if (fish.fishId == fishToFind.fishId) // Si le poisson avec lequel l'utilisateur a interagi est le bon :
            {
                audioSource.PlayOneShot(goodClip);

                StartCoroutine(CameraphotoCapture.CapturePhoto(fishToFindName, true, fish));
                StartCoroutine(QuizzphotoCapture.CapturePhoto(fishToFindName, true, fish));
                leo.SetActive(false);
                prof.SetActive(true);
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

            if (fishs.Length == fishCount) // Si tous les poissons on été trouvés
            {
                returnObj.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
    }
    public void OnApplicationQuit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

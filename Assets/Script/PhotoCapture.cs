using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* Script qui permet de gérér les photos dans le code
 */
public class PhotoCapture : MonoBehaviour
{
    [Header("Aquarium")]
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    [Header("Photo Fade Effect")]
    [SerializeField] private Animator animator;
    [SerializeField] private Animator BCKanimator;

    private Texture2D screenCapture;
    public bool viewingPhoto;
    private float timeShoot;
    private Sprite photoSprite;

    public bool isAqua = true;
    public bool QuizzVisible = true;

    public TextMeshProUGUI photoText;
    public TextMeshProUGUI LSVText;
    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    void Update()
    {
        timeShoot += Time.deltaTime;
        if (timeShoot > 5 )
        {
            RemovePhoto();
        }

    }

   // Cette fonction appellée par le gameManager permet de générer une photo en focntion du poisson cliqué
    public IEnumerator CapturePhoto(string fishName, bool isGood, FishBehavior snapedFish)
    {
        viewingPhoto = true;
        yield return new WaitForEndOfFrame();

        photoText.color = Color.black;
        photoText.fontStyle = FontStyles.Normal;
        photoText.text = fishName;
        if (!isGood ) // Si ça n'est pas le bon poisson on change le style de l'écriture
        {
            photoText.color = Color.red;
            photoText.fontStyle = FontStyles.Strikethrough;
        }
        photoSprite = snapedFish.fishData.photo;
        if (LSVText)
        {
            LSVText.text = snapedFish.fishData.lsv;
        }
        ShowPhoto();
        timeShoot = 0;
    }

    IEnumerator CameraFlashEffect()
    {
        if(cameraFlash != null)
        {
            cameraFlash.SetActive(true);
            yield return new WaitForSeconds(flashTime);
            cameraFlash.SetActive(false);
        }
    }

    // Afficahge de la photo
    void ShowPhoto()    
    {
        photoDisplayArea.sprite = photoSprite;
        photoFrame.SetActive(true);
        QuizzVisible = true;

        if (BCKanimator)
        {
            BCKanimator.Play("PhotoFade2");
        }
        StartCoroutine(CameraFlashEffect());
        animator.Play("PhotoFade");
    }

    // Disparition de la photo
    public void RemovePhoto()
    {
        viewingPhoto = false;
        if (isAqua)
        {
            photoFrame.SetActive(false);

        }
        else
        {
            if (QuizzVisible) // Dans le cas ou il sagit de la photo de la page de quizz, il y a une animation a l'affichage et à la disparition
            {
                QuizzVisible = false;
                BCKanimator.Play("PhotoFadeReverse");
                StartCoroutine(RemovePhotoQ());
            }
        }
    }
    IEnumerator RemovePhotoQ()
    {
        yield return new WaitForSeconds(0.3f);
        photoFrame.SetActive(false);

    }
}

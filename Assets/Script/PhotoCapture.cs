using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private Texture2D screenCapture;
    public bool viewingPhoto;
    private float timeShoot;
    private Sprite photoSprite;

    public TextMeshProUGUI photoText;
    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    void Update()
    {
        timeShoot += Time.deltaTime;
        if (timeShoot > 2)
        {
            RemovePhoto();
        }
    }

    public IEnumerator CapturePhoto(string fishName, bool isGood, FishBehavior snapedFish)
    {
        viewingPhoto = true;
        yield return new WaitForEndOfFrame();

        photoText.color = Color.black;
        photoText.fontStyle = FontStyles.Normal;
        photoText.text = fishName;
        if (!isGood )
        {
            photoText.color = Color.red;
            photoText.fontStyle = FontStyles.Strikethrough;
        }
        photoSprite = snapedFish.fishData.photo;
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
    void ShowPhoto()    
    {
        photoDisplayArea.sprite = photoSprite;

        photoFrame.SetActive(true);
        StartCoroutine(CameraFlashEffect());
        animator.Play("PhotoFade");
    }

    public void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
    }
}

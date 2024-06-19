using System.Collections;
using System.Collections.Generic;
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
    private bool viewingPhoto;
    private float timeShoot;
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

    public IEnumerator CapturePhoto(FishBehavior fishCaptured, bool isGood)
    {
        viewingPhoto = true;
        yield return new WaitForEndOfFrame();

        Rect regionToHead = new Rect(0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToHead, 0, 0, false);
        screenCapture.Apply();
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
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0f, 0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100);
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

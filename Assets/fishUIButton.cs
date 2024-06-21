using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class fishUIButton : MonoBehaviour
{
    public FishBehavior prefabPoisson;

    private Image buttonImage;

    public bool founded;

    void Start()
    {
        Assert.IsNotNull(prefabPoisson, "Le prefab du poisson n'est pas assigné au bouton !");

        buttonImage = GetComponent<Image>();
        RectTransform buttonRectTransform = GetComponent<RectTransform>();

        //GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

        if (prefabPoisson != null)
        {
            buttonImage.sprite = prefabPoisson.fishData.hideSprite;
            buttonRectTransform.sizeDelta = new Vector2(prefabPoisson.fishData.hideSprite.rect.width, prefabPoisson.fishData.hideSprite.rect.height);

        }
    }
    public void updateCard(int fishId)
    {
        if (prefabPoisson.GetComponent<FishBehavior>().fishId == fishId)
        {
            FishBehavior fishComponent = prefabPoisson.GetComponent<FishBehavior>();

            if (fishComponent != null)
            {
                founded = true;
                buttonImage.sprite = fishComponent.fishData.clearSprite;
            }
        }
    }

    private void OnEnable()
    {
        GameManager.UpdateCardEvent += updateCard;
    }

    private void OnDisable()
    {
        GameManager.UpdateCardEvent -= updateCard;
    }
}

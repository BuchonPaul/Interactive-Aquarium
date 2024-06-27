using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
// Script qui gère les clic sur les boutons du menu
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

        if (prefabPoisson != null)
        {
            buttonImage.sprite = prefabPoisson.fishData.hideSprite;
            buttonRectTransform.sizeDelta = new Vector2(prefabPoisson.fishData.hideSprite.rect.width, prefabPoisson.fishData.hideSprite.rect.height);

        }
    }
    public void updateCard(int fishId)
    { 
        // Quand le poisson est trouvé le style de la carte change
        if (prefabPoisson.GetComponent<FishBehavior>().fishId == fishId)
        {
            FishBehavior fishComponent = prefabPoisson.GetComponent<FishBehavior>();

            if (fishComponent != null)
            {
                founded = true;
                buttonImage.sprite = null;
                buttonImage.color = Color.clear;
            }
        }
    }

    //Un évènement est émit au clic sur la carte.
    //Le GameManager écoute cet évent
    private void OnEnable()
    {
        GameManager.UpdateCardEvent += updateCard;
    }

    private void OnDisable()
    {
        GameManager.UpdateCardEvent -= updateCard;
    }
}

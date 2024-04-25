using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class fishButton : MonoBehaviour
{
    public GameObject prefabPoisson;
    private Image buttonImage;
    private Button button;

    private void OnEnable()
    {
        GameManager.UpdateCardEvent += updateCard;
    }

    private void OnDisable()
    {
        GameManager.UpdateCardEvent -= updateCard;
    }

    private void Start()
    {
        Assert.IsNotNull(prefabPoisson, "Le prefab du poisson n'est pas assigné au bouton !");
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();
        Fish fishComponent = prefabPoisson.GetComponent<Fish>();
        
        if (fishComponent != null)
        {        
            buttonImage.sprite = fishComponent.hideSprite;
        }
        else
        {
            Debug.LogWarning("Le prefab du poisson ne contient pas de composant Fish !");
        }
    }
    public void updateCard(int fishId) {
        if (prefabPoisson.GetComponent<Fish>().fishId == fishId)
        {
            Fish fishComponent = prefabPoisson.GetComponent<Fish>();

            if (fishComponent != null)
            {
                buttonImage.sprite = fishComponent.clearSprite;
            }
        }
    }
}

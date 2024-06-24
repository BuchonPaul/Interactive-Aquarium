using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootInteraction : MonoBehaviour
{
    public delegate void FishWalkedEventHandler(FishBehavior fish);
    public static event FishWalkedEventHandler FishWalkedEvent;

    private void Start()
    {
        Debug.Log("ici");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("lalal");

        if (other.gameObject.CompareTag("Fish"))
        {
            FishWalkedEvent?.Invoke(other.GetComponent<FishBehavior>());
        };
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootInteraction : MonoBehaviour
{
    public delegate void FishWalkedEventHandler(FishBehavior fish);
    public static event FishWalkedEventHandler FishWalkedEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if(collision.gameObject.CompareTag("Fish"))
            {
                Debug.Log("Fish collision");
            }
                Debug.Log("collision");

        }
                Debug.Log("trigger");
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            FishWalkedEvent?.Invoke(other.GetComponent<FishBehavior>());
        };
    }
}

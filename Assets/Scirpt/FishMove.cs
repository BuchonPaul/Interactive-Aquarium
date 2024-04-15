using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class FishMove : MonoBehaviour
{
    public float vitesse = 2f;
    private Vector3 direction;
    private float rotation;
    private float initialRotation;
    private float isStartedFrom = 0f;
    private float animationDuration = 0f;
    private int animationType = 0;
    public GameObject viewField;

    void Start()
    {
        ChooseNewAction();
    }

    void Update()
    {   
        isStartedFrom += Time.deltaTime;
        if (isStartedFrom > animationDuration) {
            ChooseNewAction();
        }
        if (animationType == 0)
        {
            transform.Translate(Vector3.forward * vitesse * Time.deltaTime);
        }
        else if (animationType == 1)
        {
            
        }
        else if (animationType == 2)
        {
            animationType = 2;
            //transform.Rotate(Vector3.Lerp(transform.transform.eulerAngles, direction, Time.deltaTime * vitesse / 2));
            transform.eulerAngles = new Vector3(0f, Mathf.Lerp(initialRotation, rotation, Time.deltaTime * vitesse), 0f);
        }
    }

    void ChooseNewAction()
    {
        float choice = Random.Range(0, 1f) * 100;
        isStartedFrom = 0f;
        animationDuration = Random.Range(0.25f, 1.5f);

        if (choice <= 50f)
        {
            animationType = 0;
            direction = transform.forward;
        }
        else if(choice <= 70f)
        {
            animationType = 1;
        }
        else if (choice <= 100f)
        {
            animationType = 2;
            initialRotation = transform.eulerAngles.y;
            rotation = Random.Range(transform.eulerAngles.y - 15f, transform.eulerAngles.y + 15f);
        }   
    }
    void OnTriggerEnter(Collider other)
    {
        isStartedFrom = 0;
        if (other.gameObject.CompareTag("Fish") && other.gameObject != gameObject)
        {
/*            Debug.Log("fish");
            animationDuration = 0.5f;
            animationType = 2;
            direction = new Vector3(-transform.position.x, transform.position.y, -transform.position.z);*/
        }
        if (other.gameObject.CompareTag("Wall") && other.gameObject != gameObject)
        {
            Debug.Log("Wall");
            animationDuration = 0.5f;
            animationType = 2;
            //direction = -transform.position;
            direction = new Vector3(0f, -transform.position.y, 0f);
            transform.Rotate(Vector3.up, Time.deltaTime * vitesse * 40);
        }
    }
}

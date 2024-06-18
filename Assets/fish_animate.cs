using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class fish_animate : MonoBehaviour
{
    public GameObject fish;
    private Fish fishdata;

    private Animator animator;
    private Vector3 previousPosition;
    public Vector3 currentVelocity;

    void Start()
    {
        fishdata = fish.GetComponent<Fish>();
        previousPosition = fish.transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (fish != null)
        {
            Vector3 currentPosition = fishdata.transform.position;
            currentPosition += fishdata.transform.forward * fishdata.swimSpeed * Time.deltaTime;
            currentVelocity = (currentPosition - previousPosition) / Time.deltaTime;
            previousPosition = currentPosition;
            float speedMagnitude = currentVelocity.magnitude;
            float normalizedSpeed = Mathf.InverseLerp(0.5f, 1f, speedMagnitude);
            animator.speed = Mathf.Lerp(0.5f, 1f, normalizedSpeed);

            //Debug.Log("Vitesse de l'objet: " + speedMagnitude);
        }
        else
        {
            Debug.LogWarning("Rigidbody manquant sur l'objet.");
        }
    }
    
}

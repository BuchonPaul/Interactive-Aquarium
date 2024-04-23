using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastingScript : MonoBehaviour
{
    Camera cam;
    public LayerMask mask;
    public delegate void FishCatchedEventHandler(Fish fish);
    public static event FishCatchedEventHandler FishCatchedEvent;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, mask)) {
                //Debug.Log(hit.transform.name);
                FishCatchedEvent?.Invoke(hit.transform.gameObject.GetComponent<Fish>());
                //Destroy(hit.transform.gameObject);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthoRayCastingScript : MonoBehaviour
{
    Camera cam;
    public LayerMask mask;
    public delegate void FishCatchedEventHandler(FishBehavior fish);
    public static event FishCatchedEventHandler FishCatchedEvent;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -cam.transform.position.z;
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        Ray ray = new Ray(worldPos, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);

        if (cam.orthographic)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, mask))
                {
                    FishCatchedEvent?.Invoke(hit.transform.gameObject.GetComponent<FishBehavior>());
                }
            }
        }
    }
}

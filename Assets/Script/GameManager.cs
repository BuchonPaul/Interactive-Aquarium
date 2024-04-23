using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject[] fishs;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections.Generic;
using UnityEngine;

// Fonction qui créé des poissons à des positions aléatoires
public class spawnObject : MonoBehaviour
{
    public List<GameObject> fishObjects;

    void Start()
    {
        for (int x = 0; x < fishObjects.Count; x++)
        {
            for (int i = 0; i < fishObjects[x].GetComponent<FishBehavior>().fishQuantity; i++)
            {

                float randomX = Random.Range(-14f, 14f);
                float randomY = UnityEngine.Random.Range(-7f, 7f);
                Vector3 position = new Vector3(randomX, fishObjects[x].transform.position.y, randomY);
                Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                if (fishObjects[x].GetComponent<FishBehavior>().spawnPos != new Vector3(0, 0, 0))
                {
                    position = fishObjects[x].GetComponent<FishBehavior>().spawnPos;
                }
                Instantiate(fishObjects[x], position, rotation);
            }
        }
    }
}

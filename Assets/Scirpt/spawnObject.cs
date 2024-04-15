using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour
{
    public GameObject prefab; // GameObject à instancier
    public int numberOfObjects = 50; // Nombre d'objets à générer

    void Start()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Génère des coordonnées aléatoires sur les axes X et Y entre 0 et 1
            float randomX = Random.Range(-9f, 9f);
            float randomY = Random.Range(-4f, 4f);

            // Crée une position en fonction des coordonnées aléatoires
            Vector3 position = new Vector3(randomX, prefab.transform.position.y, randomY);

            // Génère une rotation aléatoire
            Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            // Instancie l'objet avec la position et la rotation aléatoires
            Instantiate(prefab, position, rotation);
        }
    }
}

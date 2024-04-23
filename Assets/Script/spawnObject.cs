using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour
{
    public int numberOfObjects = 2;

    void Start()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            for (int x = 0; x < GameManager.Instance.fishs.Length; x++)
            {
                
                // Génère des coordonnées aléatoires sur les axes X et Y entre 0 et 1
                float randomX = Random.Range(-14f, 14f);
                float randomY = Random.Range(-7f, 7f);

                // Crée une position en fonction des coordonnées aléatoires
                Vector3 position = new Vector3(randomX, GameManager.Instance.fishs[x].transform.position.y, randomY);

                // Génère une rotation aléatoire
                Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                // Instancie l'objet avec la position et la rotation aléatoires
                Instantiate(GameManager.Instance.fishs[x], position, rotation);
            }
        }
    }
}

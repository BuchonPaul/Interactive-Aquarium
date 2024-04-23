using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour
{
    public GameObject prefab; // GameObject � instancier
    public int numberOfObjects = 50; // Nombre d'objets � g�n�rer

    void Start()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // G�n�re des coordonn�es al�atoires sur les axes X et Y entre 0 et 1
            float randomX = Random.Range(-14f, 14f);
            float randomY = Random.Range(-7f, 7f);

            // Cr�e une position en fonction des coordonn�es al�atoires
            Vector3 position = new Vector3(randomX, prefab.transform.position.y, randomY);

            // G�n�re une rotation al�atoire
            Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            // Instancie l'objet avec la position et la rotation al�atoires
            Instantiate(prefab, position, rotation);
        }
    }
}

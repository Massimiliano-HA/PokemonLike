using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject Item;
    public string sortingLayerName = "itemsLayer";

    public int numberOfItemsToSpawn = 5;

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        for (int i = 0; i < numberOfItemsToSpawn; i++)
        {
            // Définissez les coordonnées de la zone où vous voulez faire apparaître les objets.
            Vector2 spawnPosition = GetRandomPositionInLayer(sortingLayerName);

            // Instanciez votre objet "Item" à la position calculée.
            Instantiate(Item, spawnPosition, Quaternion.identity);
            Debug.Log("spawned");
        }
    }

    Vector2 GetRandomPositionInLayer(string sortingLayerName)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(Vector2.zero, new Vector2(10f, 10f), 0);

        List<Vector2> validPositions = new List<Vector2>();

        foreach (Collider2D collider in colliders)
        {
            // Vérifiez si le collider appartient au Sorting Layer spécifié.
            Renderer renderer = collider.gameObject.GetComponent<Renderer>();

            if (renderer != null && renderer.sortingLayerName == sortingLayerName)
            {
                validPositions.Add(collider.transform.position);
            }
        }

        // Si des positions valides sont trouvées, retournez une position aléatoire parmi elles.
        if (validPositions.Count > 0)
        {
            // Mélangez la liste des positions valides.
            validPositions = ShuffleList(validPositions);

            // Sélectionnez les positions pour les objets suivants dans la liste mélangée.
            for (int i = 0; i < numberOfItemsToSpawn; i++)
            {
                if (i < validPositions.Count)
                {
                    return validPositions[i];
                }
            }
        }

        // Si aucune position valide n'est trouvée, retournez une valeur par défaut ou gérez l'absence de manière appropriée.
        return Vector2.zero;
    }

    List<T> ShuffleList<T>(List<T> list)
    {
        List<T> newList = new List<T>(list);
        int n = newList.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = newList[k];
            newList[k] = newList[n];
            newList[n] = value;
        }
        return newList;
    }
}
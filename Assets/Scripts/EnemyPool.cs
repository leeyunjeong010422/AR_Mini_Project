using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] GameObject[] birdPrefabs;
    [SerializeField] int poolSize = 10;

    [SerializeField] private List<GameObject> pool;

    private void Start()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            int randomIndex = Random.Range(0, birdPrefabs.Length);
            GameObject bird = Instantiate(birdPrefabs[randomIndex]);
            bird.SetActive(false);
            pool.Add(bird);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject bird in pool)
        {
            if (!bird.activeInHierarchy)
            {
                return bird;
            }
        }

        int randomIndex = Random.Range(0, birdPrefabs.Length);
        GameObject newBird = Instantiate(birdPrefabs[randomIndex]);
        newBird.SetActive(false);
        pool.Add(newBird);
        return newBird;
    }
}

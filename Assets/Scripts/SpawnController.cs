using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnController : MonoBehaviour
{
    public GameObject SpawnObject;

    public int xPos;
    public int zPos;
    public int enemyCount;


    void Start()
    {
        StartCoroutine(StartSpawing());
    }

    IEnumerator StartSpawing()
    {
        while (enemyCount < 3)
        {
            xPos = Random.Range(-4, 8);
            zPos = Random.Range(6, 11);
            Instantiate(SpawnObject, new Vector3(xPos, Random.Range(-1, 1), zPos), Quaternion.identity);
            yield return new WaitForSeconds(2);
            enemyCount += 1;
        }
    }
}

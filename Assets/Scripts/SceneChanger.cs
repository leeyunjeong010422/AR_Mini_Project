using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoadOnAnyKey;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Start" && Input.anyKeyDown && !string.IsNullOrEmpty(sceneToLoadOnAnyKey))
        {
            SceneManager.LoadScene("Game");
        }
    }
}

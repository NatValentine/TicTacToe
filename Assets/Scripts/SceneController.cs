using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}

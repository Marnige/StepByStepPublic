using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameOverHandler : MonoBehaviour
{
    private bool _gameOverActivated = false;
    public void GameOver()
    {
        gameObject.SetActive(true);
        _gameOverActivated = true;

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _gameOverActivated)
        {
            SceneManager.LoadScene("StepByStep");
        }
    }
}

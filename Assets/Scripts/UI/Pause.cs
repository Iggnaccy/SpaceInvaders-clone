using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private InputActionReference pauseAction;

    public bool canPause = true;

    private void Start()
    {
        pauseAction.action.performed += _ => TogglePause();
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        pauseAction.action.performed -= _ => TogglePause();
    }

    public void TogglePause()
    {
        if (!canPause) return;
        gameObject.SetActive(!gameObject.activeSelf);
        Time.timeScale = gameObject.activeSelf ? 0 : 1;
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}

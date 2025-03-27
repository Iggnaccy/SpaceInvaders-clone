using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText, victoryText;
    [SerializeField] private ScoreSO score;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(bool victory)
    {
        Time.timeScale = 0;
        scoreText.text = $"Score: {score.score}";
        victoryText.text = victory ? "You Won!!" : "You lost :(";
        gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }
}

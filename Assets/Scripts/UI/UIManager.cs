using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   [SerializeField] private GameObject gameOverScreen;


    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }
    //hien thi man hinh game over
    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        //sound
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int nextLevel;
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }
}


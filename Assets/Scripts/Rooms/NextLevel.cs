using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{
    public int nextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Trigger Entered");

        if (collision.tag == "Player")
        {

            print("Switching Scene to" + nextLevel);
            SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
        }

        
    }
}

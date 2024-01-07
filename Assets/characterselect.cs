using UnityEngine;
using UnityEngine.SceneManagement;


public class characterselect : MonoBehaviour
{
    public int nextLevel;
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }
}

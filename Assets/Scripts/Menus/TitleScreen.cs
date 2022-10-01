using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private string farmSceneName;

	public void PlayGame()
    {
        SceneManager.LoadScene(farmSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

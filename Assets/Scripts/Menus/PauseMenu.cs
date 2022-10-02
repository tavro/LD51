using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Selectable firstSelected;
    [SerializeField] private string titleScreenSceneName;

    private bool isActive = false;
    private GameObject[] children;

    private void Awake()
    {
        children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            children[i] = transform.GetChild(i).gameObject;
    }

    private void Start()
    {
        foreach (GameObject child in children)
            child.SetActive(false);
    }

    private void Update()
    {
        if (isActive != GameManager.Instance.IsPaused) 
        {
            isActive = GameManager.Instance.IsPaused;
            foreach (GameObject child in children)
                child.SetActive(isActive);

            if (isActive)
                firstSelected.Select();
        }
    }

    public void UnpauseGame()
    {
        GameManager.Instance.IsPaused = false;
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene(titleScreenSceneName);
        Destroy(GameManager.Instance.gameObject);
    }
}

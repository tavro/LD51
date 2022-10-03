using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Milk : MonoBehaviour
{
    void OnMouseOver() {
        if (GameManager.Instance.CurrPauseState == GameManager.PauseState.NONE && Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene("FarmScene");
        }
    }
}

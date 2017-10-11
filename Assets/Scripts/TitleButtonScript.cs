using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonScript : MonoBehaviour {

    public GameObject instruct;

    public void OnPlayClick()
    {
        SceneManager.LoadScene(1);
    }
    public void OnInstructClick()
    {
        instruct.SetActive(!instruct.activeSelf);
    }
    public void OnQuitClick()
    {
        Application.Quit();
    }
}

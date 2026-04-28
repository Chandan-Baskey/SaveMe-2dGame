using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIsystem : MonoBehaviour
{
    public void Start()
    {
        SceneManager.LoadScene("LvL-1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}


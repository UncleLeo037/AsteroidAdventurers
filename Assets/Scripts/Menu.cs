using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject nextLevel;
    public GameObject lastLevel;


    public void Start()
    {
        if (Teleport.level > 1)
        {
            nextLevel.SetActive(true);

            if (Teleport.level > 2)
            {
                lastLevel.SetActive(true);
            }
        }
    }

    public void LaunchLevel1()
    {
        if (Teleport.level == 1)
        {
            Teleport.gain = 1;
        }
        else
        {
            Teleport.gain = 0;
        }
        SceneManager.LoadSceneAsync(1);
    }

    public void LaunchLevel2()
    {
        if (Teleport.level == 2)
        {
            Teleport.gain = 1;
        }
        else
        {
            Teleport.gain = 0;
        }
        SceneManager.LoadSceneAsync(2);
    }

    public void LaunchLevel3()
    {
        if (Teleport.level < 3)
        {
            Teleport.gain = 0;
        }
        else
        {
            Teleport.gain = 1;
        }
        SceneManager.LoadSceneAsync(3);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}

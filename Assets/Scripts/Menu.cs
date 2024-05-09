using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private GameObject lastLevel;

    int level;

    private static bool progress;


    public void Start()
    {
        level = PlayerPrefs.GetInt("level", 1);
        if (level > 1)
        {
            nextLevel.SetActive(true);

            if (level > 2)
            {
                lastLevel.SetActive(true);
            }
        }
    }

    public static bool GetProgress()
    {
        return progress;
    }

    public void LaunchLevel1()
    {
        progress = (level == 1);
        SceneManager.LoadSceneAsync(1);
    }

    public void LaunchLevel2()
    {
        progress = (level == 2);
        SceneManager.LoadSceneAsync(2);
    }

    public void LaunchLevel3()
    {
        progress = (level == 3);
        SceneManager.LoadSceneAsync(3);
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("level", 1);
        nextLevel.SetActive(false);
        lastLevel.SetActive(false);
        //Application.Quit();
    }
}

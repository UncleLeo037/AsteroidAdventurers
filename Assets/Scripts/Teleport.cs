using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    static bool one;
    static bool two;

    [SerializeField] private AudioSource teleport;


    private bool progress;

    void Update()
    {
        if (one && two)
        {
            one = false;
            two = false;
            if (Menu.GetProgress())
            {
                int level = PlayerPrefs.GetInt("level", 1);
                level += 1;
                PlayerPrefs.SetInt("level", level);
            }
            teleport.Play();
            SceneManager.LoadSceneAsync(0);
        }
    }

    public void SetProgress(bool progress)
    {
        this.progress = progress;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //should be changed to check for similtanious activation
        if (collision.gameObject.name == "Player1") one = true;
        if (collision.gameObject.name == "Player2") two = true;
    }
}

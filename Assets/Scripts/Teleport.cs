using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    static bool one;
    static bool two;

    [SerializeField] private AudioSource teleport;

    public static int level = 1;
    public static int gain = 0;

    void Update()
    {
        if (one && two)
        {
            one = false;
            two = false;
            level += gain;
            teleport.Play();
            SceneManager.LoadSceneAsync(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //should be changed to check for similtanious activation
        if (collision.gameObject.name == "Player1") one = true;
        if (collision.gameObject.name == "Player2") two = true;
    }
}

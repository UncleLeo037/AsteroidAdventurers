using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    static bool one;
    static bool two;

    void Update()
    {
        if (one && two)
        {
            one = false;
            two = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player1") one = true;
        if (collision.gameObject.name == "Player2") two = true;
    }
}

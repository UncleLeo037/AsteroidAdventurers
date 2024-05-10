using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    static bool one;
    static bool two;

    [SerializeField] private AudioSource teleport;
    private SpriteRenderer portal;
    private Animator animation;
    private CircleCollider2D activater;

    void Start()
    {
        portal = GetComponent<SpriteRenderer>();
        animation = GetComponent<Animator>();
        activater = GetComponent<CircleCollider2D>();
    }


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
        if (collision.gameObject.name == "Player1" && !one)
        {
            activater.enabled = false;
            one = true;
            animation.enabled = true;
            teleport.Play();
            portal.enabled = false;
        }
        if (collision.gameObject.name == "Player2" && !two)
        {
            activater.enabled = false;
            two = true;
            animation.enabled = true;
            teleport.Play();
            portal.enabled = false;
        }
    }
}

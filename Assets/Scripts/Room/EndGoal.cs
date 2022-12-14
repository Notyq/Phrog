using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    private UIManager uiManager;
    [SerializeField] private AudioClip winSound;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            uiManager.GameEnd();
            SoundManager.instance.PlaySound(winSound);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoad : MonoBehaviour
{
    public static LevelLoad Instance;

    private Animator animator;

    private void Awake()
    {
        // Singleton yap�s�
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        animator = GetComponent<Animator>();
    }

    public void FadeIn()
    {
        animator.SetTrigger("Start");
    }

    public void FadeOut()
    {
        animator.SetTrigger("End");
    }
}
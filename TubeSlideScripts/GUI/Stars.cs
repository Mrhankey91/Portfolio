using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    private AudioSource audio;
    private Animator animator;

    private int unlockedStars = 0;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void Show(int stars)
    {
        unlockedStars = stars;
        animator.SetTrigger("Show");
    }

    public void PlaySound(int id)
    {
        if(id < unlockedStars)
            audio.Play();
    }
}

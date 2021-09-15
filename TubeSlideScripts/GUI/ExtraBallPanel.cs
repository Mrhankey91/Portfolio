using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBallPanel : MonoBehaviour
{
    private bool show = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Show()
    {
        if (show)
            return;

        show = true;
        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        if (!show)
            return;

        show = false;
        animator.SetTrigger("Hide");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAnimation : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void StartAnimationClock()
    {
        anim.SetTrigger("FreezeTime");
    }

    public void StartAnimationServe()
    {
        anim.SetTrigger("AutoServe");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBrown : PlatformGreen
{
    [SerializeField] private Animator _BrownAnimator;
    public void PlayAnim()
    {
        _BrownAnimator.SetBool("Destroyed", true);
    }
    public void BrownDestroy()
    {
        Destroy(gameObject);
    }
}

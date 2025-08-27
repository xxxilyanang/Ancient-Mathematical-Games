using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loomCanvas : MonoBehaviour
{
    public Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void OnButtonClick()
    {
        if (anim != null)
        {
            if (!anim.isPlaying)
            {
                anim.Play();
            }
        }
    }
}

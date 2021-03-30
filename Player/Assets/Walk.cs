using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (true)
            {
                anim.SetTrigger("a_Running");
                anim.ResetTrigger("a_Idle");
                anim.ResetTrigger("a_Walking");
            }
            else {
                anim.SetTrigger("a_Walking");
                anim.ResetTrigger("a_Idle");
                anim.ResetTrigger("a_Running");
            }
        }
        else {
            anim.SetTrigger("a_Idle");
            anim.ResetTrigger("a_Running");
            anim.ResetTrigger("a_Walking");
        }
    }
}

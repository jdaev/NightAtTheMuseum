using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)){
            animator.SetInteger("HorizontalAxis",-1);
        }
        else if(Input.GetKey(KeyCode.D)){
            animator.SetInteger("HorizontalAxis",1);
        }
        else{
            animator.SetInteger("HorizontalAxis",0);
        }

        if(Input.GetKey(KeyCode.W)){
            animator.SetInteger("VerticalAxis",1);
        }
        else if(Input.GetKey(KeyCode.S)){
            animator.SetInteger("VerticalAxis",-1);
        }
        else{
            animator.SetInteger("VerticalAxis",0);
        }
        
    }
}

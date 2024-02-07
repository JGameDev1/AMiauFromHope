using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBehaviour : MonoBehaviour
{public bool right,left,walk,attack,idle;
float speedToUse,cronoAnim;
public float onCronoAnim,speed;
Rigidbody2D rb;
Animator _animator;
    
    void Start()
    {rb=GetComponent<Rigidbody2D>();
    _animator=GetComponent<Animator>();
    cronoAnim=onCronoAnim;
    speedToUse=speed;}

    void Update()
    {cronoAnim-=Time.deltaTime;
    if(cronoAnim<(-3f)){cronoAnim=onCronoAnim;}
    animations();}
    private void FixedUpdate()
    {MoveToDiferentsDirections();}

    private void OnCollisionEnter2D(Collision2D collision)
    {if(collision.gameObject.name=="PlayerActionMan"){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth=0;}
    if(collision.gameObject.tag=="Destructible"){collision.gameObject.SetActive(false);}
    if(collision.gameObject.tag=="Enemy"){collision.gameObject.SetActive(false);}}
    void MoveToDiferentsDirections()
    {if(right&&!left){rb.MovePosition(transform.position+Vector3.right*speedToUse*Time.fixedDeltaTime);}
    else if(left&&!right){rb.MovePosition(transform.position+Vector3.left*speedToUse*Time.fixedDeltaTime);}
    if(cronoAnim>0){walk=false;attack=true;idle=false;speedToUse=speed;}else{speedToUse=speed*2;}
    }

    void animations(){_animator.SetBool("walk",walk);_animator.SetBool("attack",attack);_animator.SetBool("idle",idle);}
}

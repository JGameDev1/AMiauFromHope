using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public bool right,left,up,down;

    private void Start()
    {rb=GetComponent<Rigidbody2D>();}

    private void Update()
    {if(right){rb.MovePosition(transform.position+Vector3.right*speed*Time.fixedDeltaTime);}
    else if(left){rb.MovePosition(transform.position+Vector3.left*speed*Time.fixedDeltaTime);}
    else if(up){rb.MovePosition(transform.position+Vector3.up*speed*Time.fixedDeltaTime);}
    else if(down){rb.MovePosition(transform.position+Vector3.down*speed*Time.fixedDeltaTime);}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{public float Speed,X,Y,PosLimitsX,NegLimitsX,PosLimitsY,NegLimitsY;
private Rigidbody2D rb;

private void Start()
{rb=GetComponent<Rigidbody2D>();}
void DirectionalLogic(){rb.MovePosition(transform.position+new Vector3(X,Y,0)*Time.deltaTime*Speed);
if(transform.position.x>PosLimitsX){X=-1;}else if(transform.position.x<NegLimitsX){X=1;}
if(transform.position.y>PosLimitsY){Y=-1;}else if(transform.position.y<NegLimitsY){Y=1;}}
void FixedUpdate()
{DirectionalLogic();}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagFlyerBehaviour : MonoBehaviour
{Rigidbody2D SkullRb;Animator _Animator; EnemyHealthManager _EnemyHealthManager;
public int XMovement,YMovement;public float PosLimitX,NegLimitX,NegLimitY,PosLimitY,Amplitud,Speed;
public bool Horizontal,forArt;
public GameObject Player;

private void Start()
{SkullRb = GetComponent<Rigidbody2D>();_Animator=GetComponent<Animator>();_EnemyHealthManager=GetComponent<EnemyHealthManager>();Player=GameObject.Find("PlayerActionMan");}
void Update()
{VolControl();_Animator.SetInteger("LIFE",_EnemyHealthManager.CurrentHealth);Movement();}
void Movement(){if(Horizontal){SkullRb.MovePosition(transform.position+new Vector3(XMovement,Mathf.Sin(transform.position.x)*Amplitud,transform.position.z).normalized*Speed/10);
if(transform.position.x<NegLimitX){XMovement=1;transform.position=new Vector3(NegLimitX,transform.position.y,transform.position.z);}else if(transform.position.x>PosLimitX){XMovement=-1;transform.position=new Vector3(PosLimitX,transform.position.y,transform.position.z);}
if(transform.position.y<NegLimitY){transform.position=new Vector3(transform.position.x,NegLimitY,transform.position.z);}else if(transform.position.y>PosLimitY){transform.position=new Vector3(transform.position.x,PosLimitY,transform.position.z);}}

else{SkullRb.MovePosition(transform.position+new Vector3(Mathf.Sin(transform.position.y)*Amplitud,YMovement,transform.position.z).normalized*Speed/10);
if(transform.position.x<NegLimitX){XMovement=1;transform.position=new Vector3(NegLimitX,transform.position.y,transform.position.z);}else if(transform.position.x>PosLimitX){XMovement=-1;transform.position=new Vector3(PosLimitX,transform.position.y,transform.position.z);}
if(transform.position.y<NegLimitY){YMovement=1;}else if(transform.position.y>PosLimitY){YMovement=-1;}
if(transform.position.y<NegLimitY-1){transform.position=new Vector3(transform.position.x,NegLimitY,transform.position.z);}else if(transform.position.y>PosLimitY+1){transform.position=new Vector3(transform.position.x,PosLimitY,transform.position.z);}}}
void VolControl(){if(Player!=null){float DistanciaDelJugadorX=transform.position.x-Player.transform.position.x,DistanciaDelJugadorY=transform.position.y-Player.transform.position.y;if(DistanciaDelJugadorX<0){DistanciaDelJugadorX=-DistanciaDelJugadorX;}if(DistanciaDelJugadorY<0){DistanciaDelJugadorY=-DistanciaDelJugadorY;}
if(DistanciaDelJugadorX>=100||DistanciaDelJugadorY>12){GetComponent<AudioSource>().volume=0;}else{GetComponent<AudioSource>().volume=(1/(DistanciaDelJugadorX/10));}}}
private void OnCollisionStay(Collision collision){if(collision.gameObject.tag=="Player"&&!forArt){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth--;}else if(collision.gameObject.tag=="Player"&&forArt){collision.gameObject.GetComponent<PlayerArtController>().CurrentHealth--;}}
}

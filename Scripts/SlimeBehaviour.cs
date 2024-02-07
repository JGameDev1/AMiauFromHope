using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour
{public Animator SlimeAnimator;
Rigidbody2D SlimeRigidbody;
public bool Move;
public float MovementSpeed,LimitsOfMovementX,NegLimitsOfMovementX,LimitsOfMovementY,NegLimitsOfMovementY,XDistanceToView,YdistanceToView;
GameObject Player;
public Vector2 LastPositionRegistred;

private void Start()
{SlimeAnimator=GetComponent<Animator>();
SlimeRigidbody=GetComponent<Rigidbody2D>();
Player=GameObject.Find("PlayerActionMan");
SlimeAnimator.Rebind();}

private void OnEnable()
{SlimeAnimator=GetComponent<Animator>();
SlimeRigidbody=GetComponent<Rigidbody2D>();
SlimeAnimator.Rebind();}
void DesactivateMe(){if(GameManager._SharedInstanceGameManager.Cinematica==true){this.gameObject.SetActive(false);}}
void DontCrossTheLimits() 
{if(transform.position.x>=LimitsOfMovementX){Move=false;transform.position=new Vector3(LimitsOfMovementX,transform.position.y,transform.position.z);}
if(transform.position.x<=NegLimitsOfMovementX){Move=false;transform.position=new Vector3(NegLimitsOfMovementX,transform.position.y,transform.position.z);}
if(transform.position.y>=LimitsOfMovementY){Move=false;transform.position=new Vector3(transform.position.x,LimitsOfMovementY,transform.position.z);}
if(transform.position.y<=NegLimitsOfMovementY){Move=false;transform.position=new Vector3(transform.position.x,NegLimitsOfMovementY,transform.position.z);}}

void Animations(){SlimeAnimator.SetInteger("LIFE",GetComponent<EnemyHealthManager>().CurrentHealth);SlimeAnimator.SetFloat("LastX",LastPositionRegistred.x);SlimeAnimator.SetBool("Move",Move);}
private void OnCollisionEnter2D(Collision2D collision)
{if(collision.gameObject.tag=="Enemy"&&collision.gameObject.name!="FlemaV(Clone)"){transform.localScale+=new Vector3(1,1,0);collision.gameObject.SetActive(false);GetComponent<EnemyHealthManager>().HealthValue+=2;GetComponent<EnemyHealthManager>().CurrentHealth=GetComponent<EnemyHealthManager>().HealthValue;}
if(collision.gameObject.tag == "Enemy" && collision.gameObject.name=="FlemaV(Clone)"){collision.gameObject.SetActive(false);GetComponent<EnemyHealthManager>().CurrentHealth++;}}

void VolControl(){if(Player!=null){float DistanciaDelJugadorX=transform.position.x-Player.transform.position.x,DistanciaDelJugadorY=transform.position.y-Player.transform.position.y;if(DistanciaDelJugadorX<0){DistanciaDelJugadorX=-DistanciaDelJugadorX;}if(DistanciaDelJugadorY<0){DistanciaDelJugadorY=-DistanciaDelJugadorY;}
if(DistanciaDelJugadorX>=100||DistanciaDelJugadorY>=10){GetComponent<AudioSource>().volume=0;}else{GetComponent<AudioSource>().volume=(1/(DistanciaDelJugadorX/10));}}}

void MovementConf()
{if(GetComponent<EnemyHealthManager>().CurrentHealth<=0){SlimeRigidbody.velocity=Vector2.zero*0;Move=false;}
if(transform.position.x-Player.transform.position.x<=XDistanceToView&&transform.position.y-Player.transform.position.y<YdistanceToView&&transform.position.y-Player.transform.position.y>=-YdistanceToView||transform.position.x-Player.transform.position.x<=XDistanceToView&&transform.position.y-Player.transform.position.y<YdistanceToView&&transform.position.y-Player.transform.position.y>=-YdistanceToView){LastPositionRegistred=new Vector2(Player.transform.position.x-transform.position.x,0).normalized;Move=true;}else{LastPositionRegistred=Vector2.zero;Move=false;}
SlimeRigidbody.velocity=LastPositionRegistred*MovementSpeed;}

private void OnCollisionStay2D(Collision2D collision)
{if(collision.gameObject.tag=="Player"){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth--;}}

private void FixedUpdate()
{if(Player!=null){MovementConf();}}
private void Update(){Animations();DontCrossTheLimits();VolControl();DesactivateMe();}


  
    
}

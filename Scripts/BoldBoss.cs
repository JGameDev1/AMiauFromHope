using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum Estados{Near,Distance,Idle,Death,OnlyWalk,Invulnerable}
public class BoldBoss : MonoBehaviour
{Rigidbody2D Rb;
AudioSource _AudioSource;public List<AudioClip>Sounds;
EnemyHealthManager _EnemyHealthManager;
Animator _Animator;
public List<GameObject>ProjectilesInGame;public List<GameObject>ObjectsToActive;public GameObject Baldoza,Slime,Player,WinZone;
public float MovementSpeed,NegLimitsOfMovementX,LimitsOfMovementX,NegLimitsOfMovementY,LimitsOfMovementY,OnMoveCrono,OnMeleeCrono1,OnPreparingCallCrono,OnMeleeCrono2,OnDistanceCrono1,OnDistanceCrono2,OnDistanceCrono3,OnJumpCrono,OnThinkingCrono;
float MeleeCrono1,MeleeCrono2,DistanceCrono1,DistanceCrono2,DistanceCrono3,MoveCrono,ThinkingCrono;
public CapsuleCollider2D BodyCol;
public int TypeOfAttack,IndexDestination;
public Estados MyStates;
public bool NearAttacking,DistanceAttacking,CheckedNumber,IsOnColumn,Dead,Jump,CallingReinforcement,IsMoving,IsRunning;
public Vector3 LastPositionRegistred;
public LayerMask Human,Ground;
public List<Transform>Destinations;
public GameObject Merchant;
public PlayableDirector DirectorScene;

void InstantiationOfProjectiles(){GameObject Rock=Instantiate(Baldoza,transform.position+LastPositionRegistred,Baldoza.transform.rotation,transform);
ProjectilesInGame.Add(Rock);
foreach(GameObject Things in ProjectilesInGame){Things.SetActive(false);}}

void ProjectilesComprobation(){if(ProjectilesInGame[0]==null){ProjectilesInGame.Add(Instantiate(Baldoza));}}

void MeleeAttacks(){if(MyStates==Estados.Near&&!CallingReinforcement){if(!CheckedNumber){TypeOfAttack=Random.Range(0,2);CheckedNumber=true;}
switch(TypeOfAttack)
{case 0:{MeleeCrono1-=Time.deltaTime;Rb.velocity=Vector2.zero;NearAttacking=true;IsRunning=false;if(MeleeCrono1<=0){MeleeCrono1=OnMeleeCrono1;MyStates=Estados.OnlyWalk;}break;}
case 1:{MeleeCrono2-=Time.deltaTime;Rb.velocity=Vector2.zero;NearAttacking=true;IsRunning=true;Rb.velocity=LastPositionRegistred*MovementSpeed*2*Time.fixedDeltaTime;if(MeleeCrono2<=0){MeleeCrono2=OnMeleeCrono2;MyStates=Estados.OnlyWalk;}}break;}
}}

void DistanceAttacks(){if(MyStates==Estados.Distance&&!CallingReinforcement){if(!CheckedNumber){TypeOfAttack=Random.Range(2,5);CheckedNumber=true;}
switch(TypeOfAttack)
{case 2:{DistanceCrono1-=Time.deltaTime;Rb.velocity=Vector2.zero;DistanceAttacking=true;if(DistanceCrono1<=0){MyStates=Estados.OnlyWalk;DistanceCrono1=OnDistanceCrono1;}break;}
case 3:{DistanceCrono2-=Time.deltaTime;Rb.velocity=Vector2.zero;DistanceAttacking=true;if(DistanceCrono2<=0){MyStates=Estados.OnlyWalk;DistanceCrono2=OnDistanceCrono2;}break;}
case 4:{DistanceCrono3-=Time.deltaTime;Rb.velocity=Vector2.zero;DistanceAttacking=true;if(DistanceCrono3<=0){Slime.transform.position=transform.position+LastPositionRegistred;Slime.SetActive(true);MyStates=Estados.OnlyWalk;DistanceCrono3=OnDistanceCrono3;}break;}
}}}


void DecidedHowToAttack(){float DistanciaDelJugadorX=transform.position.x-Player.transform.position.x;if(DistanciaDelJugadorX<0){DistanciaDelJugadorX=-DistanciaDelJugadorX;}if(MyStates==Estados.Idle&&!CallingReinforcement){
ThinkingCrono-=Time.deltaTime;IsMoving=false;IsRunning=false;CheckedNumber=false;NearAttacking=false;DistanceAttacking=false;CallingReinforcement=false;
if(ThinkingCrono<=0&&DistanciaDelJugadorX<=2f){ThinkingCrono=OnThinkingCrono;MyStates=Estados.Near;}
else if(ThinkingCrono<=0&&DistanciaDelJugadorX>=2f){ThinkingCrono=OnThinkingCrono;MyStates=Estados.Distance;}
}}


void OnlyWalk(){if(MyStates==Estados.OnlyWalk){IsMoving=true;IsRunning=false;CheckedNumber=false;NearAttacking=false;DistanceAttacking=false;MoveCrono-=Time.deltaTime;
if(MoveCrono>=0){Rb.velocity=LastPositionRegistred*Time.fixedDeltaTime*MovementSpeed;}
else if(MoveCrono<0){IsMoving=false;Rb.velocity=Vector3.down*Time.fixedDeltaTime*MovementSpeed;}
if(MoveCrono<=-2&&!IsMoving){MoveCrono=OnMoveCrono;MyStates=Estados.Idle;}
}}



private void OnCollisionEnter2D(Collision2D collision)
{if(collision.gameObject.tag=="Player"&&TypeOfAttack==2&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor>=0){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor-=30;}
else if(collision.gameObject.tag=="Player"&&TypeOfAttack==2&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor<=0){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=30;}
else if(collision.gameObject.tag=="Floor"||collision.gameObject.tag=="Destructible"){IndexDestination=Random.Range(0,Destinations.Count);}}

private void OnCollisionStay2D(Collision2D collision){if(collision.gameObject.tag=="Player"&&DirectorScene.enabled==false){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth--;}}

private void OnTriggerEnter2D(Collider2D collision)
{if(collision.gameObject.tag=="Player"&&TypeOfAttack==0&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor>0){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor-=100;}
else if(collision.gameObject.tag=="Player"&&TypeOfAttack==0&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor<=0){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=100;}
else if(collision.gameObject.tag=="Player"&&TypeOfAttack==1&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor>0){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor-=100;}
else if(collision.gameObject.tag=="Player"&&TypeOfAttack==1&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor<=0){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=100;}}

void DontCrossTheLimits()
{if(transform.position.x>=LimitsOfMovementX){transform.position=new Vector3(LimitsOfMovementX,transform.position.y,transform.position.z);}
if(transform.position.x<=NegLimitsOfMovementX){transform.position=new Vector3(NegLimitsOfMovementX,transform.position.y,transform.position.z);}
if(transform.position.y>=LimitsOfMovementY){transform.position=new Vector3(transform.position.x,LimitsOfMovementY,transform.position.z);}
if(transform.position.y<=NegLimitsOfMovementY){transform.position=new Vector3(transform.position.x,NegLimitsOfMovementY,transform.position.z);}}

void InfoForAnimations(){_Animator.SetFloat("LastPositionRegistred",LastPositionRegistred.x);_Animator.SetBool("NearAttacking",NearAttacking);_Animator.SetBool("IsMoving",IsMoving);_Animator.SetBool("IsRunning",IsRunning);
_Animator.SetBool("DistanceAttacking",DistanceAttacking);_Animator.SetInteger("TypeOfAttack",TypeOfAttack);_Animator.SetBool("Dead",Dead);_Animator.SetInteger("LIFE",_EnemyHealthManager.CurrentHealth);}

private void OnEnable(){WinZone=GameObject.Find("WinZone");Merchant.SetActive(false);WinZone.SetActive(false);foreach(var Objects in ObjectsToActive){Objects.SetActive(true);};}

private void OnDisable(){WinZone.SetActive(true);GameManager._SharedInstanceGameManager.Cinematica=true;}

private void Start()
{Player=GameObject.Find("PlayerActionMan");Rb=GetComponent<Rigidbody2D>();_AudioSource=GetComponent<AudioSource>();_EnemyHealthManager=GetComponent<EnemyHealthManager>();_Animator=GetComponent<Animator>();
InstantiationOfProjectiles();MoveCrono=OnMoveCrono;MeleeCrono1=OnMeleeCrono1;MeleeCrono2=OnMeleeCrono2;DistanceCrono1=OnDistanceCrono1;DistanceCrono2=OnDistanceCrono2;
ThinkingCrono=OnThinkingCrono;}

private void Update(){if(_EnemyHealthManager.CurrentHealth>0){if(Player.transform.position.x<transform.position.x){LastPositionRegistred.x=-1;}else{LastPositionRegistred.x=1;}
DontCrossTheLimits();InfoForAnimations();DecidedHowToAttack();ProjectilesComprobation();}
else if(_EnemyHealthManager.CurrentHealth<=0){MyStates=Estados.Death;InfoForAnimations();DirectorScene.enabled=true;GameManager._SharedInstanceGameManager.Cinematica=true;}
}

private void FixedUpdate()
{if(_EnemyHealthManager.CurrentHealth>0){DistanceAttacks();MeleeAttacks();OnlyWalk();}else{}}

}
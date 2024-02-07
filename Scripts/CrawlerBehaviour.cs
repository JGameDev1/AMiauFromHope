using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States{Attack,Super,Stop,Death,OnGuard}
public class CrawlerBehaviour : MonoBehaviour
{SpriteRenderer Imagen;
Rigidbody2D Rb;
AudioSource _AudioSource;public List<AudioClip>Sounds;
EnemyHealthManager _EnemyHealthManager;
Animator _Animator;
CapsuleCollider2D Body;
public List<GameObject>ProjectilesInGame;public List<GameObject>ObjectsToActive;public GameObject Gp,Yp,Rp,YRain,Player,WinZone;
public float GuardCrono,MovementSpeed,NegLimitsOfMovementX,LimitsOfMovementX,NegLimitsOfMovementY,LimitsOfMovementY,OnMoveCrono,OnMeleeCrono1,OnMeleeCrono2,OnMeleeCrono3,OnSuperCrono1,OnSuperCrono2,OnSuperCrono3,OnGuardCrono;
float MeleeCrono1,MeleeCrono2,MeleeCrono3,SuperCrono1,SuperCrono2,SuperCrono3,MoveCrono;
public int TypeOfMeleeAttack,TypeOfSuperAttack,IndexDestination;
public States MyStates;
public bool IsOnGuard,IsAttacking,IsMoving,IsPreparing,IsSuper,CheckedNumber,IsFlying,Dead;
public Vector3 LastPositionRegistred;
public LayerMask Human,Ground;
public List<Transform>Destinations;
public GameObject AcechadorFinalDeathImageL,AcechadorFinalDeathImageR,Merchant;

void InstantiationOfProjectiles(){GameObject GreenProjectile=Instantiate(Gp,transform.position+LastPositionRegistred,Gp.transform.rotation,transform);
GameObject YellowProjectile=Instantiate(Yp,transform.position+LastPositionRegistred+Vector3.up,Yp.transform.rotation,transform);
GameObject RedProjectile=Instantiate(Rp,transform.position+LastPositionRegistred+Vector3.up,Yp.transform.rotation,transform);
ProjectilesInGame.Add(GreenProjectile);ProjectilesInGame.Add(YellowProjectile);ProjectilesInGame.Add(RedProjectile);
GameObject Rain=Instantiate(YRain,transform.position+LastPositionRegistred,YRain.transform.rotation);ProjectilesInGame.Add(Rain);
foreach(GameObject Spits in ProjectilesInGame){Spits.SetActive(false);}}

void ProjectilesComprobation(){if(ProjectilesInGame[0]==null){ProjectilesInGame.Add(Instantiate(Gp));}
if(ProjectilesInGame[1]==null){ProjectilesInGame.Add(Instantiate(Yp));}
if(ProjectilesInGame[2]==null){ProjectilesInGame.Add(Instantiate(Rp));}
if(ProjectilesInGame[3]==null){ProjectilesInGame.Add(Instantiate(YRain));}
}

void MeleeAttacks(){if(MyStates==States.Attack){IsOnGuard=false;if(!CheckedNumber){TypeOfMeleeAttack=Random.Range(0,3);CheckedNumber=true;}
switch(TypeOfMeleeAttack)
{case 0:{MeleeCrono1-=Time.deltaTime;if(MeleeCrono1<=0){IsAttacking=true;if(!_AudioSource.isPlaying){_AudioSource.clip=Sounds[0];_AudioSource.PlayOneShot(_AudioSource.clip);}if(MeleeCrono1<=-2){MeleeCrono1=OnMeleeCrono1;MyStates=States.Stop;}}break;}
case 1:{MeleeCrono2-=Time.deltaTime;IsPreparing=true;if(MeleeCrono2<=0){IsAttacking=true;if(!_AudioSource.isPlaying){_AudioSource.clip=Sounds[1];_AudioSource.PlayOneShot(_AudioSource.clip);}if(MeleeCrono2<=-1){MeleeCrono2=OnMeleeCrono2;MyStates=States.Stop;}}break;}
case 2:{if(!IsFlying){IndexDestination=Random.Range(0,Destinations.Count);}MeleeCrono3-=Time.deltaTime;IsPreparing=true;if(!_AudioSource.isPlaying){_AudioSource.clip=Sounds[2];_AudioSource.PlayOneShot(_AudioSource.clip);}if(MeleeCrono3<=0){IsFlying=true;Rb.MovePosition(this.transform.position+(Destinations[IndexDestination].position-transform.position)*0.035f);
if(transform.position.x>=Destinations[3].position.x-3){IndexDestination=Random.Range(0,Destinations.Count);}else
if(transform.position.x<=Destinations[0].position.x+3){IndexDestination=Random.Range(0,Destinations.Count);}
if(MeleeCrono3<=-10){MeleeCrono3=OnMeleeCrono3;MyStates=States.OnGuard;}}break;}}}
}

void SuperAttacks(){if(MyStates==States.Super){IsOnGuard=false;if(!CheckedNumber){TypeOfSuperAttack=Random.Range(0,3);CheckedNumber=true;}
switch(TypeOfSuperAttack)
{case 0:{SuperCrono1-=Time.deltaTime;IsPreparing=true;Imagen.color=new Color(0,1,0);if(SuperCrono1<=0){ProjectilesInGame[0].SetActive(true);if(!_AudioSource.isPlaying){_AudioSource.clip=Sounds[3];_AudioSource.PlayOneShot(_AudioSource.clip);}}
if(SuperCrono1<=-1){IsAttacking=false;IsPreparing=false;SuperCrono1=OnSuperCrono1;MyStates=States.Stop;}break;}
case 1:{SuperCrono2-=Time.deltaTime;IsSuper=true;Imagen.color=new Color(1,1,0);if(SuperCrono2<=0){ProjectilesInGame[1].SetActive(true);if(!_AudioSource.isPlaying){_AudioSource.clip=Sounds[3];_AudioSource.PlayOneShot(_AudioSource.clip);}}
if(SuperCrono2<=-2){IsAttacking=false;IsPreparing=false;SuperCrono2=OnSuperCrono2;MyStates=States.Stop;ProjectilesInGame[3].SetActive(true);if(!_AudioSource.isPlaying){_AudioSource.clip=Sounds[3];_AudioSource.PlayOneShot(_AudioSource.clip);}}break;}
case 2:{SuperCrono3-=Time.deltaTime;IsSuper=true;Imagen.color=new Color(1,0,0);if(SuperCrono3<=0){ProjectilesInGame[2].SetActive(true);if(!_AudioSource.isPlaying){_AudioSource.clip=Sounds[3];_AudioSource.PlayOneShot(_AudioSource.clip);}}
if(SuperCrono3<=-2){IsAttacking=false;IsPreparing=false;SuperCrono3=OnSuperCrono3;MyStates=States.Stop;}break;}
}}
}
private void OnCollisionEnter2D(Collision2D collision)
{if(collision.gameObject.tag=="Player"&&TypeOfMeleeAttack==2&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor>0){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor-=30;Body.isTrigger=true;}
else if(collision.gameObject.tag=="Player"&&TypeOfMeleeAttack==2&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor<=0){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=30;Body.isTrigger=true;}
else if(collision.gameObject.tag=="Floor"||collision.gameObject.tag=="Destructible"){IndexDestination=Random.Range(0,Destinations.Count);}}

private void OnCollisionStay2D(Collision2D collision){if(collision.gameObject.tag=="Player"){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth--;}}

private void OnTriggerEnter2D(Collider2D collision)
{if(collision.gameObject.tag=="Player"&&TypeOfMeleeAttack==0&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor>=0){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor-=100;}
else if(collision.gameObject.tag=="Player"&&TypeOfMeleeAttack==0&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor<=0){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=100;}
else if(collision.gameObject.tag=="Player"&&TypeOfMeleeAttack==1&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor>=0){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor-=50;}
else if(collision.gameObject.tag=="Player"&&TypeOfMeleeAttack==1&&collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor<=0){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=50;}}

void StopActions(){if(MyStates==States.Stop){Body.isTrigger=false;CheckedNumber=false;IsFlying=false;IsMoving=false;IsSuper=false;IsAttacking=false;IsOnGuard=false;IsPreparing=false;transform.position=transform.position;Imagen.color=new Color(1,1,1);
MoveCrono-=Time.deltaTime;if(MoveCrono>0&&MyStates==States.Stop){IsMoving=true;if(Physics2D.Raycast(transform.position-new Vector3(0,2.16f,0),LastPositionRegistred,1f,Ground.value)){Rb.velocity=Vector3.up*MovementSpeed;}
else if(!Physics2D.Raycast(transform.position-new Vector3(0,2.14f,0),LastPositionRegistred,1f,Ground.value)){Rb.velocity=LastPositionRegistred*MovementSpeed+Vector3.down*3;}
else if(MoveCrono<0&&MyStates==States.Stop){IsMoving=false;Rb.velocity=Vector2.down*3;}}
if(MoveCrono<-1){;MyStates=States.OnGuard;}}}

void OnDeath(){if(MyStates==States.Death){Debug.Log("EntroOnDead");Dead=true;Rb.velocity=Vector3.down*600*Time.fixedDeltaTime;NegLimitsOfMovementY=-2;transform.localScale=new Vector3(2,2,1);}}

void OnGuard(){float DistanciaDelJugadorX=transform.position.x-Player.transform.position.x;if(DistanciaDelJugadorX<0){DistanciaDelJugadorX=-DistanciaDelJugadorX;}
if(MyStates==States.OnGuard){Body.isTrigger=false;MoveCrono=OnMoveCrono;GuardCrono-=Time.deltaTime;IsOnGuard=true;IsPreparing=false;IsAttacking=false;IsSuper=false;
if(GuardCrono<=0&&DistanciaDelJugadorX<=2f){GuardCrono=OnGuardCrono;IsOnGuard=false;MyStates=States.Attack;}
else if(GuardCrono<=0&&DistanciaDelJugadorX>=2f){GuardCrono=OnGuardCrono;IsOnGuard=false;MyStates=States.Super;}}}

void DontCrossTheLimits()
{if(transform.position.x>=LimitsOfMovementX){transform.position=new Vector3(LimitsOfMovementX,transform.position.y,transform.position.z);}
if(transform.position.x<=NegLimitsOfMovementX){transform.position=new Vector3(NegLimitsOfMovementX,transform.position.y,transform.position.z);}
if(transform.position.y>=LimitsOfMovementY){transform.position=new Vector3(transform.position.x,LimitsOfMovementY,transform.position.z);}
if(transform.position.y<=NegLimitsOfMovementY){transform.position=new Vector3(transform.position.x,NegLimitsOfMovementY,transform.position.z);}}

void InfoForAnimations(){_Animator.SetBool("IsOnGuard",IsOnGuard);_Animator.SetBool("IsAttacking",IsAttacking);_Animator.SetBool("IsSuper",IsSuper);
_Animator.SetFloat("LastPositionRegistred",LastPositionRegistred.x);
_Animator.SetBool("IsPreparing",IsPreparing);_Animator.SetBool("IsMoving",IsMoving);_Animator.SetBool("IsFlying",IsFlying);_Animator.SetInteger("TypeOfMeleeAttack",TypeOfMeleeAttack);
_Animator.SetBool("Dead",Dead);_Animator.SetInteger("TypeOfSuperAttack",TypeOfSuperAttack);_Animator.SetInteger("LIFE",_EnemyHealthManager.CurrentHealth);}

private void OnEnable(){Merchant.SetActive(false);WinZone.SetActive(false);foreach(var Objects in ObjectsToActive){Objects.SetActive(true);};}

private void OnDisable(){WinZone.SetActive(true);if(LastPositionRegistred.x<0){Instantiate(AcechadorFinalDeathImageL,transform.position-new Vector3(0,0.1f,0),transform.rotation);}else if(LastPositionRegistred.x>0){Instantiate(AcechadorFinalDeathImageR,transform.position-new Vector3(0,0.1f,0),transform.rotation);}}

private void Start()
{Player=GameObject.Find("PlayerActionMan");Imagen=GetComponent<SpriteRenderer>();Rb=GetComponent<Rigidbody2D>();Body=GetComponent<CapsuleCollider2D>();_AudioSource=GetComponent<AudioSource>();_EnemyHealthManager=GetComponent<EnemyHealthManager>();_Animator=GetComponent<Animator>();
InstantiationOfProjectiles();MoveCrono=OnMoveCrono;MeleeCrono1=OnMeleeCrono1;MeleeCrono2=OnMeleeCrono2;MeleeCrono3=OnMeleeCrono3;SuperCrono1=OnMeleeCrono1;SuperCrono2=OnSuperCrono2;SuperCrono3=OnSuperCrono3;
GuardCrono=OnGuardCrono;}

private void Update(){if(_EnemyHealthManager.CurrentHealth>0){if(Player.transform.position.x<transform.position.x){LastPositionRegistred.x=-1;}else{LastPositionRegistred.x=1;}
DontCrossTheLimits();
InfoForAnimations();ProjectilesComprobation();
MeleeAttacks();SuperAttacks();OnGuard();}else if(_EnemyHealthManager.CurrentHealth<=0){MyStates=States.Death;OnDeath();InfoForAnimations();}}

private void FixedUpdate(){if(!Dead){StopActions();}}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public enum PlayerStates{Attack,Super,Stop};
public class PlayerArtController : MonoBehaviour
{Rigidbody2D PlayerBody;AudioSource _PlayerAudioSource;public List<AudioClip>SuperEffects;public List<AudioClip>PunchEffects;
public float MovementSpeed,SuperSpeed,JumpForce,JumpFromGround,HorizontalInput,VerticalInput,JumpInput,LimitsOfMovementX,NegLimitsOfMovementX,LimitsOfMovementY,NegLimitsOfMovementY,OnAttackCronometre,OnSuperCronometre,AttackCronometre,SuperCronometre;
Animator _Animator;
public LayerMask Ground;
public Vector3 LastMovement=Vector3.zero;
public List<AudioClip>SoundsClips;
public string PunchKey,KickKey;
public bool IsDeath,Idle,IsJumping,SuperPunch;private bool Joystick,PCGame,IsPunching,IsKicking;
public Joystick joystick;
public PlayerUIArt _PlayerUIArt;
public int HealthValue,ArmorValue;
public int CurrentHealth,CurrentArmor,SuperCharge;
public PlayerStates State;
public HitImpact HitImpCod;
private AudioSource MusicLanguajeManagerAudioSource;
PlayableDirector Director;

private void OnEnable(){CurrentHealth=HealthValue;_Animator.Rebind();}

private void Awake(){MusicLanguajeManagerAudioSource=GameObject.Find("Music&LanguageManager").GetComponent<AudioSource>();PlayerBody=GetComponent<Rigidbody2D>();_PlayerAudioSource=GetComponent<AudioSource>();_Animator=GetComponent<Animator>();_PlayerUIArt=FindObjectOfType<PlayerUIArt>();Joystick=FindObjectOfType<MusicLanguajeManager>().UseJoystick;PCGame=FindObjectOfType<MusicLanguajeManager>().PCGame;Director=GameObject.FindObjectOfType<PlayableDirector>();}
void Start(){MusicLanguajeManagerAudioSource.volume=0.4f;State=PlayerStates.Stop;SuperCronometre=OnSuperCronometre;AttackCronometre=OnAttackCronometre;CurrentHealth=HealthValue;CurrentArmor=ArmorValue;CeroButton();Idle=true;IsJumping=false;IsDeath=false;LastMovement=new Vector3(1,0,0);}

void Movement()
{if(LastMovement.x<0){transform.rotation=Quaternion.Euler(0,180,0);}else{transform.rotation=Quaternion.Euler(0,0,0);}
if(PCGame){HorizontalInput=Input.GetAxisRaw("Horizontal")*MovementSpeed;VerticalInput=Input.GetAxisRaw("Vertical");if(HorizontalInput>0){Idle=false;LastMovement=new Vector3(1,LastMovement.y,LastMovement.z);}else if(HorizontalInput<0){Idle =false;LastMovement=new Vector3(-1,LastMovement.y,LastMovement.z);}
if(HorizontalInput==0){Idle =true;}
if(VerticalInput>0){LastMovement=new Vector3(LastMovement.x,1,LastMovement.z);}else if(VerticalInput<0){LastMovement=new Vector3(LastMovement.x,-1,LastMovement.z);}else if(VerticalInput==0){LastMovement=new Vector3(LastMovement.x,0,LastMovement.z);}
PlayerBody.velocity=new Vector2(HorizontalInput,PlayerBody.velocity.y);
JumpInput=Input.GetAxisRaw("Jump")*Time.fixedDeltaTime*JumpForce;
if(Physics2D.Raycast(transform.position,Vector2.down,JumpFromGround,Ground.value)&&JumpInput>0){PlayerBody.AddForce(Vector2.up*JumpInput,ForceMode2D.Impulse);IsJumping=true;_Animator.SetFloat("JumpInput",Input.GetAxisRaw("Jump"));}}}

void MeleeAttacks(){if(Input.GetKey(PunchKey)&&State==PlayerStates.Stop){State=PlayerStates.Attack;IsPunching=true;IsKicking=false;if(!_PlayerAudioSource.isPlaying){_PlayerAudioSource.PlayOneShot(PunchEffects[Random.Range(0,PunchEffects.Count)]);}}else if(Input.GetKey(PunchKey)&&State==PlayerStates.Super){IsPunching=false;IsKicking=false;SuperPunch=true;MovementSpeed=SuperSpeed;if(!_PlayerAudioSource.isPlaying){_PlayerAudioSource.PlayOneShot(SuperEffects[Random.Range(0,SuperEffects.Count)]);}}
if(Input.GetKey(KickKey)&&State==PlayerStates.Stop){State=PlayerStates.Attack;IsKicking=true;IsPunching=false;}
}

void AttacksCronometresFunction(){if(State==PlayerStates.Attack){AttackCronometre-=Time.deltaTime;}
if(SuperPunch){SuperCronometre-=Time.deltaTime;}
if(AttackCronometre<=0){State=PlayerStates.Stop;AttackCronometre=OnAttackCronometre;IsPunching=false;IsKicking=false;SuperPunch=false;}
if(SuperCronometre<=0){State=PlayerStates.Stop;AttackCronometre=OnAttackCronometre;IsPunching=false;IsKicking=false;SuperPunch=false;SuperCharge=0;SuperCronometre=OnSuperCronometre;MovementSpeed=8;}
}

void ForSuper(){if(SuperCharge>=10){State=PlayerStates.Super;HitImpCod.ImpactDamage=10;}else{HitImpCod.ImpactDamage=1;}}

void DontCrossTheLimits()
{if(transform.position.x>=LimitsOfMovementX){transform.position=new Vector3(LimitsOfMovementX,transform.position.y,transform.position.z);}
if(transform.position.x<=NegLimitsOfMovementX){transform.position=new Vector3(NegLimitsOfMovementX,transform.position.y,transform.position.z);}
if(transform.position.y>=LimitsOfMovementY){transform.position=new Vector3(transform.position.x,LimitsOfMovementY,transform.position.z);}
if(transform.position.y<=NegLimitsOfMovementY){transform.position=new Vector3(transform.position.x,NegLimitsOfMovementY,transform.position.z);}}
//--------------------------------------------------------------CODIGOS PARA BOTONES-------------------------
public void PointRightUpButton(){if(!Director.isActiveAndEnabled){LastMovement=new Vector3(1,1,LastMovement.z);Idle=false;IsJumping=false;PlayerBody.velocity=new Vector2(PlayerBody.velocity.x,PlayerBody.velocity.y);_Animator.SetBool("Idle",Idle);_Animator.SetBool("IsJumping",IsJumping);}}
public void PointRightDownButton(){if(!Director.isActiveAndEnabled){LastMovement=new Vector3(1,-1,LastMovement.z);Idle=false;IsJumping=false;PlayerBody.velocity=new Vector2(PlayerBody.velocity.x,PlayerBody.velocity.y);_Animator.SetBool("Idle",Idle);_Animator.SetBool("IsJumping",IsJumping);}}
public void MoveToRightButton(){if(!Director.isActiveAndEnabled){LastMovement=new Vector3(1,0,LastMovement.z);Idle=false;IsJumping=false;PlayerBody.velocity=new Vector2(MovementSpeed,PlayerBody.velocity.y);_Animator.SetBool("Idle",Idle);_Animator.SetBool("IsJumping",IsJumping);}}
public void CeroButton(){if(!Director.isActiveAndEnabled){LastMovement=new Vector3(0,0,0);Idle=true;IsJumping=false;PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);_Animator.SetBool("Idle",Idle);_Animator.SetBool("IsJumping",IsJumping);}}
public void MoveToLeftButton(){if(!Director.isActiveAndEnabled){LastMovement=new Vector3(-1,0,LastMovement.z);Idle=false;IsJumping=false;PlayerBody.velocity=new Vector2(-MovementSpeed,PlayerBody.velocity.y);_Animator.SetFloat("HorizontalInput",PlayerBody.velocity.x);_Animator.SetBool("Idle",Idle);_Animator.SetBool("IsJumping",IsJumping);}}
public void PointLeftUpButton(){if(!Director.isActiveAndEnabled){LastMovement=new Vector3(-1,1,LastMovement.z);Idle=false;IsJumping=false;PlayerBody.velocity=new Vector2(PlayerBody.velocity.x,PlayerBody.velocity.y);_Animator.SetBool("Idle",Idle);_Animator.SetBool("IsJumping",IsJumping);}}
public void PointLeftDownButton(){if(!Director.isActiveAndEnabled){LastMovement=new Vector3(-1,-1,LastMovement.z);Idle=false;IsJumping=false;PlayerBody.velocity=new Vector2(PlayerBody.velocity.x,PlayerBody.velocity.y);_Animator.SetBool("Idle",Idle);_Animator.SetBool("IsJumping",IsJumping);}}
public void JumpButton(){if(Physics2D.Raycast(transform.position,Vector2.down,JumpFromGround,Ground.value)&&!Director.isActiveAndEnabled){PlayerBody.AddForce(Vector2.up*(JumpForce+220)*Time.fixedDeltaTime,ForceMode2D.Impulse);if(transform.position.y>-4.532){IsJumping=true;Idle =false;_Animator.SetBool("IsJumping",IsJumping);_Animator.SetBool("Idle",Idle);}else{IsJumping=false;Idle=true;_Animator.SetBool("Idle",Idle);}}}
public void PunchButton(){if(State==PlayerStates.Stop&&!Director.isActiveAndEnabled){State=PlayerStates.Attack;IsPunching=true;IsKicking=false;if(!_PlayerAudioSource.isPlaying){_PlayerAudioSource.PlayOneShot(PunchEffects[Random.Range(0,PunchEffects.Count)]);}}else if(State==PlayerStates.Super){IsPunching=false;IsKicking=false;SuperPunch=true;MovementSpeed=SuperSpeed;if(!_PlayerAudioSource.isPlaying){_PlayerAudioSource.PlayOneShot(SuperEffects[Random.Range(0,SuperEffects.Count)]);}}}
public void KickButton(){if(State==PlayerStates.Stop&&!Director.isActiveAndEnabled){State=PlayerStates.Attack;IsKicking=true;IsPunching=false;}}
//-------------------------------------------------------------CODIGOS PARA JOYSTICK------------------------
void JoystickMovement()
{if(Joystick){Vector2 JoystickInput=joystick.Vertical*Vector2.up+joystick.Horizontal*Vector2.right;
PlayerBody.velocity=new Vector2(JoystickInput.x*MovementSpeed,PlayerBody.velocity.y);
if(JoystickInput.x>0){Idle=false;LastMovement=new Vector3(1,LastMovement.y,LastMovement.z);}else if(JoystickInput.x<0){Idle =false;LastMovement = new Vector3(-1, LastMovement.y, LastMovement.z); }
if(JoystickInput.x==0){Idle=true;}
if(JoystickInput.y>0.4){LastMovement=new Vector3(LastMovement.x,1,LastMovement.z);}else if(JoystickInput.y<-0.4){LastMovement = new Vector3(LastMovement.x, -1, LastMovement.z);}else if(JoystickInput.y==0){LastMovement=new Vector3(LastMovement.x,0,LastMovement.z);}}}
//----------------------------------------------------------------------------------------------------------
void MoveAnimation(){_Animator.SetBool("Idle",Idle);_Animator.SetFloat("JumpInput",Input.GetAxisRaw("Jump"));_Animator.SetBool("IsJumping",IsJumping);_Animator.SetBool("IsDeath",IsDeath);_Animator.SetBool("SuperPunch",SuperPunch);_Animator.SetBool("IsKicking",IsKicking);_Animator.SetBool("IsPunching",IsPunching);}

void OnCollisionEnter2D(Collision2D collision){if(collision.gameObject.tag=="Floor"){IsJumping=false;}}
private void OnCollisionExit(Collision collision){if(collision.gameObject.tag=="Floor"){IsJumping=true;}}

void Update()
{ForSuper();MeleeAttacks();AttacksCronometresFunction();
DontCrossTheLimits();MoveAnimation();
if(CurrentArmor<=0){CurrentArmor=0;}
if(CurrentHealth>HealthValue){CurrentHealth=HealthValue;}
if(CurrentHealth<=0){IsDeath=true;GameManager._SharedInstanceGameManager.GameOver();gameObject.SetActive(false);}
else{IsDeath=false;}}
void FixedUpdate(){if(Director.enabled==false){JoystickMovement();Movement();}else{}}
}

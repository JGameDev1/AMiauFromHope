using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerControllerWMW2D : MonoBehaviour
{Rigidbody2D PlayerBody;AudioSource _PlayerAudioSource;
public float MovementSpeed,NewMovementSpeed,JumpForce,JumpFromGround,HorizontalInput,VerticalInput,JumpInput,LimitsOfMovementX,NegLimitsOfMovementX,LimitsOfMovementY,NegLimitsOfMovementY,NextFire,FireRate,TimeValue,OnHandGunShotCrono,OnShotgunShotCrono,OnUziShotCrono,OnAxeCrono;private float HandGunShotCrono,ShotgunShotCrono,UziShotCrono,AxeCrono;
Animator _Animator;
public LayerMask Ground;
public Vector3 LastMovement=Vector3.zero;
public List<GameObject>HSProjectiles;public List<GameObject>MSProjectiles;public int WeaponID;public List<AudioClip>SoundsClips;
public GameObject PistolShot,ShotgunShot,MachineGunShot;
public string ShootKey;
public bool IsDeath,Iddle,IsJumping,InDanger,IsInteracting;private bool Joystick,PCGame,AxeAttackAnim,HandgunShotAnim,ShotgunShotAnim,UziShotAnim;
public Joystick joystick;
public PlayerUI _PlayerUI;
public int HealthValue,ArmorValue;
public int CurrentHealth,CurrentArmor;
public int AmmoForUzi,AmmoForShotgun,AmmoForPistol;
private Canvas Dialoguecanvas;public List<Image>Dialogues;public int DialogN;
public List<AudioClip>Quejidos;
private AudioSource MusicLanguajeManagerAudioSource;
private PlayableDirector Director;

private void OnEnable(){CurrentHealth=HealthValue;}

private void Awake(){MusicLanguajeManagerAudioSource=GameObject.Find("Music&LanguageManager").GetComponent<AudioSource>();AxeCrono=OnAxeCrono;HandGunShotCrono=OnHandGunShotCrono;ShotgunShotCrono=OnShotgunShotCrono;UziShotCrono=OnUziShotCrono;PlayerBody=GetComponent<Rigidbody2D>();_PlayerAudioSource=GetComponent<AudioSource>();_Animator=GetComponent<Animator>();_PlayerUI=FindObjectOfType<PlayerUI>();NextFire=0;Joystick=FindObjectOfType<MusicLanguajeManager>().UseJoystick;PCGame=FindObjectOfType<MusicLanguajeManager>().PCGame;Director=FindObjectOfType<PlayableDirector>();}
void Start(){MusicLanguajeManagerAudioSource.volume=1f;Dialoguecanvas=GameObject.Find("DialogueCanvas").GetComponent<Canvas>();CurrentHealth=HealthValue;CeroButton();CreationOfBullets();Iddle=true;IsJumping=false;IsDeath=false;LastMovement=new Vector3(1,0,0);}

void Movement()
{if(PCGame){HorizontalInput=Input.GetAxisRaw("Horizontal")*MovementSpeed;VerticalInput=Input.GetAxisRaw("Vertical");if(HorizontalInput>0){Iddle=false;LastMovement=new Vector3(1,LastMovement.y,LastMovement.z);}else if(HorizontalInput<0){Iddle=false;LastMovement=new Vector3(-1,LastMovement.y,LastMovement.z);}
if(HorizontalInput==0){Iddle=true;}
if(VerticalInput>0){LastMovement=new Vector3(LastMovement.x,1,LastMovement.z);}else if(VerticalInput<0){LastMovement=new Vector3(LastMovement.x,-1,LastMovement.z);}else if(VerticalInput==0){LastMovement=new Vector3(LastMovement.x,0,LastMovement.z);}
PlayerBody.velocity=new Vector2(HorizontalInput,PlayerBody.velocity.y);
JumpInput=Input.GetAxisRaw("Jump")*Time.fixedDeltaTime*JumpForce;
if(Physics2D.Raycast(transform.position,Vector2.down,JumpFromGround,Ground.value)&&JumpInput>0){PlayerBody.AddForce(Vector2.up*JumpInput,ForceMode2D.Impulse);IsJumping=true;_Animator.SetFloat("JumpInput",Input.GetAxisRaw("Jump"));}}}

void DontCrossTheLimits()
{if(transform.position.x>=LimitsOfMovementX){transform.position=new Vector3(LimitsOfMovementX,transform.position.y,transform.position.z);}
if(transform.position.x<=NegLimitsOfMovementX){transform.position=new Vector3(NegLimitsOfMovementX,transform.position.y,transform.position.z);}
if(transform.position.y>=LimitsOfMovementY){transform.position=new Vector3(transform.position.x,LimitsOfMovementY,transform.position.z);}
if(transform.position.y<=NegLimitsOfMovementY){transform.position=new Vector3(transform.position.x,NegLimitsOfMovementY,transform.position.z);}}
//--------------------------------------------------------------CODIGOS PARA BOTONES-------------------------
public void PointRightUpButton(){if(!Director.isActiveAndEnabled){LastMovement=new Vector3(1,1,LastMovement.z);Iddle=false;IsJumping=false;PlayerBody.velocity=new Vector2(PlayerBody.velocity.x,PlayerBody.velocity.y);_Animator.SetFloat("HorizontalInput",PlayerBody.velocity.x);_Animator.SetFloat("LastX",LastMovement.x);_Animator.SetBool("Iddle",Iddle);_Animator.SetBool("IsJumping",IsJumping);_Animator.SetInteger("WeaponID",WeaponID);}}
public void PointRightDownButton(){if(!Director.isActiveAndEnabled){LastMovement=new Vector3(1,-1,LastMovement.z);Iddle=false;IsJumping=false;PlayerBody.velocity=new Vector2(PlayerBody.velocity.x,PlayerBody.velocity.y);_Animator.SetFloat("HorizontalInput",PlayerBody.velocity.x);_Animator.SetFloat("LastX",LastMovement.x);_Animator.SetBool("Iddle",Iddle);_Animator.SetBool("IsJumping",IsJumping);_Animator.SetInteger("WeaponID",WeaponID);}}
public void MoveToRightButton(){if(!Director.isActiveAndEnabled){LastMovement=new Vector3(1,0,LastMovement.z);Iddle=false;IsJumping=false;PlayerBody.velocity=new Vector2(MovementSpeed,PlayerBody.velocity.y);_Animator.SetFloat("HorizontalInput",PlayerBody.velocity.x);_Animator.SetFloat("LastX",LastMovement.x);_Animator.SetBool("Iddle",Iddle);_Animator.SetBool("IsJumping",IsJumping);_Animator.SetInteger("WeaponID",WeaponID);}}
public void CeroButton(){if(!Director.isActiveAndEnabled){LastMovement=new Vector3(0,0,0);Iddle=true;IsJumping=false;PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);_Animator.SetFloat("HorizontalInput",PlayerBody.velocity.x);_Animator.SetFloat("LastX",LastMovement.x);_Animator.SetBool("Iddle",Iddle);_Animator.SetBool("IsJumping",IsJumping);_Animator.SetInteger("WeaponID",WeaponID);}}
public void MoveToLeftButton(){if(!Director.isActiveAndEnabled){LastMovement=new Vector3(-1,0,LastMovement.z);Iddle=false;IsJumping=false;PlayerBody.velocity=new Vector2(-MovementSpeed,PlayerBody.velocity.y);_Animator.SetFloat("HorizontalInput",PlayerBody.velocity.x);_Animator.SetFloat("LastX",LastMovement.x);_Animator.SetBool("Iddle",Iddle);_Animator.SetBool("IsJumping",IsJumping);_Animator.SetInteger("WeaponID",WeaponID);}}
public void PointLeftUpButton(){if(!Director.isActiveAndEnabled){LastMovement=new Vector3(-1,1,LastMovement.z);Iddle=false;IsJumping=false;PlayerBody.velocity=new Vector2(PlayerBody.velocity.x,PlayerBody.velocity.y);_Animator.SetFloat("HorizontalInput",PlayerBody.velocity.x);_Animator.SetFloat("LastX",LastMovement.x);_Animator.SetBool("Iddle",Iddle);_Animator.SetBool("IsJumping",IsJumping);_Animator.SetInteger("WeaponID",WeaponID);}}
public void PointLeftDownButton(){if(!Director.isActiveAndEnabled){LastMovement=new Vector3(-1,-1,LastMovement.z);Iddle=false;IsJumping=false;PlayerBody.velocity=new Vector2(PlayerBody.velocity.x,PlayerBody.velocity.y);_Animator.SetFloat("HorizontalInput",PlayerBody.velocity.x);_Animator.SetFloat("LastX",LastMovement.x);_Animator.SetBool("Iddle",Iddle);_Animator.SetBool("IsJumping",IsJumping);_Animator.SetInteger("WeaponID",WeaponID);}}
public void JumpButton(){if(Physics2D.Raycast(transform.position,Vector2.down,JumpFromGround,Ground.value)&&!Director.isActiveAndEnabled){PlayerBody.AddForce(Vector2.up*(JumpForce+220)*Time.fixedDeltaTime,ForceMode2D.Impulse);if(transform.position.y>-4.532){IsJumping=true;Iddle=false;_Animator.SetBool("IsJumping",IsJumping);_Animator.SetBool("Iddle",Iddle);}else{IsJumping=false;Iddle=true;_Animator.SetBool("Iddle",Iddle);_Animator.SetInteger("WeaponID",WeaponID);}}}

public void FireButton(){
if(LastMovement.x!=0&&WeaponID==0&&Time.time>NextFire&&!Director.isActiveAndEnabled){_Animator.SetTrigger("MeleeAttack");AxeAttackAnim=true;PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);}
if(LastMovement.x!=0&&_PlayerUI.PistolCurrentBullets>0&&WeaponID==1&&Time.time>NextFire&&!Director.isActiveAndEnabled){NextFire=Time.time+FireRate;RequestHandgunShot();_Animator.SetTrigger("PistolAttack");_PlayerUI.PistolCurrentBullets--;PlayerBody.velocity=new Vector2(0, PlayerBody.velocity.y);GetComponent<AudioSource>().PlayOneShot(SoundsClips[2]);}
if(LastMovement.x!=0&&_PlayerUI.ShotgunCurrentBullets>0&&WeaponID==2&&!ShotgunShotAnim&&!IsJumping&&!Director.isActiveAndEnabled){_Animator.SetTrigger("ShotgunShot");ShotgunShotAnim=true;_PlayerUI.ShotgunCurrentBullets--;PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);GetComponent<AudioSource>().PlayOneShot(SoundsClips[3]);}
if(LastMovement.x!=0&&_PlayerUI.UziCurrentBullets>0&&WeaponID==3&&Time.time>NextFire&&!Director.isActiveAndEnabled){NextFire=Time.time+FireRate;_Animator.SetTrigger("UziAttack");RequestMachineGunShot();_PlayerUI.UziCurrentBullets--;PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);GetComponent<AudioSource>().PlayOneShot(SoundsClips[4]);}}

public void ChangWeaponIDButton(){if(!ShotgunShotAnim&&!AxeAttackAnim&&!IsJumping){PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);WeaponID++;}}
//-------------------------------------------------------------CODIGOS PARA JOYSTICK------------------------
void JoystickMovement()
{if(Joystick){Vector2 JoystickInput=joystick.Vertical*Vector2.up+joystick.Horizontal*Vector2.right;
PlayerBody.velocity=new Vector2(JoystickInput.x*MovementSpeed,PlayerBody.velocity.y);
if(JoystickInput.x>0){Iddle=false;LastMovement=new Vector3(1,LastMovement.y,LastMovement.z);}else if(JoystickInput.x<0){Iddle=false;LastMovement = new Vector3(-1, LastMovement.y, LastMovement.z); }
if(JoystickInput.x==0){Iddle=true;}
if(JoystickInput.y>0.4){LastMovement=new Vector3(LastMovement.x,1,LastMovement.z);}else if(JoystickInput.y<-0.4){LastMovement = new Vector3(LastMovement.x, -1, LastMovement.z);}else if(JoystickInput.y==0){LastMovement=new Vector3(LastMovement.x,0,LastMovement.z);}}}
//----------------------------------------------------------------------------------------------------------
void MoveAnimation(){_Animator.SetInteger("WeaponID",WeaponID);_Animator.SetFloat("LastX",LastMovement.x);_Animator.SetFloat("LastY",LastMovement.y);_Animator.SetBool("Iddle",Iddle);_Animator.SetFloat("JumpInput",Input.GetAxisRaw("Jump"));_Animator.SetBool("IsJumping",IsJumping);_Animator.SetBool("IsDeath",IsDeath);_Animator.SetBool("InDanger",InDanger);}

void OnCollisionEnter2D(Collision2D collision)
{IsJumping=false;if(collision.gameObject.tag=="Enemy"&&!_PlayerAudioSource.isPlaying){_PlayerAudioSource.PlayOneShot(Quejidos[Random.Range(0,Quejidos.Count)]);}}

void CreationOfBullets()
{for(int i=0;i<10;i++)
{GameObject HS=Instantiate(PistolShot);
HS.SetActive(false);
HSProjectiles.Add(HS);}
for(int i=0;i<10;i++)
{GameObject MS=Instantiate(MachineGunShot);
MS.SetActive(false);
MSProjectiles.Add(MS);}}

void PistolAmmoComprobation(){for(int i=0;i<HSProjectiles.Count;i++)if(HSProjectiles[i]==null){GameObject RepuestoHS=Instantiate(PistolShot);RepuestoHS.SetActive(false);HSProjectiles[i]=(RepuestoHS);}}
void MachineGunAmmoComprobation(){for(int i=0;i<MSProjectiles.Count;i++)if(MSProjectiles[i]==null){GameObject RepuestoMS=Instantiate(MachineGunShot);RepuestoMS.SetActive(false);MSProjectiles[i]=(RepuestoMS);}}

public GameObject RequestHandgunShot()
{for(int i=0;i<HSProjectiles.Count;i++)
{if(!HSProjectiles[i].activeSelf){HSProjectiles[i].SetActive(true);
if(LastMovement.y<0){HSProjectiles[i].transform.position=transform.position+new Vector3(LastMovement.x,-0.2f,0);}
if(LastMovement.y>0){HSProjectiles[i].transform.position=transform.position+new Vector3(LastMovement.x,1.85f,0);}
if(LastMovement.y==0){HSProjectiles[i].transform.position=transform.position+new Vector3(LastMovement.x,0.8f,0);}
return HSProjectiles[i];}}
return HSProjectiles[0];}

public GameObject RequestMachineGunShot()
{for(int i=0;i<MSProjectiles.Count;i++)
{if(!MSProjectiles[i].activeSelf){MSProjectiles[i].SetActive(true);
if(LastMovement.x==1&&LastMovement.y==1||LastMovement.x==-1&&LastMovement.y==-1){MSProjectiles[i].transform.rotation=Quaternion.Euler(0,0,45);}
if(LastMovement.x==-1&&LastMovement.y==0||LastMovement.x==1&&LastMovement.y==0){MSProjectiles[i].transform.rotation=Quaternion.Euler(0,0,0);}
if(LastMovement.y<0){MSProjectiles[i].transform.position=transform.position+new Vector3(LastMovement.x,-0.5f,0);}
if(LastMovement.y>0){MSProjectiles[i].transform.position=transform.position+new Vector3(LastMovement.x,1.85f,0);}
if(LastMovement.y==0){MSProjectiles[i].transform.position=transform.position+new Vector3(LastMovement.x,0.7f,0);}
if(LastMovement.x==-1&&LastMovement.y==1||LastMovement.x==1&&LastMovement.y==-1){MSProjectiles[i].transform.rotation=Quaternion.Euler(0,0,-45);}
if(LastMovement.x==-1&&LastMovement.y==1||LastMovement.x==1&&LastMovement.y==-1){MSProjectiles[i].transform.rotation=Quaternion.Euler(0,0,-45);}
return MSProjectiles[i];}}
return MSProjectiles[0];}

void UseAxe(){if(Input.GetKeyDown(ShootKey)&&!Director.isActiveAndEnabled){_Animator.SetTrigger("MeleeAttack");AxeAttackAnim=true;}}
void UsePistol(){if(Input.GetKey(ShootKey)&&_PlayerUI.PistolCurrentBullets>0&&WeaponID==1&&Time.time>NextFire&&!HandgunShotAnim&&!Director.isActiveAndEnabled){NextFire=Time.time+FireRate;RequestHandgunShot();_Animator.SetTrigger("PistolAttack");_PlayerUI.PistolCurrentBullets--;GetComponent<AudioSource>().PlayOneShot(SoundsClips[2]);if(Time.time<NextFire){PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);HandgunShotAnim=true;}}}
void UseShotGun(){if(Input.GetKeyDown(ShootKey)&&_PlayerUI.ShotgunCurrentBullets>0&&WeaponID==2&&!ShotgunShotAnim&&!IsJumping&&!Director.isActiveAndEnabled){_Animator.SetTrigger("ShotgunShot");ShotgunShotAnim=true;_PlayerUI.ShotgunCurrentBullets--;PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);GetComponent<AudioSource>().PlayOneShot(SoundsClips[3]);}}
void UseMachineGun(){if(Input.GetKey(ShootKey)&&_PlayerUI.UziCurrentBullets>0&&WeaponID==3&&Time.time>NextFire&&!UziShotAnim&&!Director.isActiveAndEnabled){NextFire=Time.time+FireRate;RequestMachineGunShot();_Animator.SetTrigger("UziAttack");PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);_PlayerUI.UziCurrentBullets--;GetComponent<AudioSource>().PlayOneShot(SoundsClips[4]);UziShotAnim=true;}}

public void ChangWeaponID(){if(Input.GetKeyDown(KeyCode.Tab)&&!ShotgunShotAnim&&!AxeAttackAnim&&!IsJumping&&!Director.isActiveAndEnabled){PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);WeaponID++;}}

private void OnTriggerEnter2D(Collider2D collision){if(collision.gameObject.name=="Sani"&&!MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles){Dialogues[0].enabled=true;}
else if(collision.gameObject.name=="Sani"&&MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles){Dialogues[1].enabled=true;}}
private void OnTriggerExit2D(Collider2D collision){if(collision.gameObject.name=="Sani"){foreach (Image D in Dialogues){D.enabled=false;}}}

void Update()
{if(ShotgunShotAnim){ShotgunShotCrono-=Time.deltaTime;PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);if(ShotgunShotCrono<=0){ShotgunShotAnim=false;ShotgunShotCrono=OnShotgunShotCrono;}}
if(AxeAttackAnim){AxeCrono-=Time.deltaTime;if(AxeCrono!=OnAxeCrono){PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);}if(AxeCrono<=0){AxeAttackAnim=false;AxeCrono=OnAxeCrono;_Animator.Rebind();}}
if(HandgunShotAnim){HandGunShotCrono-=Time.deltaTime;PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);if(HandGunShotCrono<=0){HandgunShotAnim=false;HandGunShotCrono=OnHandGunShotCrono;}}
if(UziShotAnim){UziShotCrono-=Time.deltaTime;PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);if(UziShotCrono<=0){UziShotAnim=false;UziShotCrono=OnUziShotCrono;_Animator.Rebind();}}
DontCrossTheLimits();MoveAnimation();if(WeaponID>3){WeaponID=0;}ChangWeaponID();AmmoForUzi=Random.Range(15,60);AmmoForShotgun=Random.Range(1,12);AmmoForPistol=Random.Range(5,25);
switch(WeaponID)
{case 0:MovementSpeed=9;UseAxe();break;
case 1:PistolAmmoComprobation();UsePistol();NewMovementSpeed=5;MovementSpeed=NewMovementSpeed;FireRate=1f;break;
case 2:UseShotGun();NewMovementSpeed=5;MovementSpeed=NewMovementSpeed;FireRate=1.8f;break;
case 3:MachineGunAmmoComprobation();UseMachineGun();NewMovementSpeed=5;MovementSpeed=NewMovementSpeed;FireRate=0.5f;break;
default:UseAxe();break;}TimeValue=Time.time;
if(CurrentHealth>HealthValue){CurrentHealth=HealthValue;}
if(CurrentHealth<=0){IsDeath=true;GameManager._SharedInstanceGameManager.GameOver();gameObject.SetActive(false);}
else{IsDeath=false;}
if(CurrentArmor<=0){CurrentArmor=0;}}
void FixedUpdate(){if(!AxeAttackAnim&&!HandgunShotAnim&&!ShotgunShotAnim&&!UziShotAnim&&Director.enabled==false){JoystickMovement();Movement();
if(Physics2D.Raycast(transform.position-new Vector3(0,1.7f,0),new Vector2(LastMovement.x,0),0.5f,Ground.value)){PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);}
if(Physics2D.Raycast(transform.position+new Vector3(0,1.8f,0),new Vector2(LastMovement.x,0),0.5f,Ground.value)){PlayerBody.velocity=new Vector2(0,PlayerBody.velocity.y);}}}

}
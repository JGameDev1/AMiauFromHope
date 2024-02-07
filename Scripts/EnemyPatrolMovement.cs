using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyPatrolMovement : MonoBehaviour
{   [Range(0, 100)]
    public int DamageValue,GreenIndex; public int TopH, ButtomH, LeftMove, RightMove;
    public Rigidbody2D EnemyRb;
    public Vector2 LastPositionRegistred=Vector2.right;
    public float EnemySpeed,OnMoveCronometre,ReinitializeCronometreIn;
    private float MoveCronometre;
    public bool IsLookingAtTheRight,IsLookingAtTheLeft,IsMoving,GreenFlema,RedFlema,YellowFlema;
    public Animator _Animator;
    public EnemyHealthManager _HealthManager;
    public float LimitsOfMovementX,NegLimitsOfMovementX,LimitsOfMovementY,NegLimitsOfMovementY,XDistanceToView,YDistanceToView;
    public string TypeOfEnemy;
    public List<GameObject>FlemasToInst;public List<GameObject>FlemasInGame;
    public List<AudioClip>Sounds;
    public GameObject Player;
    public bool PlayerArt;

void MovementConf()
{if(GetComponent<EnemyHealthManager>().CurrentHealth<=0){EnemyRb.velocity=Vector2.zero*0;}
MoveCronometre-=Time.deltaTime;
if(MoveCronometre>0){EnemyRb.velocity=LastPositionRegistred*EnemySpeed;IsMoving=true;}
else{EnemyRb.velocity=Vector2.zero*EnemySpeed;IsMoving=false;}
if(MoveCronometre<=ReinitializeCronometreIn){MoveCronometre=OnMoveCronometre;int INDEXY=Random.Range(ButtomH,TopH),INDEXX=Random.Range(LeftMove,RightMove);LastPositionRegistred=new Vector2(INDEXX,INDEXY);}
if(LastPositionRegistred.x==0){LastPositionRegistred.x=-1;}
if(transform.position.x-Player.transform.position.x<=XDistanceToView&&transform.position.y-Player.transform.position.y<YDistanceToView&&transform.position.y-Player.transform.position.y>=-YDistanceToView||transform.position.x-Player.transform.position.x<=XDistanceToView&&transform.position.y-Player.transform.position.y<YDistanceToView&&transform.position.y-Player.transform.position.y>=-YDistanceToView&&TypeOfEnemy!="Flyer"){LastPositionRegistred=new Vector2(Player.transform.position.x-transform.position.x,LastPositionRegistred.y).normalized;}
if(transform.position.x-Player.transform.position.x<=XDistanceToView&&transform.position.y-Player.transform.position.y<YDistanceToView&&transform.position.y-Player.transform.position.y>=-YDistanceToView||transform.position.x-Player.transform.position.x<=XDistanceToView&&transform.position.y-Player.transform.position.y<YDistanceToView&&transform.position.y-Player.transform.position.y>=-YDistanceToView&&TypeOfEnemy=="Flyer"){LastPositionRegistred=new Vector2(Player.transform.position.x-transform.position.x,Player.transform.position.y-transform.position.y).normalized;}
if(GetComponent<EnemyHealthManager>().CurrentHealth<=0){EnemyRb.velocity=Vector2.zero*0;}}

void DontCrossTheLimits() 
{if(transform.position.x>= LimitsOfMovementX){transform.position=new Vector3(LimitsOfMovementX, transform.position.y,transform.position.z);}
if (transform.position.x<= NegLimitsOfMovementX){transform.position = new Vector3(NegLimitsOfMovementX, transform.position.y, transform.position.z);}
if (transform.position.y>= LimitsOfMovementY){transform.position = new Vector3(transform.position.x, LimitsOfMovementY, transform.position.z); }
if (transform.position.y<= NegLimitsOfMovementY){transform.position = new Vector3(transform.position.x, NegLimitsOfMovementY, transform.position.z);}}

void InstantiateFlemas(){for (int i=0;i<2;i++){GameObject FlemasInst=Instantiate(FlemasToInst[i]);FlemasInst.SetActive(false);FlemasInst.transform.parent=gameObject.transform;FlemasInGame.Add(FlemasInst);}}

public GameObject RequestFlemaVerde(){if(!FlemasInGame[GreenIndex].activeSelf){FlemasInGame[GreenIndex].SetActive(true);FlemasInGame[GreenIndex].transform.position=transform.position+new Vector3(LastPositionRegistred.x,2,0);}return FlemasInGame[0];}
void FlemaVerdeComprobation(){if(FlemasInGame[GreenIndex]==null){FlemasInGame[GreenIndex]=Instantiate(FlemasToInst[GreenIndex]);FlemasInGame.Add(FlemasInGame[GreenIndex]);}}
    
void UpdateViewOfEnemy()
{if(LastPositionRegistred.x<=-1){IsLookingAtTheLeft=true;}else{IsLookingAtTheLeft=false;}
if(LastPositionRegistred.x>=1){IsLookingAtTheRight=true;}else{IsLookingAtTheRight=false;}
if(LastPositionRegistred.x==0){IsLookingAtTheRight=false;IsLookingAtTheLeft=false;}}

void VolControl(){if(Player!=null){float DistanciaDelJugadorX=transform.position.x-Player.transform.position.x,DistanciaDelJugadorY=transform.position.y-Player.transform.position.y;if(DistanciaDelJugadorX<0){DistanciaDelJugadorX=-DistanciaDelJugadorX;}if(DistanciaDelJugadorY<0){DistanciaDelJugadorY=-DistanciaDelJugadorY;}
if(DistanciaDelJugadorX>=100||DistanciaDelJugadorY>=10){GetComponent<AudioSource>().volume=0;}else{GetComponent<AudioSource>().volume=(1/(DistanciaDelJugadorX/10));}}}

private void OnCollisionStay2D(Collision2D Collision)
{if(!PlayerArt){if(Collision.gameObject.CompareTag("Player")&&Collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor<=0){Collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=DamageValue;}else if(Collision.gameObject.CompareTag("Player")&&Collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor>0){Collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor-=DamageValue;}}
else if(PlayerArt){if(Collision.gameObject.CompareTag("Player")&&Collision.gameObject.GetComponent<PlayerArtController>().CurrentArmor<=0){Collision.gameObject.GetComponent<PlayerArtController>().CurrentHealth-=DamageValue;}else if(Collision.gameObject.CompareTag("Player")&&Collision.gameObject.GetComponent<PlayerArtController>().CurrentArmor>0){Collision.gameObject.GetComponent<PlayerArtController>().CurrentArmor-=DamageValue;}}}
void Animations(){_Animator.SetBool("IsMoving",IsMoving);_Animator.SetFloat("LastPositionRegistred",LastPositionRegistred.x);_Animator.SetInteger("LIFE",GetComponent<EnemyHealthManager>().CurrentHealth);}

private void OnEnable()
{MoveCronometre=OnMoveCronometre;
switch (TypeOfEnemy)
{case "ZombieMale":DamageValue=1;GetComponent<AudioSource>().clip=Sounds[Random.Range(3,5)];if(!GetComponent<AudioSource>().isPlaying){GetComponent<AudioSource>().Play();}break;
case "ZombieFemale":DamageValue=1;GetComponent<AudioSource>().clip=Sounds[Random.Range(5,8)];if(!GetComponent<AudioSource>().isPlaying){GetComponent<AudioSource>().Play();}break;
case "Flyer":DamageValue=1;GetComponent<AudioSource>().clip=Sounds[8];if(!GetComponent<AudioSource>().isPlaying){GetComponent<AudioSource>().Play();}break;
case "Animal":DamageValue=2;GetComponent<AudioSource>().clip=Sounds[Random.Range(0,3)];if(!GetComponent<AudioSource>().isPlaying){GetComponent<AudioSource>().Play();}break;
default: DamageValue=1;break;}
}

private void Start()
{Player=GameObject.Find("PlayerActionMan");
InstantiateFlemas();
EnemyRb=GetComponent<Rigidbody2D>();_HealthManager=GetComponent<EnemyHealthManager>();_Animator=GetComponent<Animator>();}

private void FixedUpdate()
{if(GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.RunningGame){MovementConf();}else if(GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.PauseTheGame){IsMoving=false;EnemyRb.velocity=Vector2.zero;}}

void DesactivateMe(){if(GameManager._SharedInstanceGameManager.Cinematica==true){this.gameObject.SetActive(false);}}

private void Update()
{VolControl();UpdateViewOfEnemy();DesactivateMe();DontCrossTheLimits();Animations();if(GetComponent<EnemyHealthManager>().CurrentHealth==0){DamageValue=0;}
if(GreenFlema){FlemaVerdeComprobation();RequestFlemaVerde();if(GreenIndex>1){GreenIndex=0;}}}
}
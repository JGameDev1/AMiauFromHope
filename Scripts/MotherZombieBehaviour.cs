using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherZombieBehaviour : MonoBehaviour
{
    [Range(0, 100)]
    public int DamageValue; public int LeftMove, RightMove;
    public Rigidbody2D EnemyRb;
    public Vector2 LastPositionRegistred = Vector2.right;
    public float EnemySpeed, OnMoveCronometre, ReinitializeCronometreIn;
    private float MoveCronometre;
    public bool IsLookingAtTheRight,IsLookingAtTheLeft,IsMoving;
    public Animator _Animator;
    public EnemyHealthManager _HealthManager;
    public float LimitsOfMovementX,NegLimitsOfMovementX,LimitsOfMovementY,NegLimitsOfMovementY,XDistanceToView,YDistanceToView;
    GameObject Player;

    void MovementConf()
{if(GetComponent<EnemyHealthManager>().CurrentHealth<=0){EnemyRb.velocity=Vector2.zero*0;}
    MoveCronometre-=Time.deltaTime;
    if(MoveCronometre>0){EnemyRb.velocity=LastPositionRegistred*EnemySpeed;IsMoving=true;}
    else{EnemyRb.velocity=Vector2.zero*EnemySpeed;IsMoving=false;}
    if(MoveCronometre<=ReinitializeCronometreIn){MoveCronometre=OnMoveCronometre;int INDEXX=Random.Range(LeftMove,RightMove);LastPositionRegistred=new Vector2(INDEXX,transform.position.y);}
    if(LastPositionRegistred.x==0){LastPositionRegistred.x=-1;}
    if(transform.position.x-Player.transform.position.x<=XDistanceToView&&transform.position.y-Player.transform.position.y<YDistanceToView&&transform.position.y-Player.transform.position.y>=-YDistanceToView||transform.position.x-Player.transform.position.x<=XDistanceToView&&transform.position.y-Player.transform.position.y<YDistanceToView&&transform.position.y-Player.transform.position.y>=-YDistanceToView){LastPositionRegistred=new Vector2(Player.transform.position.x-transform.position.x,LastPositionRegistred.y).normalized;}
    if(GetComponent<EnemyHealthManager>().CurrentHealth<=0){EnemyRb.velocity=Vector2.zero*0;}}

    void DontCrossTheLimits() 
    {if(transform.position.x>= LimitsOfMovementX){transform.position=new Vector3(LimitsOfMovementX, transform.position.y,transform.position.z);}
    if (transform.position.x<= NegLimitsOfMovementX){transform.position = new Vector3(NegLimitsOfMovementX, transform.position.y, transform.position.z);}
    if (transform.position.y>= LimitsOfMovementY){transform.position = new Vector3(transform.position.x, LimitsOfMovementY, transform.position.z); }
    if (transform.position.y<= NegLimitsOfMovementY){transform.position = new Vector3(transform.position.x, NegLimitsOfMovementY, transform.position.z);}}

    void UpdateViewOfEnemy()
{if(LastPositionRegistred.x<=-1){IsLookingAtTheLeft=true;}else{IsLookingAtTheLeft=false;}
if(LastPositionRegistred.x>=1){IsLookingAtTheRight=true;}else{IsLookingAtTheRight=false;}
if(LastPositionRegistred.x==0){IsLookingAtTheRight=false;IsLookingAtTheLeft=false;}}

void VolControl(){if(Player!=null){float DistanciaDelJugadorX=transform.position.x-Player.transform.position.x,DistanciaDelJugadorY=transform.position.y-Player.transform.position.y;if(DistanciaDelJugadorX<0){DistanciaDelJugadorX=-DistanciaDelJugadorX;}if(DistanciaDelJugadorY<0){DistanciaDelJugadorY=-DistanciaDelJugadorY;}
if(DistanciaDelJugadorX>=100||DistanciaDelJugadorY>=3){GetComponent<AudioSource>().volume=0;}else{GetComponent<AudioSource>().volume=(1/(DistanciaDelJugadorX/10));}}}

private void OnCollisionStay2D(Collision2D Collision)
{if(Collision.gameObject.CompareTag("Player")&&Collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor<=0){Collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth-=DamageValue;}else if(Collision.gameObject.CompareTag("Player")&&Collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor>0){Collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentArmor-=DamageValue;}}

void Animations(){_Animator.SetBool("IsMoving",IsMoving);_Animator.SetFloat("LastPositionRegistred",LastPositionRegistred.x);_Animator.SetInteger("LIFE",GetComponent<EnemyHealthManager>().CurrentHealth);}
private void Start()
{Player=GameObject.Find("PlayerActionMan");
EnemyRb=GetComponent<Rigidbody2D>();_HealthManager=GetComponent<EnemyHealthManager>();_Animator=GetComponent<Animator>();}

private void FixedUpdate()
{if(GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.RunningGame){MovementConf();}else if(GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.PauseTheGame){IsMoving=false;EnemyRb.velocity=Vector2.zero;}}

private void Update()
{VolControl();UpdateViewOfEnemy();DontCrossTheLimits();Animations();if(GetComponent<EnemyHealthManager>().CurrentHealth==0){DamageValue=0;}}
}

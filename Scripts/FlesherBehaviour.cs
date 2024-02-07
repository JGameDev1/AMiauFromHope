using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlesherBehaviour : MonoBehaviour
{public GameObject Player;
 public float LimitsOfMovementX,NegLimitsOfMovementX,LimitsOfMovementY,NegLimitsOfMovementY,OnMoveCronometre,ReinitializeCronometreIn,EnemySpeed,XDistanceToView,YDistanceToView;
 float MoveCronometre;
public Vector2 LastPositionRegistred;
bool IsDeath;
 Rigidbody2D EnemyRb;
 Animator animator;
 AudioSource audioSource;

    private void Start()
    {Player=GameObject.Find("PlayerActionMan");
    EnemyRb=GetComponent<Rigidbody2D>();
    animator=GetComponent<Animator>();
    audioSource=GetComponent<AudioSource>();
    IsDeath=false;}

    private void OnEnable()
    {IsDeath=false;}

    void VolControl(){if(Player!=null){float DistanciaDelJugadorX=transform.position.x-Player.transform.position.x,DistanciaDelJugadorY=transform.position.y-Player.transform.position.y;if(DistanciaDelJugadorX<0){DistanciaDelJugadorX=-DistanciaDelJugadorX;}if(DistanciaDelJugadorY<0){DistanciaDelJugadorY=-DistanciaDelJugadorY;}
if(DistanciaDelJugadorX>=100||DistanciaDelJugadorY>=20){GetComponent<AudioSource>().volume=0;}else{GetComponent<AudioSource>().volume=(1/(DistanciaDelJugadorX/10));}}}
        void DontCrossTheLimits() 
    {if(transform.position.x>=LimitsOfMovementX){transform.position=new Vector3(LimitsOfMovementX, transform.position.y,transform.position.z);}
    if (transform.position.x<=NegLimitsOfMovementX){transform.position = new Vector3(NegLimitsOfMovementX, transform.position.y, transform.position.z);}
    if (transform.position.y>=LimitsOfMovementY){transform.position = new Vector3(transform.position.x, LimitsOfMovementY, transform.position.z); }
    if (transform.position.y<=NegLimitsOfMovementY){transform.position = new Vector3(transform.position.x, NegLimitsOfMovementY, transform.position.z);}}

void MovementConf()
{if(GetComponent<EnemyHealthManager>().CurrentHealth<=0){EnemyRb.velocity=Vector2.zero*0;}
MoveCronometre-=Time.deltaTime;
if(MoveCronometre>0){EnemyRb.velocity=LastPositionRegistred*EnemySpeed;}
else{EnemyRb.velocity=Vector2.zero*EnemySpeed;}
if(MoveCronometre<=ReinitializeCronometreIn){MoveCronometre=OnMoveCronometre;int INDEXX=Random.Range(-1,2);LastPositionRegistred=new Vector2(INDEXX,transform.position.y);}
if(LastPositionRegistred.x==0){LastPositionRegistred.x=-1;}
if(transform.position.x-Player.transform.position.x<=XDistanceToView&&transform.position.y-Player.transform.position.y<YDistanceToView&&transform.position.y-Player.transform.position.y>=-YDistanceToView||transform.position.x-Player.transform.position.x<=XDistanceToView&&transform.position.y-Player.transform.position.y<YDistanceToView&&transform.position.y-Player.transform.position.y>=-YDistanceToView){LastPositionRegistred=new Vector2(Player.transform.position.x-transform.position.x,LastPositionRegistred.y).normalized;}
if(GetComponent<EnemyHealthManager>().CurrentHealth<=0){EnemyRb.velocity=Vector2.zero*0;}}

    private void OnCollisionEnter2D(Collision2D collision)
    {if(collision.gameObject.tag=="Enemy"&&collision.gameObject.name!="FlesherMini"){GetComponent<EnemyHealthManager>().CurrentHealth=GetComponent<EnemyHealthManager>().HealthValue;collision.gameObject.SetActive(false);}}

    private void OnCollisionStay2D(Collision2D collision)
{if(collision.gameObject.tag=="Player"){collision.gameObject.GetComponent<PlayerControllerWMW2D>().CurrentHealth--;}}
    void Animations(){animator.SetBool("IsDeath",IsDeath);animator.SetFloat("LastX",LastPositionRegistred.x);}

    private void Update()
    {if(GetComponent<EnemyHealthManager>().CurrentHealth<=0){IsDeath=true;}
    DontCrossTheLimits();VolControl();Animations();}

    private void FixedUpdate(){if(GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.RunningGame){MovementConf();}else if(GameManager._SharedInstanceGameManager.CurrentGamestate==Gamestates.PauseTheGame){EnemyRb.velocity=Vector2.zero;}}
}

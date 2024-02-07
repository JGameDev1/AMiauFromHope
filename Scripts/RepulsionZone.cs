using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsionZone : MonoBehaviour
{   public bool ToLeft, ToRight, ToUp, ToDown;
    [Range(0,1000)]public float TranslationForce;
    private BoxCollider2D TranslationArea;
    public GameObject BareerOfZone,Player,air;
    public bool barrera;

void VolAndAirControl()
{float DistanciaDelJugadorX=transform.position.x-Player.transform.position.x,DistanciaDelJugadorY=transform.position.y-Player.transform.position.y;if(DistanciaDelJugadorX<0){DistanciaDelJugadorX=-DistanciaDelJugadorX;}if(DistanciaDelJugadorY<0){DistanciaDelJugadorY=-DistanciaDelJugadorY;}
if(DistanciaDelJugadorX>=100||DistanciaDelJugadorY>=10){GetComponent<AudioSource>().volume=0;}else{GetComponent<AudioSource>().volume=1/(DistanciaDelJugadorX/10);}
if(DistanciaDelJugadorX>=10){air.SetActive(false);}else{air.SetActive(true);}}

    private void Start()
    {Player=GameObject.Find("PlayerActionMan");TranslationArea=GetComponent<BoxCollider2D>();TranslationArea.isTrigger = true;}


    private void OnTriggerStay2D(Collider2D Other)
    {if (Other.gameObject.tag == "Player")
    {BareerOfZone.SetActive(false);
    if(ToLeft){Other.GetComponent<Rigidbody2D>().AddForce(Vector2.left*TranslationForce);}
    if(ToRight){Other.GetComponent<Rigidbody2D>().AddForce(Vector2.right*TranslationForce);}
    if(ToUp){Other.GetComponent<Rigidbody2D>().gravityScale=-0.2f;}
    }
    }

private void OnTriggerExit2D(Collider2D collision)
{if(collision.gameObject.CompareTag("Player")){collision.GetComponent<Rigidbody2D>().gravityScale=1;if(barrera){BareerOfZone.SetActive(true);TranslationArea.enabled=false;}}}
    private void Update()
    {VolAndAirControl();}
}

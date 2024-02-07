using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]

public class MerchantBehaviour : MonoBehaviour
{public List<Image> DialogoDeVenta;GameObject _Player;
public bool LookAtRight,LookAtLeft,Talking;
private Animator _Animator;
public int ZombieCoresNecesaries,ComprasPosibles;
AudioSource _AudioSource;public List<AudioClip> Phrases;

private void OnTriggerEnter2D(Collider2D collision){if(collision.gameObject.tag=="Player"&&ComprasPosibles>=1){GameObject.FindObjectOfType<PlayerUI>().BuyButton.SetActive(true);Talking=true;DialogoDeVenta[0].enabled=true;}else if(collision.gameObject.tag=="Player"&&ComprasPosibles<1&&!GameObject.FindObjectOfType<MusicLanguajeManager>().Ingles){GameObject.FindObjectOfType<PlayerUI>().BuyButton.SetActive(true);Talking=true;DialogoDeVenta[3].enabled=true;}
if(collision.gameObject.tag=="Player"&&ComprasPosibles>=1){GameObject.FindObjectOfType<PlayerUI>().BuyButton.SetActive(true);Talking=true;DialogoDeVenta[0].enabled=true;}else if(collision.gameObject.tag=="Player"&&ComprasPosibles<1&&GameObject.FindObjectOfType<MusicLanguajeManager>().Ingles){GameObject.FindObjectOfType<PlayerUI>().BuyButton.SetActive(true);Talking=true;DialogoDeVenta[4].enabled=true;}}
private void OnTriggerStay2D(Collider2D collision){
if(collision.gameObject.tag=="Player"&&Input.GetKeyDown(KeyCode.Return)&&GameObject.FindObjectOfType<PlayerUI>().CoresColected>=ZombieCoresNecesaries&&ComprasPosibles>0&&!GameObject.FindObjectOfType<MusicLanguajeManager>().Ingles){foreach(Image Im in DialogoDeVenta){Im.enabled=false;}ComprasPosibles--;DialogoDeVenta[1].enabled=true;GameObject.FindObjectOfType<PlayerUI>().PistolCurrentBullets+=50;GameObject.FindObjectOfType<PlayerUI>().UziCurrentBullets+=100;GameObject.FindObjectOfType<PlayerUI>().ShotgunCurrentBullets+=10;GameObject.FindObjectOfType<PlayerControllerWMW2D>().CurrentHealth=GameObject.FindObjectOfType<PlayerControllerWMW2D>().HealthValue;GameObject.FindObjectOfType<PlayerControllerWMW2D>().CurrentArmor=GameObject.FindObjectOfType<PlayerControllerWMW2D>().ArmorValue;}else if(collision.gameObject.tag=="Player"&&Input.GetKeyDown(KeyCode.Return)&&GameObject.FindObjectOfType<PlayerUI>().CoresColected>=ZombieCoresNecesaries&&ComprasPosibles>0&&GameObject.FindObjectOfType<MusicLanguajeManager>().Ingles){foreach(Image Im in DialogoDeVenta){Im.enabled=false;}ComprasPosibles--;DialogoDeVenta[2].enabled=true;GameObject.FindObjectOfType<PlayerUI>().PistolCurrentBullets+=50;GameObject.FindObjectOfType<PlayerUI>().UziCurrentBullets+=100;GameObject.FindObjectOfType<PlayerUI>().ShotgunCurrentBullets+=10;GameObject.FindObjectOfType<PlayerControllerWMW2D>().CurrentHealth=GameObject.FindObjectOfType<PlayerControllerWMW2D>().HealthValue;GameObject.FindObjectOfType<PlayerControllerWMW2D>().CurrentArmor=GameObject.FindObjectOfType<PlayerControllerWMW2D>().ArmorValue;}
if(collision.gameObject.tag=="Player"&&Input.GetKeyDown(KeyCode.Return)&&GameObject.FindObjectOfType<PlayerUI>().CoresColected<ZombieCoresNecesaries&&ComprasPosibles>0&&!GameObject.FindObjectOfType<MusicLanguajeManager>().Ingles){foreach(Image Im in DialogoDeVenta){Im.enabled=false;}DialogoDeVenta[5].enabled=true;}else if(collision.gameObject.tag=="Player"&&Input.GetKeyDown(KeyCode.Return)&&GameObject.FindObjectOfType<PlayerUI>().CoresColected<ZombieCoresNecesaries&&ComprasPosibles>0&&!GameObject.FindObjectOfType<MusicLanguajeManager>().Ingles){foreach(Image Im in DialogoDeVenta){Im.enabled=false;}DialogoDeVenta[6].enabled=true;}
if(collision.gameObject.tag=="Player"&&Talking&&!_AudioSource.isPlaying&&ComprasPosibles>0){_AudioSource.clip=Phrases[0];_AudioSource.PlayOneShot(_AudioSource.clip);}
if(collision.gameObject.tag=="Player"&&Talking&&!_AudioSource.isPlaying&&ComprasPosibles<=0){_AudioSource.clip=Phrases[1];_AudioSource.PlayOneShot(_AudioSource.clip);}
if(collision.gameObject.tag=="Player"&&Talking&&!_AudioSource.isPlaying&&ComprasPosibles>0&&GameObject.FindObjectOfType<PlayerUI>().CoresColected<ZombieCoresNecesaries){_AudioSource.clip=Phrases[2];_AudioSource.PlayOneShot(_AudioSource.clip);}}
private void OnTriggerExit2D(Collider2D collision){if(collision.gameObject.tag=="Player"){Talking=false;_AudioSource.Stop();GameObject.FindObjectOfType<PlayerUI>().BuyButton.SetActive(false);foreach(Image Dialogs in DialogoDeVenta){Dialogs.enabled=false;}}}

private void Start()
{_Player=GameObject.Find("PlayerActionMan");_Animator=GetComponent<Animator>();_AudioSource=GetComponent<AudioSource>();}
private void Update(){if(_Player.transform.position.x<transform.position.x){LookAtRight=false;LookAtLeft=true;}else if(_Player.transform.position.x>transform.position.x){LookAtRight=true;LookAtLeft=false;}
_Animator.SetBool("LookAtRight",LookAtRight);_Animator.SetBool("LookAtLeft",LookAtLeft);_Animator.SetBool("Talking",Talking);}
}

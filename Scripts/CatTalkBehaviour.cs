using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CatTalkBehaviour : MonoBehaviour
{public List<Image> Dialogs;
public List<AudioClip>CatSounds;AudioSource _AudioSource;
public bool InDanger,Cartoon;Animator _Animator;
[Tooltip("Colocar Intro, Instructions1, Instructions2, LastTutorial, Ultimo")]
public string TypeOfDialogue;
public GameObject Player;
public float Desaparezcocuandolleguesa;
public List<GameObject>ObjectsInView;
private void Start(){Player=GameObject.Find("PlayerActionMan");_Animator=GetComponent<Animator>();_AudioSource=GetComponent<AudioSource>();foreach(var I in Dialogs){I.enabled=false;};}

private void OnTriggerEnter2D(Collider2D collision){if(collision.gameObject.tag=="Player")
{switch (TypeOfDialogue)
{case "Intro":if(!MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles){Dialogs[0].enabled=true;}else{Dialogs[1].enabled=true;}break;
case "Instructions1":if(!MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles){Dialogs[2].enabled=true;}else{Dialogs[3].enabled=true;}break;
case "Instructions2":if(!MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles){Dialogs[4].enabled=true;}else{Dialogs[5].enabled=true;}break;
case "Instructions3":if(!MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles){Dialogs[6].enabled=true;}else{Dialogs[7].enabled=true;}break;
case "LastTutorial":if(!MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles){Dialogs[8].enabled=true;}else{Dialogs[9].enabled=true;}break;
case "Ultimo":if(!MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles){Dialogs[14].enabled=true;}else{Dialogs[15].enabled=true;}break;
default:break;}
_AudioSource.Stop();if(!_AudioSource.isPlaying){_AudioSource.clip=CatSounds[Random.Range(0,1)];_AudioSource.PlayOneShot(_AudioSource.clip);}Cartoon=true;}
if(collision.gameObject.tag=="Enemy"){InDanger=true;if(!_AudioSource.isPlaying){_AudioSource.clip=CatSounds[2];_AudioSource.PlayOneShot(_AudioSource.clip);}if(!MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles){Dialogs[10].enabled=true;}else{Dialogs[11].enabled=true;}}
if(collision.gameObject.tag=="Boss"){if(!MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles){Dialogs[12].enabled=true;}else{Dialogs[13].enabled=true;}}
if(collision.gameObject.tag=="Player"&&ObjectsInView.Count==0){ObjectsInView.Add(collision.gameObject);}
if(collision.gameObject.tag=="Enemy"&&ObjectsInView.Count==0){ObjectsInView.Add(collision.gameObject);}}


private void OnTriggerExit2D(Collider2D collision){if(collision.gameObject.tag=="Player")
{_AudioSource.Stop();Cartoon=false;ObjectsInView.Clear();foreach(var I in Dialogs){I.enabled=false;}}
if(collision.gameObject.tag=="Enemy"){InDanger=false;ObjectsInView.Clear();}}

public void CatAnimations(){_Animator.SetBool("InDanger",InDanger);_Animator.SetBool("Cartoon",Cartoon);}

private void Update(){CatAnimations();
if(Player.transform.position.x>=Desaparezcocuandolleguesa){gameObject.SetActive(false);}
if(ObjectsInView.Count>0){if(ObjectsInView[0].gameObject.tag=="Player"){_Animator.SetFloat("OtherPosX",-ObjectsInView[0].gameObject.GetComponent<PlayerControllerWMW2D>().LastMovement.x);}else if(ObjectsInView[0].gameObject.tag=="Enemy"){_Animator.SetFloat("OtherPosX",-ObjectsInView[0].gameObject.GetComponent<EnemyPatrolMovement>().LastPositionRegistred.x);}}
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class DirectorActivation : MonoBehaviour
{   private PlayableDirector _Director;
    public List<GameObject>ObjectsToOff;
    public List<GameObject>ObjectsToOn;
    public TimelineAsset[]TimeLines;

    void Start()
    {_Director=GetComponent<PlayableDirector>();
    foreach(GameObject O in ObjectsToOn){O.SetActive(false);}
    if(!MusicLanguajeManager.MusicLanguajeManagerSharedInstance.Ingles){_Director.playableAsset=TimeLines[0];}else{_Director.playableAsset=TimeLines[1];}}

    private void OnTriggerEnter2D(Collider2D collision)
    {if(collision.gameObject.tag=="Player"&&SceneManager.GetActiveScene().buildIndex==1||collision.gameObject.tag=="Player"&&SceneManager.GetActiveScene().buildIndex==9){GameObject.FindAnyObjectByType<MusicLanguajeManager>().MyAudioSource.volume=0f;}
    if(collision.gameObject.tag=="Player"){foreach(GameObject G in ObjectsToOff){G.SetActive(false);}foreach(GameObject O in ObjectsToOn){O.SetActive(true);}_Director.enabled=true;}
    }

    private void Update()
{if(_Director.time>=_Director.playableAsset.duration){_Director.enabled=false;GameManager._SharedInstanceGameManager.Cinematica=true;}
if(_Director.enabled==true&&SceneManager.GetActiveScene().buildIndex==16){GameObject.FindAnyObjectByType<MusicLanguajeManager>().MyAudioSource.volume=0f;}
}
}
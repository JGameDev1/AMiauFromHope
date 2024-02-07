using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class NPCPositionSight : MonoBehaviour
{   private GameObject player;
    private AudioSource MyAudioSource;
    public float rotationY;
    void Start()
    {MyAudioSource=GetComponent<AudioSource>();
     player=FindObjectOfType<PlayerControllerWMW2D>().gameObject;}


    void Update()
    {if(player.transform.position.x>transform.position.x){transform.rotation=Quaternion.Euler(0,rotationY,0);}else{transform.rotation=Quaternion.Euler(0,this.transform.rotation.y,0);}
    if(player.transform.position.x>=transform.position.x-2&&player.transform.position.x<=transform.position.x+2&&!MyAudioSource.isPlaying){MyAudioSource.PlayOneShot(MyAudioSource.clip);}
    }
}

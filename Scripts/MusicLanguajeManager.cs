using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MusicLanguajeManager : MonoBehaviour
{public static MusicLanguajeManager MusicLanguajeManagerSharedInstance;
public AudioSource MyAudioSource;
public List<AudioClip>SongsToPlay;
public bool Ingles,UseJoystick,PCGame;
public int IndexSong;

public void listenerToWin(){GameManager._SharedInstanceGameManager.RecordLvl();GameManager._SharedInstanceGameManager.lvlPassed.RemoveListener(listenerToWin);}

private void Awake(){if(MusicLanguajeManager.MusicLanguajeManagerSharedInstance!=null){Destroy(gameObject);}
else{MusicLanguajeManager.MusicLanguajeManagerSharedInstance=this;
DontDestroyOnLoad(gameObject);}}
void Start(){MyAudioSource=GetComponent<AudioSource>();
IndexSong=SceneManager.GetActiveScene().buildIndex;
MyAudioSource.PlayOneShot(SongsToPlay[IndexSong]);
}

public void RequestSongs(){if(SceneManager.GetActiveScene().buildIndex!=IndexSong){MyAudioSource.Stop();IndexSong=SceneManager.GetActiveScene().buildIndex;}
if(!MyAudioSource.isPlaying){MyAudioSource.clip=SongsToPlay[IndexSong];MyAudioSource.PlayOneShot(MyAudioSource.clip);}}

void Update(){RequestSongs();}
}
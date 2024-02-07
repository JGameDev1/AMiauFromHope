using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Events;

public enum Gamestates {FinishTheLevel,RunningGame,PauseTheGame,GameOver}
public class GameManager : MonoBehaviour
{public static GameManager _SharedInstanceGameManager;
public string PauseKey;
public Canvas PlayerCanvas,GameOverCanvas,PauseCanvas,FinishLevelCanvas;
public Gamestates CurrentGamestate;
public int scoreToUnlockLvls,lastLvlNumber;
private PlayerControllerWMW2D _PlayerControllerWMW2D;
public PlayerUI PlayerUI;
public PlayerUIArt PlayerUIArt;
public UnityEvent lvlPassed;
public bool Art,Cinematica;

public void RunTheGame(){CurrentGamestate=Gamestates.RunningGame;if(CurrentGamestate==Gamestates.RunningGame){PlayerCanvas.enabled=true;GameOverCanvas.enabled=false;FinishLevelCanvas.enabled=false;PauseCanvas.enabled=false;Time.timeScale=1;}}
public void PauseTheGame(){CurrentGamestate=Gamestates.PauseTheGame;if(CurrentGamestate==Gamestates.PauseTheGame){PlayerCanvas.enabled=false;FinishLevelCanvas.enabled=false;GameOverCanvas.enabled=false;PauseCanvas.enabled=true;Time.timeScale=0;}}
public void GameOver(){CurrentGamestate=Gamestates.GameOver;if(CurrentGamestate==Gamestates.GameOver){PlayerCanvas.enabled=false;GameOverCanvas.enabled=true;FinishLevelCanvas.enabled=false;PauseCanvas.enabled=false;}}
public void FinishTheLevel(){CurrentGamestate=Gamestates.FinishTheLevel;if(CurrentGamestate==Gamestates.FinishTheLevel){PlayerCanvas.enabled=false;FinishLevelCanvas.enabled=true;GameOverCanvas.enabled=false;PauseCanvas.enabled=false;Time.timeScale=0;}}
public void InteractionWithNPC(){CurrentGamestate=Gamestates.RunningGame;if(CurrentGamestate==Gamestates.RunningGame&&_PlayerControllerWMW2D.IsInteracting){PlayerCanvas.enabled=true;FinishLevelCanvas.enabled=false;GameOverCanvas.enabled=false;PauseCanvas.enabled=false;}}
public void ChangeGamestates(Gamestates NewGameState)
{if(NewGameState==Gamestates.RunningGame){CurrentGamestate=NewGameState;RunTheGame();}
else if(NewGameState==Gamestates.PauseTheGame){CurrentGamestate=NewGameState;PauseTheGame();}
else if(NewGameState==Gamestates.GameOver){CurrentGamestate=NewGameState;GameOver();}
else if(NewGameState==Gamestates.FinishTheLevel){CurrentGamestate=NewGameState;FinishTheLevel();}}
public void BackMenu(){SceneManager.LoadScene("Menu");}
public void RetryLvl(){SceneManager.LoadScene(SceneManager.GetActiveScene().name);}
public void NextLevelButton(){if(scoreToUnlockLvls<=SceneManager.GetActiveScene().buildIndex){scoreToUnlockLvls++;}
lvlPassed.Invoke();SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);}
public void PauseAndContinueTheGame(){if(CurrentGamestate==Gamestates.RunningGame){PauseTheGame();}
else if(CurrentGamestate==Gamestates.PauseTheGame){RunTheGame();}}
public void RecordLvl(){if(scoreToUnlockLvls>PlayerPrefs.GetInt("lvlUnlock",1)){PlayerPrefs.SetInt("lvlUnlock",scoreToUnlockLvls);}}
public void RecordScore(){if(PlayerUI.CoresColected>PlayerPrefs.GetInt("CoresColected")&&!Art){PlayerPrefs.SetInt("CoresColected",PlayerUI.CoresColected);}}

private void Awake(){_SharedInstanceGameManager=this;PlayerCanvas=GameObject.Find("InGameCanvas").GetComponent<Canvas>();GameOverCanvas=GameObject.Find("GameOverCanvas").GetComponent<Canvas>();PauseCanvas=GameObject.Find("PauseCanvas").GetComponent<Canvas>();FinishLevelCanvas=GameObject.Find("WinCanvas").GetComponent<Canvas>();}

private void Start(){PlayerUI=GameObject.FindObjectOfType<PlayerUI>();
lvlPassed=new UnityEvent();lvlPassed.AddListener(MusicLanguajeManager.MusicLanguajeManagerSharedInstance.listenerToWin);
ChangeGamestates(CurrentGamestate);
scoreToUnlockLvls=PlayerPrefs.GetInt("lvlUnlock",2);lastLvlNumber=scoreToUnlockLvls;
GameObject.FindAnyObjectByType<MusicLanguajeManager>().MyAudioSource.volume=1f;}

}
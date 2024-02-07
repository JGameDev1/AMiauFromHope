using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class YRain : MonoBehaviour
{public List<GameObject>YellowClones;
public float OnTimerOff;float TimerOff;
private void OnEnable(){TimerOff=OnTimerOff;transform.position=GameObject.Find("PlayerActionMan").transform.position+Vector3.up*16;foreach (GameObject Clones in YellowClones)
{Clones.SetActive(true);}}

private void OnDisable(){foreach(GameObject Clones in YellowClones){Clones.transform.position=new Vector3(Clones.transform.position.x,this.transform.position.y,Clones.transform.position.z);}}

 private void Update(){TimerOff-=Time.deltaTime;if(TimerOff<=0){gameObject.SetActive(false);}}
}

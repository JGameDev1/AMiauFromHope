using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitsEffects : MonoBehaviour
{
public float OnTimerOff;
float TimerOff;

private void Update()
{TimerOff-=Time.deltaTime;
if(TimerOff<=0){gameObject.SetActive(false);}}

private void OnEnable(){TimerOff=OnTimerOff;}
private void OnDisable(){TimerOff=OnTimerOff;}

}

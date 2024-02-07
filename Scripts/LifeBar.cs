using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{public Image Life,BaseColorBar;
public float CurrentHealth,HealthValue;

    void Start()
    {HealthValue=GetComponentInParent<EnemyHealthManager>().HealthValue;
    CurrentHealth=HealthValue;}
    void ActualizarVida(){CurrentHealth=GetComponentInParent<EnemyHealthManager>().CurrentHealth;
    Life.fillAmount=CurrentHealth/HealthValue;}

    private void Update()
    {ActualizarVida();}
}

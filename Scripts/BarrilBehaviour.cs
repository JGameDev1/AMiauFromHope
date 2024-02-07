using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class BarrilBehaviour : MonoBehaviour
{
     DistanceJoint2D u;
     BoxCollider2D BarrelCollider;
     CircleCollider2D EfectZone;
    public bool aereal;
    public UnityEvent CutJoin, Explote;
    public GameObject Explosion,Barril;
    public List<AudioClip> ExplosionSounds;
    AudioSource a;
    public float markToDesactivate;

    void Start()
    {if(aereal){u=GetComponentInChildren<DistanceJoint2D>();}
    BarrelCollider = GetComponentInChildren<BoxCollider2D>();EfectZone=GetComponentInChildren<CircleCollider2D>();
    CutJoin=new UnityEvent();
    Explote=new UnityEvent();
    CutJoin.AddListener(JointCut);
    Explote.AddListener(ExplosionActivation);
    Explosion.SetActive(false);
    EfectZone.enabled=false;
    a=GetComponent<AudioSource>();
    a.clip=ExplosionSounds[Random.Range(0,ExplosionSounds.Count)];}

    void JointCut(){if(aereal){u.enabled=false;CutJoin.RemoveListener(JointCut);}}
    void ExplosionActivation(){BarrelCollider.gameObject.GetComponent<SpriteRenderer>().enabled=false;BarrelCollider.enabled=false;a.PlayOneShot(a.clip);Explosion.SetActive(true);EfectZone.enabled=true;Explote.RemoveListener(ExplosionActivation);}
    private void Update()
    {if(Barril.transform.position.y<markToDesactivate){Barril.SetActive(false);}}
}
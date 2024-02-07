using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   GameObject Target;
    public float PosY,NegLimitX,PosLimitX,NegLimitY,PosLimitY;

    void CameraPositionActualization()
    {   transform.position=new Vector3(Target.transform.position.x,Target.transform.position.y+PosY,transform.position.z);
        if(transform.position.y>=PosLimitY){transform.position=new Vector3(transform.position.x,PosLimitY,transform.position.z);}
        if (transform.position.y<=NegLimitY){transform.position=new Vector3(transform.position.x,NegLimitY,transform.position.z);}
        if (transform.position.x<=NegLimitX){transform.position=new Vector3(NegLimitX,transform.position.y,transform.position.z);}
        if (transform.position.x>=PosLimitX){transform.position=new Vector3(PosLimitX,transform.position.y,transform.position.z);}}

    void Start()
    {Target=GameObject.Find("PlayerActionMan");}

    void Update()
    {CameraPositionActualization();}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTr;
    private Transform tr;
    
    void Start()
    {
        tr = GetComponent<Transform>(); //캔버스의 Transform
        camTr = Camera.main.transform;  //카메라의 Transform
    }

    void LateUpdate()
    {
        tr.LookAt(camTr.position);
    }
}

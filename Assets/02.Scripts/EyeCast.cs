using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCast : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private Transform tr;

    [Range(5.0f, 20.0f)]
    public float range = 10.0f;

    private int hashIsLook = Animator.StringToHash("IsLook");
    public Animator anim; //CrooHair의 Animator 컴포넌트를 저장

    // Start is called before the first frame update
    void Start()
    {
        tr = transform;  //tr = GetComponent<Transform>();
        anim = transform.Find("Canvas/CrossHair").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ray(광선의 발사 원점, 광선의 방향)
        ray = new Ray(tr.position, tr.forward);

        #if UNITY_EDITOR
        Debug.DrawRay(ray.origin, ray.direction * range , Color.green);
        #endif

        //Physics.Raycast(광선, out 결괏값, 거리, 검출할 레이어)
        if (Physics.Raycast(ray, out hit, range, 1<<8 | 1<<9))
        {
            MoveCtrl.isStopped = true;
            //anim.SetBool("IsLook", true); //해시테이블 검색비용 발생
            anim.SetBool(hashIsLook, true);
        }
        else
        {
            MoveCtrl.isStopped = false;
            anim.SetBool(hashIsLook, false);
        }        

    }
}

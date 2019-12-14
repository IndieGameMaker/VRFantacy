using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCtrl : MonoBehaviour
{
    public enum MoveType
    {
        WAY_POINT, LOOK_AT
    }
    
    //이동방식을 결정하는 변수
    public MoveType moveType = MoveType.WAY_POINT;
    //웨이 포인트의 Transform 저장할 배열
    public Transform[] points;
    //이동해야할 다음 웨이포인트의 인덱스값
    public int nextIdx = 1;

    public float speed = 2.0f;
    public float damping = 3.0f;

    //MainCamera의 Transform 추출해서 저장할 변수
    private Transform camTr;

    // Start is called before the first frame update
    void Start()
    {
        camTr = Camera.main.GetComponent<Transform>();
        //camTr = Camera.main.transform;

        //WayPointGroup 게임오브젝트를 검색
        GameObject wayPointGroup = GameObject.Find("WayPointGroup");
        if (wayPointGroup != null)
        {
            //WayPointGroup 하위에 있는 모든 Point의 Transform컴포넌트를 추출
            points = wayPointGroup.GetComponentsInChildren<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (moveType)
        {
            case MoveType.WAY_POINT:
                MoveWayPoint();
                break;
            case MoveType.LOOK_AT:
                break;
        }
    }

    void MoveWayPoint()
    {
        //1. 이동할 방향으로의 벡터를 계산
        Vector3 dir = points[nextIdx].position - transform.position;
        //2. 산출한 벡터의 각도(쿼터니언 타입)를 산출
        Quaternion rot = Quaternion.LookRotation(dir);
        //3. 회전
        transform.rotation = Quaternion.Slerp(transform.rotation , rot , Time.deltaTime * damping);
        //4. 전진로직
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("WAY_POINT"))
        {
            //삼항연산자
            nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;

            /*
            nextIdx = nextIdx + 1;
            if (nextIdx >= points.Length)
            {
                nextIdx = 1;
            }
            */
        }
    }
}

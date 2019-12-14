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

    // Start is called before the first frame update
    void Start()
    {
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

    }
}

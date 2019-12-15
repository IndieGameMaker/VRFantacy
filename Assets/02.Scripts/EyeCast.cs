using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EyeCast : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private Transform tr;

    [Range(5.0f, 20.0f)]
    public float range = 10.0f;

    private int hashIsLook = Animator.StringToHash("IsLook");
    private Animator anim; //CrooHair의 Animator 컴포넌트를 저장

    //응시한 버튼을 저장하기 위한 변수
    private GameObject currButton = null;
    private GameObject prevButton = null;

    //현재 응시하고 있는 FillAmount 이미지 저장
    private Image circleBar;

    //응시할 시간
    public float selectedTime = 1.0f;
    //응시한 후부터 지난 시간
    private float passedTime = 0.0f;


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
            //버튼을 응시했을 경우
            LookButton();
        }
        else
        {
            MoveCtrl.isStopped = false;
            anim.SetBool(hashIsLook, false);
            //
            ReleaseButton();
        }        

    }

    void LookButton()
    {
        PointerEventData data = new PointerEventData(EventSystem.current);

        //8번 레이어
        if (hit.collider.gameObject.layer == 8)
        {
            currButton = hit.collider.gameObject; //현재 응시하고 있는 버튼
            circleBar = currButton.GetComponentsInChildren<Image>()[1];

            if (currButton != prevButton)
            {
                passedTime = 0.0f;
                
                //현재 응시하는 버튼에게 PointerEnter 이벤트를 전달
                ExecuteEvents.Execute(currButton
                                     , data
                                     , ExecuteEvents.pointerEnterHandler);

                //이전에 응시했던 버튼에게 PointerExit 이벤트를 전달
                ExecuteEvents.Execute(prevButton
                                     , data
                                     , ExecuteEvents.pointerExitHandler);
                
                prevButton = currButton;
            }
            else //같은 버튼을 계속 응시하는 경우
            {
                passedTime += Time.deltaTime;
                circleBar.fillAmount = passedTime / selectedTime;
            }
        }
        else
        {
            ReleaseButton();
        }
    }

    void ReleaseButton()
    {
        PointerEventData data = new PointerEventData(EventSystem.current);

        if (prevButton != null)
        {
            prevButton.GetComponentsInChildren<Image>()[1].fillAmount = 0.0f;
            ExecuteEvents.Execute(prevButton, data, ExecuteEvents.pointerExitHandler);
            prevButton = null;
        }
    }

}

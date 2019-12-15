using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemLook : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnLookItem(bool isLooked)
    {
        //Debug.Log(isLooked);
        //MoveCtrl.isStopped = isLooked;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MoveCtrl.isStopped = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       MoveCtrl.isStopped = false;
    }

    public void OnBoxOpen(bool isOpened)
    {
        Debug.Log(isOpened);
    }
}

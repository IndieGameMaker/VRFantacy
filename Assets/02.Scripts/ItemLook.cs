using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLook : MonoBehaviour
{
    public void OnLookItem(bool isLooked)
    {
        //Debug.Log(isLooked);
        MoveCtrl.isStopped = isLooked;
    }
}

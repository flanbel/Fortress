using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FiringPortUI : MonoBehaviour, IDropHandler
{
    //大砲。
    [SerializeField]
    private Cannon _Cannon;
    //発射。
    [SerializeField]
    private Firing _Firing;

    public void OnDrop(PointerEventData eventData)
    {
        BulletUI ui;
        if ((eventData.pointerDrag) &&
            (ui = eventData.pointerDrag.GetComponent<BulletUI>()))
        {
            //弾を発射。
            _Cannon.Shot(_Firing,ui.bulletInfo);
            
            //UI削除。
            Destroy(eventData.pointerDrag);
        }
    }
}
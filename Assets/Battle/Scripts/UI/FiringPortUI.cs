using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FiringPortUI : MonoBehaviour, IDropHandler
{
    //要塞。
    [SerializeField]
    private Fortress _Fortress;

    public void OnDrop(PointerEventData eventData)
    {
        BulletUI ui;
        if ((eventData.pointerDrag) &&
            (ui = eventData.pointerDrag.GetComponent<BulletUI>()))
        {
            //弾を発射。
            _Fortress.Shot(transform.GetSiblingIndex(), ui.bulletInfo);
            
            //ドラッグUIを非アクティブにする。
            eventData.pointerDrag.SetActive(false);
        }
    }
}
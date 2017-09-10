using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragRect : MonoBehaviour, IDragHandler
{
    [SerializeField]
    GameObject _Cannon;

    //ドラッグ感度。
    [SerializeField]
    private float _Sensitivity = 5.0f;

    public void OnDrag(PointerEventData eventData)
    {
        _Cannon.transform.Rotate(new Vector3(0, 0, Input.GetAxis("Mouse Y") * _Sensitivity));
    }
}
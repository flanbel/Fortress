using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FiringPortUI : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    //大砲。
    [SerializeField]
    private Cannon _Cannon;
    //発射。
    [SerializeField]
    private Firing _Firing;

    //ダミーイメージ。
    private Image _Dummy;
    //表示、非表示カラー。
    private Color _Display,_Hide;

    // Use this for initialization
    void Start()
    {
        _Dummy = transform.Find("Dummy").GetComponent<Image>();
        _Display = _Hide = _Dummy.color;
        _Hide.a = 0.0f;
        SetDummyImage(null);
    }

    private void SetDummyImage(Sprite sprite)
    {
        _Dummy.sprite = sprite;
        _Dummy.color = (sprite) ? _Display : _Hide;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Image image;
        if ((eventData.pointerDrag != null) && 
            (image = eventData.pointerDrag.GetComponent<Image>()))
        {
            SetDummyImage(image.sprite);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetDummyImage(null);
    }

    public void OnDrop(PointerEventData eventData)
    {
        BulletUI ui;
        if ((eventData.pointerDrag) &&
            (ui = eventData.pointerDrag.GetComponent<BulletUI>()))
        {
            //弾を発射。
            _Cannon.Shot(_Firing,ui.bulletInfo);
            //ダミーを消す。
            SetDummyImage(null);
            
            //ドラッグ終了。
            ui.EndDrag();
            //UI削除。
            Destroy(eventData.pointerDrag);
        }
    }
}
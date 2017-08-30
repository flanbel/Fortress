using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropObject : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    //アイコンを表示するイメージスクリプト。
    [SerializeField]
    private Image _Icon;
    //元のスプライト。
    private Sprite _Original;
    public Sprite original { set { _Original = value; } }

    // Use this for initialization
    void Start()
    {
        _Icon = transform.Find("Icon").GetComponent<Image>();
        _Original = _Icon.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DragObject drag;
        //ドラッグしている &&
        //イメージコンポーネントを持っている。
        if ((eventData.pointerDrag != null) &&
            (drag = eventData.pointerDrag.GetComponent<DragObject>()))
        {
            //ドラッグ中のイメージと同じ画像をダミー画像に設定。
            _Icon.sprite = drag.sourceImage.sprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //ドラッグ中。
        if ((eventData.pointerDrag != null))
        {
            //オリジナル画像に戻す。
            _Icon.sprite = _Original;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //オリジナル画像に戻す。
        _Icon.sprite = _Original;
    }
}

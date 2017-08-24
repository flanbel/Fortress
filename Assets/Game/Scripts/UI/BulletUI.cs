using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class BulletUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //弾丸の情報。
    [SerializeField]
    private BulletInfo _BulletInfo;
    public BulletInfo bulletInfo { get { return _BulletInfo; } set { _BulletInfo = value; } }
    //元の画像。
    Image _SourceImage;
    static Image _DragImage;
    //ドラッグ時に表示するオブジェクト。
    static GameObject _DragObject;

	// Use this for initialization
	void Start () {
        _SourceImage = GetComponent<Image>();

        if (_DragObject == null)
        {
            CreateDragObject();
        }
    }

    public void CreateDragObject()
    {
        _DragObject = new GameObject("Dragging Object");
        _DragObject.transform.SetParent(GameObject.Find("Canvas").transform);
        _DragObject.transform.SetAsLastSibling();
        _DragObject.transform.localScale = Vector3.one;

        // レイキャストがブロックされないように
        CanvasGroup canvasGroup = _DragObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;


        _DragImage = _DragObject.AddComponent<Image>();
        _DragObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SetDragObject();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _DragObject.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDrag();
    }

    //ドラッグ終了時に呼び出される処理。
    public void EndDrag()
    {
        _SourceImage.color = Color.white;
        _DragObject.SetActive(false);
    }


    // ドラッグオブジェクト作成
    private void SetDragObject()
    {
        _DragObject.SetActive(true);
        //情報をコピー。
        _DragImage.sprite = _SourceImage.sprite;
        _DragImage.rectTransform.sizeDelta = _SourceImage.rectTransform.sizeDelta;
        _DragImage.color = _SourceImage.color;
        _DragImage.material = _SourceImage.material;

        _SourceImage.color = Vector4.one * 0.6f;
    }
}

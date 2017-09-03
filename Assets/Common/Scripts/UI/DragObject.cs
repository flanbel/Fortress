using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //元の画像。
    [SerializeField]
    private Image _SourceImage;
    public Image sourceImage
    {
        get
        {
            if (_SourceImage == null)
            {
                _SourceImage = GetComponent<Image>();
            }
            return _SourceImage;
        }
    }
    //ドラッグする画像。
    static Image _DragImage;
    //ドラッグ時に表示するオブジェクト。
    static GameObject _DragObject;
    private GameObject dragObject
    {
        get
        {
            if (_DragObject == null)
            {
                CreateDragObject();
            }
            return _DragObject;
        }
    }

    RectTransform _CanvasRect;
    //自分が所属しているキャンバス。
    Canvas _ParentCanvas;
    Canvas canvas
    {
        get
        {
            if (_ParentCanvas == null)
            {
                _ParentCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                _CanvasRect = _ParentCanvas.GetComponent<RectTransform>();
;            }
            return _ParentCanvas;
        }
    }

    //ドラッグオブジェクト作成。
    public void CreateDragObject()
    {
        _DragObject = new GameObject("Dragging Object");
        _DragObject.transform.SetParent(canvas.transform,false);
        _DragObject.transform.SetAsLastSibling();
        _DragObject.transform.localScale = Vector3.one;

        // レイキャストがブロックされないように
        CanvasGroup canvasGroup = _DragObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;


        _DragImage = _DragObject.AddComponent<Image>();
        _DragObject.SetActive(false);
    }

    // ドラッグオブジェクト設定。
    private void SetDragObject()
    {
        dragObject.SetActive(true);
        //情報をコピー。
        _DragImage.sprite = sourceImage.sprite;
        _DragImage.rectTransform.sizeDelta = sourceImage.rectTransform.sizeDelta;
        _DragImage.color = sourceImage.color;
        _DragImage.material = sourceImage.material;

        sourceImage.color = Vector4.one * 0.6f;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SetDragObject();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //マウスに追従させる。
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            //マウスのポジションそのまま。
            dragObject.transform.position = Input.mousePosition;
        }
        else
        {
            //
            Vector3 screenPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            Vector2 WorldObject_ScreenPosition = new Vector2(
      ((screenPos.x * _CanvasRect.sizeDelta.x) - (_CanvasRect.sizeDelta.x * 0.5f)),
      ((screenPos.y * _CanvasRect.sizeDelta.y) - (_CanvasRect.sizeDelta.y * 0.5f)));

            dragObject.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDrag();
    }

    //ドラッグ終了時に呼び出される処理。
    public void EndDrag()
    {
        sourceImage.color = Color.white;
        dragObject.SetActive(false);
    }

    private void OnDisable()
    {
        EndDrag();
    }
}

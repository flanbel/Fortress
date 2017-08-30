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
    public Image sourceImage { get { return _SourceImage; } }
    //ドラッグする画像。
    static Image _DragImage;
    //ドラッグ時に表示するオブジェクト。
    static GameObject _DragObject;

    // Use this for initialization
    void Start()
    {
        if (_SourceImage == null)
            _SourceImage = GetComponent<Image>();

        if (_DragObject == null)
        {
            CreateDragObject();
        }
    }
    //ドラッグオブジェクト作成。
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

    // ドラッグオブジェクト設定。
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

    private void OnDestroy()
    {
        if (_DragObject)
            _DragObject.SetActive(false);
    }
}

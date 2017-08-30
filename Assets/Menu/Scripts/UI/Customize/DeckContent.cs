using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//デッキをUIでカスタマイズするときDropを受け取るクラス。
public class DeckContent : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    //デッキのＵＩ。
    private BulletsDeckUI _BulletsDeck;
    public BulletsDeckUI bulletsDeck { set { _BulletsDeck = value; } }

    //アイコン。
    [SerializeField]
    private Image _Icon;
    public Sprite icon
    {
        set
        {
            DropObject _Drop = GetComponent<DropObject>();
            //新しいスプライト設定。
            _Drop.original = _Icon.sprite = value;
        }
    }

    //何番目か？
    public int idx = 0;

    //弾丸の説明テキスト。
    private Text _DescriptionText;
    private void Start()
    {
        _DescriptionText = GameObject.Find("DescriptionText").GetComponent<Text>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        BulletItem item = eventData.pointerDrag.GetComponent<BulletItem>();
        if (item != null)
        {
            _BulletsDeck.SetBulletInfo(idx, item.bulletInfo.ID);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        BulletInfo info = Data.GetBulletInfo(Data.Deck.bullets[idx]);
        if (info != null)
            _DescriptionText.text = info.Name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _DescriptionText.text = "";
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//カスタマイズ画面の弾丸リストに並ぶ、弾丸の情報を持ったアイテム。
public class BulletItem : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{ 
    //弾丸の情報
    private BulletInfo _BulletInfo;
    public BulletInfo bulletInfo
    {
        get
        {
            return _BulletInfo;
        }
        set
        {
            _BulletInfo = value;
            //ここに書いていいのか・・・？
            if(_BulletInfo.ID == 0)
            {
                Data.bulletsStock[0] = -1;
            }
            //アイコン設定。
            _Icon.sprite = _BulletInfo.Icon;
            //テキスト設定。
            SetStockText();
            StockCheck();
        }
    }
    //弾丸のアイコン。
    [SerializeField]
    private Image _Icon;
    //弾丸の在庫を表示するテキスト。
    [SerializeField]
    private Text _BulletNum;

    //弾丸の説明テキスト。
    private Text _DescriptionText;

    //ボタンスクリプト。
    private Button _Button;
    private Button button
    {
        get
        {
            if (!_Button) _Button = GetComponent<Button>();
            return _Button;
        }
    }
    //ドラッグスクリプト。
    private DragObject _Drag;
    private DragObject drag
    {
        get
        {
            if (!_Drag) _Drag = GetComponent<DragObject>();
            return _Drag;
        }
    }

    private void Start()
    {
        _DescriptionText = GameObject.Find("DescriptionText").GetComponent<Text>();
        
        //ボタンを押したらデッキに弾丸をセットする様にする。
        BulletsDeckUI deck = FindObjectOfType<BulletsDeckUI>();
        //onClickに関数追加。
        button.onClick.AddListener(() => { deck.SetBulletInfoFromButton(this); });
    }

    private void OnEnable()
    {
        if (_BulletInfo)
        {
            //弾数はもしかしたら変わっているかもしれないので(生産の関係で。)
            SetStockText();
            StockCheck();
        }
    }

    //ストックがないときは選べないようにする。
    private void StockCheck()
    {
        //選択不可。
        button.interactable = drag.enabled = !(GetBulletStock(_BulletInfo.ID) == 0);
    }

    //弾数を可視化するテキストに設定。
    private void SetStockText()
    {
        int stock = GetBulletStock(_BulletInfo.ID);
        //弾数設定。
        _BulletNum.text = "×" + ((stock == -1) ? "無限" : stock.ToString());
    }

    //残弾取得
    private int GetBulletStock(int id)
    {
        //登録されているか？
        if (Data.bulletsStock.Count <= id)
        {
            //要素追加。
            Data.bulletsStock.Insert(id, 0);
            //保存
            SaveData.SetList("BulletStockList", Data.bulletsStock);
            SaveData.Save();
        }

        return Data.bulletsStock[id];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _DescriptionText.text = bulletInfo.Name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _DescriptionText.text = "";
    }
}

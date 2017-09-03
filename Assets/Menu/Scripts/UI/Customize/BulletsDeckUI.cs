using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//デッキをUIでカスタマイズする。
public class BulletsDeckUI : MonoBehaviour {

    //UI上でデッキを構成するコンテナたち。
    private DeckContent[] _Contents;    

    // Use this for initialization
    void Start () {

        //Dropを受け取るＵＩ作成。
        GameObject prefab = Resources.Load("Prefab/UI/Custom/DeckContent") as GameObject;
        for(int idx = 0; idx < Data.Deck.bulletNum; idx++)
        {
            GameObject content = Instantiate(prefab, transform);
            DeckContent deck = content.GetComponent<DeckContent>();
            deck.bulletsDeck = this;
            deck.idx = idx;
        }
        _Contents = GetComponentsInChildren<DeckContent>();
        //
        SetDeckIcon();
    }

    //デッキを空にする。
    public void ClearDeck()
    {
        for(int i = 0; i < Data.Deck.bulletNum; i++)
        {
            Data.Deck.bullets[i] = -1;
        }
        SetDeckIcon();
        SaveData.SetClass("BulletsDeck_0", Data.Deck);
        SaveData.Save();
    }

    //デッキコンテナのアイコンを設定。
    private void SetDeckIcon()
    {
        int i = 0;
        foreach(DeckContent con in _Contents)
        {
            BulletInfo info = Data.GetBulletInfo(Data.Deck.bullets[i++]);   
            con.icon = (info != null) ? info.Icon : null;
        }
    }

    //ボタンを押したらセット。
    public void SetBulletInfoFromButton(BulletItem button)
    {
        //空いているところを探す。
        int idx = 0;
        foreach(int id in Data.Deck.bullets)
        {
            //空か？
            if(id < 0)
            {
                //情報セット。
                SetBulletInfo(idx, button.bulletInfo.ID);
                break;
            }
            idx++;
        }
    }

    //Dropが受け取った弾丸の情報をデッキに設定する。
    //[in] 何番目か？
    //[in] 弾丸のID。
    public void SetBulletInfo(int idx,int id)
    {
        Data.Deck.bullets[idx] = id;

        //配列を昇順にソート。
        Array.Sort(Data.Deck.bullets, delegate (int a, int b)
        {
            //入れ替えなし。
            if (a < 0 && b < 0)
                return 0;
            //
            if (a < 0)
                return 1;
            //
            if (b < 0)
                return -1;


            return a - b;
        }
        );

        SetDeckIcon();
        SaveData.SetClass("BulletsDeck_0", Data.Deck);
        SaveData.Save();
    }
}

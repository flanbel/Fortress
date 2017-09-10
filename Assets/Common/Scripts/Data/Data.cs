using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : SingletonMonoBehaviour<Data> {

    //全ての弾丸の情報が格納された図鑑。
    private BulletInfoSO _BulltsDictionary;
    /// <summary>
    /// 弾丸の情報が格納された配列。
    /// </summary>
    static public BulletInfo[] bulletsDictionary
    {
        get
        {
            if (Instance._BulltsDictionary == null)
            {
                Instance._BulltsDictionary =  Resources.Load("Data/BulletInfoSO") as BulletInfoSO;
            }
            //配列を返す。
            return Instance._BulltsDictionary.array;
        }
    }

    //引数のidと一致したIDを持つ弾丸の情報を返す。
    //[in] 弾丸のID。
    //[out] IDと一致した弾丸の情報。
    static public BulletInfo GetBulletInfo(int id)
    {
        //IDが一致する弾丸が登録されてる場合のみ情報が返る。
        return Array.Find(bulletsDictionary, (a) => (a.ID == id));
    }

    //弾丸のレシピ情報が格納された図鑑。
    private BulletRecipeSO _BulltsRecipe;
    /// <summary>
    /// 弾丸のレシピ情報が格納された配列。
    /// </summary>
    static public BulletRecipe[] bulletsRecipe
    {
        get
        {
            if (Instance._BulltsRecipe == null)
            {
                Instance._BulltsRecipe = Resources.Load("Data/BulletRecipeSO") as BulletRecipeSO;
            }
            //配列を返す。
            return Instance._BulltsRecipe.array;
        }
    }

    //引数のidと一致したIDを持つ弾丸のレシピを返す。
    //[in] 弾丸のID。
    //[out] IDと一致した弾丸のレシピ。
    static public BulletRecipe GetBulletRecipe(int id)
    {
        return Array.Find(bulletsRecipe, (a) => (a.bulletID == id));
    }

    //各弾丸の保持量を格納したリスト。
    private List<int> _BulletStockList;
    /// <summary>
    /// 弾丸の保持量が格納されたリスト。
    /// </summary>
    static public List<int> bulletsStock
    {
        get
        {
            if (Instance._BulletStockList == null)
            {
                Instance._BulletStockList = SaveData.GetList("BulletStockList", new List<int>());
                while(Instance._BulletStockList.Count < bulletsDictionary.Length)
                {
                    Instance._BulletStockList.Add(0);
                }
            }
            return Instance._BulletStockList;
        }
    }

    //弾丸の所持数追加。
    //[in] 増やしたい弾丸のID。
    //[in] 増やす個数。
    static public void AddBulletStock(int id,int num)
    {
        BulletInfo item = GetBulletInfo(id);
        if (item)
        {
            int idx = Array.IndexOf(bulletsDictionary, item);
            //弾丸を追加。
            bulletsStock[idx] += num;

            SaveData.SetList("BulletStockList", Instance._BulletStockList);
            SaveData.Save();
        }
    }

    //デッキ。
    private BulletsDeck _BulletsDeck;
    /// <summary>
    /// デッキを返す。
    /// </summary>
    static public BulletsDeck Deck
    {
        get
        {
            if (Instance._BulletsDeck == null)
            {
                Instance._BulletsDeck = SaveData.GetClass("BulletsDeck_0", new BulletsDeck());
            }
            return Instance._BulletsDeck;
        }
    }
}

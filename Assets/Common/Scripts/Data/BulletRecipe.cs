using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//弾丸を生産する際のレシピ。
[System.Serializable]
public class BulletRecipe {
    //作成する弾の情報。
    public int bulletID = -1;

    //作成する個数。
    public int generateNum = 1;

    //生成に必要なコスト。
    public int cost = -1;

    //弾丸の情報を取得。
    public BulletInfo bulletInfo { get { return Data.GetBulletInfo(bulletID); } }
}

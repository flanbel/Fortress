using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤー
public class Player : MonoBehaviour {

    Fortress _Fortress;

    private void Start()
    {
        _Fortress = transform.GetComponentInChildren<Fortress>();
        //要塞の情報を設定。
        _Fortress.Info.deck = SaveData.GetClass("BulletsDeck_0", new BulletsDeck());
    }

    //要塞から弾を発射する。
    public void Shot(BulletInfo info)
    {
        //発射。
        _Fortress.Shot(info, Vector3.right * 100);
    }
}

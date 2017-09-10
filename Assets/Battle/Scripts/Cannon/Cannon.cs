using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//大砲の基底クラス。
public abstract class Cannon : MonoBehaviour
{
    //発射口。
    private Transform _FiringPoint;
    private Transform firingPoint
    {
        get
        {
            if(_FiringPoint == null)
            {
                //子から検索。
                _FiringPoint = transform.Find("FiringPoint");
            }
            return _FiringPoint;
        }
    }

    //重力補正。
    [SerializeField]
    protected float _GravityScale =0.5f;
    public float gravityScale { set { _GravityScale = value; } }

    //弾を発射する。
    public abstract void Shot(BulletInfo info,Vector3 vec);

    //弾を生成して。必要な情報を設定。
    //[in] 弾丸の情報。
    //[out] 生成された弾丸のオブジェクト。
    protected GameObject SetBullet(BulletInfo info)
    {
        //発射口に弾丸のインスタンスを生成。
        GameObject obj = GameBulletsManager.Instance.GetBullet();
        obj.transform.position = firingPoint.position;
        obj.transform.localScale = transform.lossyScale;
        //大砲と同じタグを設定。
        obj.tag = tag + "Bullet";
        obj.layer = LayerMask.NameToLayer(obj.tag);

        Bullet bullet = obj.GetComponent<Bullet>();
        //弾丸の情報を設定。
        bullet.bulletInfo = info;
        return obj;
    }
}

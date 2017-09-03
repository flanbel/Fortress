using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    
    //発射口。
    [SerializeField]
    private Transform _FiringPoint;
    public Vector3 firingPos { get { return _FiringPoint.position; } }

    public void Start()
    {
        _FiringPoint.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    //弾を発射。
    public void Shot(Firing firing,BulletInfo info)
    {
        //発射口に弾丸のインスタンスを生成。
        GameObject obj = GameBulletsManager.Instance.GetBullet();
        obj.transform.position = _FiringPoint.position;
        //大砲と同じタグを設定。
        obj.tag = tag;

        Bullet bullet = obj.GetComponent<Bullet>();
        //弾丸の情報を設定。
        bullet.bulletInfo = info;
        
        Rigidbody2D rigid = obj.GetComponent<Rigidbody2D>();
        //力を加える。
        rigid.AddForce(firing.GetVectorToTarget(), ForceMode2D.Impulse);
        //重力倍率設定。
        rigid.gravityScale = firing.gravityScale;
    }
}

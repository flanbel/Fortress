using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    //要塞。
    Fortress _Fortress;
    //ベースとなる弾丸のプレハブ。
    private GameObject _BulletBasePrefab;
	// Use this for initialization
	void Start () {
        _Fortress = transform.parent.gameObject.GetComponent<Fortress>();
        //プレハブの取得。
        _BulletBasePrefab = Resources.Load("Prefab/BulletBase") as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    //弾を発射。
    public void Shot(Firing firing,BulletInfo info)
    {
        //弾薬を消費。
        _Fortress.armory.Remove(info);

        //発射口に弾丸のインスタンスを生成。
        GameObject obj = Instantiate(_BulletBasePrefab, firing.firingPoint.position, new Quaternion());
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

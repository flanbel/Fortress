using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//標準的な大砲。
public class StandardCannon : Cannon {

    //弾を発射。
    public override void Shot(BulletInfo info, Vector3 vec)
    {
        GameObject obj = SetBullet(info);

        Rigidbody2D rigid = obj.GetComponent<Rigidbody2D>();
        //力を加える。
        rigid.AddForce(Quaternion.Euler(0, 0, transform.eulerAngles.z) * vec, ForceMode2D.Impulse);
        float rot = (transform.lossyScale.x > 0) ? transform.eulerAngles.z : 360 - transform.eulerAngles.z;
        //
        rigid.gravityScale = (rot < 180) ? rot / 50.0f * 0.8f : 0.0f;
        rigid.gravityScale *= (info.Speed / 2.5f);
    }
}

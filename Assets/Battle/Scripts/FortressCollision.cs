using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortressCollision : MonoBehaviour {

    [SerializeField]
    private Fortress _Fortress;
    private Fortress fortress
    {
        get
        {
            if (_Fortress == null)
            {
                _Fortress = transform.GetComponentInParent<Fortress>();
            }
            return _Fortress;
        }
    }

    //当たり判定。
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //異なるタグと衝突した。
        if (!collision.gameObject.tag.Contains(tag))
        {
            Bullet bullet;
            if (bullet = collision.gameObject.GetComponent<Bullet>())
            {
                int damage = bullet.bulletInfo.Power;
                //ダメージのテキストを作成。
                DisplayDamage.Instance.DisplayDamageText(damage, bullet.transform.position, fortress);
            }
        }
    }
}

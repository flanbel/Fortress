using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲームシーンで発射する弾丸。
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    SpriteRenderer _Sprite;
    SpriteRenderer sprite
    {
        get
        {
            if(_Sprite == null)
            {
                _Sprite = GetComponent<SpriteRenderer>();
            }
            return _Sprite;
        }
    }

    //弾丸の情報。
    [SerializeField]
    private BulletInfo _BulletInfo;
    public BulletInfo bulletInfo
    {
        get { return _BulletInfo; }
        set
        {
            //値をコピーする
            _BulletInfo.CopyInfo(value);
            sprite.sprite = _BulletInfo.Texture;
        }
    }

    Rigidbody2D _Rigid;

    private void Start()
    {
        _Rigid = GetComponent<Rigidbody2D>();        
    }

    private void OnEnable()
    {
        sprite.sortingOrder = 1;
        Invoke("SetOrder", 2.0f);
    }

    // Update is called once per frame
    void Update () {
        //範囲外チェック。
        CheckLimit();

        if (_BulletInfo.Straight)
        {
            Vector3 vec = _Rigid.velocity.normalized * transform.localScale.x;
            transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(vec.x, -vec.y) - 90);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -1 * transform.localScale.x));
        }
    }

    //
    private void SetOrder()
    {
        sprite.sortingOrder = 100;
    }

    //耐久値の減算。
    private void SubEndurance(int sub)
    {
        //減算した結果、耐久値が0以下になったなら。
        if ((_BulletInfo.Endurance -= sub) <= 0)
        {
            _Break();
        }        
    }

    //弾丸破壊。
    private void _Break()
    {
        GameObject bom = GameBulletsManager.Instance.Explosion();
        bom.transform.position = transform.position;

        gameObject.SetActive(false);
    }

    //範囲外に出た。
    private void CheckLimit()
    {
        // 画面右上のワールド座標をビューポートから取得
        Vector2 size = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.x > size.x ||
            transform.position.y < -10)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //相手の弾丸と衝突した。
        Bullet bullet;
        if (bullet = collision.gameObject.GetComponent<Bullet>())
        {
            SubEndurance(1);

            //else
            //{
            //    //反射。
            //    tag = tag.Contains("Player") ? "EnemyBullet" : "PlayerBullet";

            //    transform.localScale = -transform.localScale;
            //}
        }
        else
        {
            SubEndurance(999);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //相手の城と衝突した。
        string Tag = tag.Replace("Bullet", "");
        if (!collision.gameObject.tag.Contains(Tag))
        {
            _Break();   
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲームシーンで発射する弾丸。
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
	
	// Update is called once per frame
	void Update () {
        //範囲外チェック。
        CheckLimit();
        transform.Rotate(new Vector3(0, 0, 1));
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
        if (transform.position.x > 1960 ||
            transform.position.y < -10)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //異なるタグと衝突した。
        if (tag != collision.gameObject.tag)
        {
            SubEndurance(1);
        }
    }
}

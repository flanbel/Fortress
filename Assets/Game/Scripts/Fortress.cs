using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fortress : MonoBehaviour {

    //要塞の情報。
    [SerializeField]
    private FortressInfo _FortressInfo;
    public FortressInfo Info
    {
        get
        {
            if(_FortressInfo == null)
            {
                _FortressInfo = new FortressInfo();
            }
            return _FortressInfo;
        }
    }
    //体力。
    private int HP
    {
        get { return Info.HP; }
        set
        {
            Info.HP = value;
            //テキスト変更。
            _HPText.text = Info.HP.ToString();
            //HPバー変更。
            float rate = (float)HP / (float)Info.param.MaxHP;
            _HPBar.fillAmount = rate;
        }
    }
    
    //弾丸が補給されるところ。
    [SerializeField]
    private List<int> _Armory = new List<int>();
    public List<int> armory { get { return _Armory; } }

    //弾薬の限界保持数。
    private int _AmmoLimitNum = 20;
    public int ammoLimitNum { get { return _AmmoLimitNum; } }

    //大砲。
    private Cannon _Cannon;
    private Cannon cannon
    {
        get
        {
            if(_Cannon == null)
            {
                //子にあるキャノン取得。
                _Cannon = transform.GetComponentInChildren<Cannon>();
                _Cannon.tag = tag;
            }
            return _Cannon;
        }
    }

    private Firing[] _Firing;
    private Firing[] firing
    {
        get
        {
            if (_Firing == null)
            {
                //子にあるキャノン取得。
                _Firing = transform.GetComponentsInChildren<Firing>();
            }
            return _Firing;
        }
    }

    private Text _HPText;
    private Image _HPBar;

    // Use this for initialization
    void Start () {
        //体力テキスト検索。
        _HPText = GameObject.Find(name + "HPText").GetComponent<Text>();
        _HPBar = GameObject.Find(name + "HpBar").GetComponent<Image>();
        HP = Info.param.MaxHP;
    }

    //弾丸を補給する。
    //[in] 補給したい弾丸のID。
    //[out] 補給できたかどうか？
    public bool AddAmmo(int id)
    {
        BulletInfo info = Data.GetBulletInfo(id);
        //限界数未満か？
        bool ret = (_Armory.Count < _AmmoLimitNum);

        if ((info != null) && ret)
        {
            //内部弾薬を補給。
            _Armory.Add(id);

            //UI設定。
            if (tag == "Player")
                GameBulletsManager.Instance.DisplayBulletUI(info);
        }
        return ret;
    }

    //弾を発射。
    public void Shot(int idx,BulletInfo info)
    {
        //弾薬を消費。
        _Armory.Remove(info.ID);

        idx = Mathf.Min(idx, firing.Length - 1);
        //発射。
        cannon.Shot(firing[idx], info);
    }

    //ダメージを適用。
    public void ApplyDamage(int damage)
    {
        StartCoroutine(AnimationHP(-damage));
    }

    //HP変動アニメーション。
    private IEnumerator AnimationHP(int value)
    {
        //整数にする。
        int abs = Mathf.Abs(value);
        //1フレームに1づつ変化させる。
        for(int i = 0; i < abs; i++)
        {
            //正規化した値を渡す(1か-1)。
            HP += (value / abs);
            //1フレーム待機。
            yield return new WaitForEndOfFrame();
        }
    }

    //当たり判定。
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //異なるタグと衝突した。
        if (tag != collision.gameObject.tag)
        {
            Bullet bullet;
            if (bullet = collision.gameObject.GetComponent<Bullet>())
            {
                int damage = bullet.bulletInfo.Power;
                //ダメージのテキストを作成。
                DisplayDamage.Instance.DisplayDamageText(damage, bullet.transform.position, tag);
            }
        }
    }
}

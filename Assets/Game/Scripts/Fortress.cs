using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fortress : MonoBehaviour {

    //要塞の情報。
    [SerializeField]
    private FortressInfo _FortressInfo;
    public FortressInfo fortressInfo { get { return _FortressInfo; } }
    
    //弾薬庫。
    [SerializeField]
    private List<BulletInfo> _Armory = new List<BulletInfo>();
    public List<BulletInfo> armory { get { return _Armory; } }
    //弾薬の限界保持数。
    private int _AmmoLimitNum = 20;

    //弾薬を補給する先のUI。
    private Transform _BulletsListUI;

    //弾薬のUIのプレハブ。
    private GameObject _BulletUIPrefab;

    [SerializeField]
    private Text _HPText;
    [SerializeField]
    private Image _HPBar;

    // Use this for initialization
    void Start () {
        //要塞の情報を設定。
        //TODO:メニュー画面でカスタマイズした内容を読み込む。
        _FortressInfo = new FortressInfo();
        _FortressInfo.deck = SaveData.GetClass("BulletsDeck_0", new BulletsDeck());

        //体力テキスト検索。
        _HPText = GameObject.Find(name + "HPText").GetComponent<Text>();
        _HPText.text = _FortressInfo.HP.ToString();

        _HPBar = GameObject.Find(name + "HpBar").GetComponent<Image>();

        _BulletsListUI = GameObject.Find("BulletsList").transform;
        _BulletUIPrefab = Resources.Load("Prefab/UI/BulletUI") as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //弾薬を補給する。
    public bool AddAmmo(BulletInfo bullet)
    {
        bool ret;
        //限界数以下なら。
        if (ret = (_Armory.Count <= _AmmoLimitNum))
        {
            //内部弾薬を補給。
            _Armory.Add(bullet);
            //UI上で補給。
            GameObject bulletui = Instantiate(_BulletUIPrefab, _BulletsListUI);
            bulletui.GetComponent<BulletUI>().bulletInfo = bullet;
        }
        return ret;
    }

    //ダメージを適用。
    public void ApplyDamage(int damage)
    {
        StartCoroutine(AnimationHP(-damage));
    }

    //体力を変動させる。
    private void FluctuationHP(int value)
    {
        _FortressInfo.HP += value;
        //テキスト変更。
        _HPText.text = _FortressInfo.HP.ToString();
        //HPバー変更。
        float rate = (float)_FortressInfo.HP / (float)_FortressInfo.MaxHP;
        _HPBar.fillAmount = rate;
    }

    //HP変動アニメーション。
    private IEnumerator AnimationHP(int value)
    {
        int abs = Mathf.Abs(value);
        for(int i = 0; i < abs; i++)
        {
            //体力変更。
            FluctuationHP(value / abs);
            //1フレーム待機。
            yield return new WaitForEndOfFrame();
        }
    }

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
                DisplayDamage.GetInstance().CreateDamageText(damage, bullet.transform.position, tag);
            }
        }
    }
}

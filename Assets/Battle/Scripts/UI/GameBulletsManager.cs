using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲーム画面の弾丸を管理するクラス。
public class GameBulletsManager : SingletonMonoBehaviour<GameBulletsManager> {

    //要塞。
    [SerializeField]
    Fortress _PlayerFortress;

    //弾薬のUIのプレハブ。
    [SerializeField]
    private GameObject _BulletUIPrefab;

    //弾丸のUIを配置するリスト。
    [SerializeField]
    private Transform _BulletsListUI;

    private List<BulletUI> _UIList = new List<BulletUI>();

    //弾丸のプレハブ。
    [SerializeField]
    private GameObject _BulletBasePrefab;

    private List<GameObject> _Bullets = new List<GameObject>();

    //おかしい気がするけど。
    //爆発のプレハブ。
    [SerializeField]
    private GameObject _ExplosionPrefab;

    //爆発を配置するリスト。
    [SerializeField]
    private Transform _ExplosionList;

    private List<GameObject> _BomList = new List<GameObject>();

    private void Start()
    {
        //一番最初に生成しておく。
        //UI作成。
        for (int i = 0; i < _PlayerFortress.ammoLimitNum; i++)
        {
            GameObject obj = Instantiate(_BulletUIPrefab, _BulletsListUI);
            obj.SetActive(false);
            _UIList.Add(obj.GetComponent<BulletUI>());
        }
        //弾丸作成。
        for (int i = 0; i < 60; i++)
        {
            GameObject bullet = Instantiate(_BulletBasePrefab,transform);
            bullet.name = "Bullet_" + i;
            bullet.SetActive(false);
            _Bullets.Add(bullet);
        }
        //爆発作成。
        for (int i = 0; i < 60; i++)
        {
            GameObject bom = Instantiate(_ExplosionPrefab, _ExplosionList);
            bom.SetActive(false);
            _BomList.Add(bom);
        }
    }

    //弾丸のＵＩ表示。
    public void DisplayBulletUI(BulletInfo info)
    {
        //非アクティブなUIを取得。
        BulletUI ui = _UIList.Find((a) => a.gameObject.activeSelf == false);

        ui.gameObject.SetActive(true);

        //一番後ろに回す。
        ui.transform.SetAsLastSibling();
        ui.bulletInfo = info;
    }

    //弾丸を取得。
    public GameObject GetBullet()
    {
        //非アクティブな弾丸を取得。
        GameObject bullet = _Bullets.Find((a) => a.gameObject.activeSelf == false);
        bullet.SetActive(true);
        return bullet;
    }

    //爆発を表示。
    public GameObject Explosion()
    {
        //非アクティブな弾丸を取得。
        GameObject bom = _BomList.Find((a) => a.gameObject.activeSelf == false);
        bom.SetActive(true);
        return bom;
    }
}

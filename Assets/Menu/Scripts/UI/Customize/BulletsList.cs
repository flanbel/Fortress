using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsList : MonoBehaviour {
    //カスタムリスト。
    private CustomList _CustomList;

    //ページに追加するプレハブ。
    private GameObject _BulletItemPrefab;

    private void Start()
    {
        _BulletItemPrefab = Resources.Load("Prefab/UI/BulletItem") as GameObject;

        //リストのアイテム生成方法を設定。
        _CustomList = GetComponent<CustomList>();
        _CustomList.createItemCB = CreateBullet;
    }

    public GameObject CreateBullet(int id,Transform page)
    {
        GameObject bullet = Instantiate(_BulletItemPrefab, page);
        BulletItem item = bullet.GetComponent<BulletItem>();
        item.bulletInfo = new BulletInfo(id);
        return bullet;
    }
}

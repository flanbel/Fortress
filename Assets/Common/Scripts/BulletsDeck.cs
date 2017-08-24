using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//複数の弾丸で構成されたデッキ。
public class BulletsDeck {
    //弾丸の配列。
    private BulletInfo[] _Bullets;
    public BulletInfo[] bullets { get { return _Bullets; } }

    private const int _BulletNum = 40;
    public int bulletNum { get { return _BulletNum; } }

    public BulletsDeck()
    {
        //配列をサイズ分確保しただけ。
        _Bullets = new BulletInfo[_BulletNum];        
    }

    public void Initialize()
    {
        //コンストラクタを呼んで初期化。
        for (int i = 0; i < _BulletNum; i++)
        {
            _Bullets[i] = new BulletInfo(0);
        }
    }
}

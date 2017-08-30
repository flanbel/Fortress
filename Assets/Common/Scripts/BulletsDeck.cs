using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//複数の弾丸で構成されたデッキ。
[System.Serializable]
public class BulletsDeck {
    //弾丸のIDの配列。
    [SerializeField]
    private int[] _Bullets;
    public int[] bullets
    {
        get
        {
            if(_Bullets == null)
            {
                //配列をサイズ分確保。
                _Bullets = new int[_BulletNum];
                for(int i = 0; i < _BulletNum; i++)
                {
                    _Bullets[i] = -1;
                }
            }
            return _Bullets;
        }
    }
    [SerializeField]
    private const int _BulletNum = 40;
    public int bulletNum { get { return _BulletNum; } }
}

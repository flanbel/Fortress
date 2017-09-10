using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//要塞の情報を持っているクラス。
[System.Serializable]
public class FortressInfo
{
    //要塞のパラメータ。
    private FortressParameter _Param = new FortressParameter();
    public FortressParameter param { get { return _Param; } set { _Param = value; } }

    //現在の体力
    [SerializeField]
    private int _HP;
    public int HP { get { return _HP; } set { _HP = Mathf.Max(0, value); } }

    private FortressParts[] _Parts;
    public FortressParts[] parts { get { return _Parts; } set { _Parts = value; } }

    //弾丸のデッキ。
    private BulletsDeck _Deck = new BulletsDeck();
    public BulletsDeck deck { get { return _Deck; } set { _Deck = value; } }

    public FortressInfo()
    {
        _Parts = new FortressParts[4];

        _HP = param.MaxHP;
    }
}

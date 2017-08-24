using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//要塞の情報を持っているクラス。
[System.Serializable]
public class FortressInfo
{
    //最大体力
    public int MaxHP = 100;
    //体力
    [SerializeField]
    private int _HP = 0;
    public int HP { get { return _HP; } set { _HP = Mathf.Max(0, value); } }

    private BulletsDeck _Deck;
    public BulletsDeck deck { get { return _Deck; } }

    public FortressInfo()
    {
        _Deck = new BulletsDeck();
        _HP = MaxHP;
    }
}

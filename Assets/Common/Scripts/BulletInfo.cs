using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//弾丸の情報を持っているクラス。
[System.Serializable]
public class BulletInfo {
    //弾丸を識別するための一意な番号。
    public int ID = -1;
    //名前。
    public string Name = "unknown";
    //攻撃力。
    public int Power = 0;
    //弾速。
    public int Speed = 0;
    //耐久値。
    public int Endurance = 0;
    //アイコン画像。
    private Sprite _Icon;
    public Sprite Icon
    {
        get
        {
            if(_Icon == null)
            {
                _Icon = Resources.Load<Sprite>("Texture/BulletTexture");
            }
            return _Icon;
        }
    }
    //ゲームで使用する画像。
    private Sprite _Texture;
    public Sprite Texture
    {
        get
        {
            if (_Texture == null)
            {
                //"Texture_" + ID; 
                _Texture = Resources.Load<Sprite>("Texture/BulletTexture");
            }
            return _Texture;
        }
    }

    public static bool operator true(BulletInfo info ) { return info != null; }
    public static bool operator false(BulletInfo info) { return info == null; }

    public BulletInfo()
    {

    }

    public BulletInfo(int id)
    {       
        //情報設定。
        SetInfo(Data.GetBulletInfo(id));
    }

    private void SetInfo(BulletInfo info)
    {
        this.ID = info.ID;
        this.Name = info.Name;
        this.Power = info.Power;
        this.Speed = info.Speed;
        this.Endurance = info.Endurance;

        _Icon = Resources.Load<Sprite>("Texture/BulletTexture");
        _Texture = Resources.Load<Sprite>("Texture/BulletTexture");
    }
}
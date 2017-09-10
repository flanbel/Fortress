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
    //まっすぐ飛ぶか？
    public bool Straight = false;

    //テクスチャたち。
    static Sprite[] _BulletsTexture;
    private Sprite[] bulletsTexture
    {
        get
        {
            if(_BulletsTexture == null)
            {
                // Resoucesから対象のテクスチャから生成したスプライト一覧を取得
                _BulletsTexture = Resources.LoadAll<Sprite>("Texture/bareto");
            }

            return _BulletsTexture;
        }
    }
    //アイコン画像。
    [SerializeField]
    private Sprite _Icon = null;
    public Sprite Icon
    {
        get
        {
            if(_Icon == null)
            {

                // 対象のスプライトを取得                 
                Sprite s = Array.Find<Sprite>(bulletsTexture, (sprite) => sprite.name.Equals("bareto_" + ID));
                _Icon = (s != null) ? s : Resources.Load<Sprite>("Texture/BulletTexture");
            }
            return _Icon;
        }
    }
    //ゲームで使用する画像。
    private Sprite _Texture = null;
    public Sprite Texture
    {
        get
        {
            if (_Texture == null)
            {
                Sprite s = Array.Find<Sprite>(bulletsTexture, (sprite) => sprite.name.Equals("bareto_" + ID));
                _Texture = (s != null) ? s : Resources.Load<Sprite>("Texture/BulletTexture");
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
        CopyInfo(Data.GetBulletInfo(id));
    }

    //値をコピーする。
    public void CopyInfo(BulletInfo info)
    {
        this.ID = info.ID;
        this.Name = info.Name;
        this.Power = info.Power;
        this.Speed = info.Speed;
        this.Endurance = info.Endurance;
        this.Straight = info.Straight;

        this._Icon = info.Icon;
        this._Texture = info.Texture;
    }
}
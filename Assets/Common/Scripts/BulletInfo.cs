using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//弾丸の情報を持っているクラス。
[System.Serializable]
public class BulletInfo {
    //弾丸を識別するための一意な番号。
    public int ID = 0;
    //攻撃力。
    public int Power = 1;
    //弾速。
    public int Speed = 1;
    //耐久値。
    public int Endurance = 1;
    //アイコン画像。
    public Sprite Icon;
    //ゲームで使用する画像。
    public Sprite Texture;

    private BulletInfoSO _BulletsInfo;

    public BulletInfo(int id)
    {
        if(_BulletsInfo == null)
        {
            _BulletsInfo = Resources.Load("Data/BulletInfoSO") as BulletInfoSO;
        }
        this.ID = id;
        //情報設定。
        SetInfo(_BulletsInfo.array[id]);
    }

    private void SetInfo(BulletInfo info)
    {
        this.Power = info.Power;
        this.Speed = info.Speed;
        this.Endurance = info.Endurance;
    }
}
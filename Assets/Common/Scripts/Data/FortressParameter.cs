using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//要塞のパラメータ。
[System.Serializable]
public class FortressParameter {
    //最大体力。
    public int MaxHP = 100;
    //弾速補正。
    public float SpeedCorrection = 1.0f;
    //大砲発射後のクールタイム。
    public float FiringCoolTime = 3.0f;
    //弾の補給間隔。
    public float SupplyInterval = 2.0f;
    //パーツを装備するのにかかるコスト。
    public int Cost = 0;
}

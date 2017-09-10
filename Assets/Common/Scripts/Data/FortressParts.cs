using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//要塞を構成するパーツ。
[System.Serializable]
public class FortressParts {
    //パーツを識別するＩＤ。
    public int ID;
    //パーツの名前。
    public int Name;
    //パーツのタイプ
    public enum PartsTypeE
    {
        Body,       //要塞の外装、外壁。
        Cannon,     //大砲。
        SupplyBase, //補給基地。
        Engine,     //エンジン。

        Num,        //パーツの種類。
        Unknown = -1,
    }
    public PartsTypeE PartsType = PartsTypeE.Unknown;

    //増加するパラメータ。
    public FortressParameter Param = new FortressParameter();
}

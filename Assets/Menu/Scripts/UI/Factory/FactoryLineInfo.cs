using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//工場のライン情報。
[System.Serializable]
public class FactoryLineInfo
{
    //ラインの状態を示す。
    public enum LineState
    {
        Initial = -1,       //初期状態。
        Idle = 0,           //待機状態。
        Generating = 1,     //生産中。
        Completed = 2       //完成。
    }

    //残り時間。(分)
    public int timer = -1;

    //状態。
    public LineState state = LineState.Initial;

    //リピートするかどうか？
    public bool repeat = false;

    //生産する弾丸のレシピ。
    public BulletRecipe recipe = new BulletRecipe();
}

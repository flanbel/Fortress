using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogo : MonoBehaviour {

    [SerializeField]
    TitleBullet _Bullet;
    [SerializeField]
    GameObject _TapToStart;

    //アニメーション終了。
    public void EndAnim()
    {
        _TapToStart.SetActive(true);
        _Bullet._Ready = true;
    }
    //テキスト
}

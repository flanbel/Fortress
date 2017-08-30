using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class BulletUI : MonoBehaviour
{
    //弾丸の情報。
    [SerializeField]
    private BulletInfo _BulletInfo;
    public BulletInfo bulletInfo { get { return _BulletInfo; } set { _BulletInfo = value; } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Image))]
public class BulletUI : MonoBehaviour
{
    Image _Image;
    Image image
    {
        get
        {
            if (_Image == null)
            {
                _Image = GetComponent<Image>();
            }
            return _Image;
        }
    }

    //弾丸の情報。
    [SerializeField]
    private BulletInfo _BulletInfo;
    public BulletInfo bulletInfo
    {
        get { return _BulletInfo; }
        set
        {
            _BulletInfo = value;
            image.sprite = _BulletInfo.Icon;
        }
    }
}

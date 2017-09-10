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

    private Player _Player;
    private Player player
    {
        get
        {
            if(_Player == null)
            {
                _Player = GameObject.FindObjectOfType<Player>();
            }
            return _Player;
        }
    }

    //UIが押された時に呼び出される処理。
    public void OnClick()
    {
        //弾の発射。
        player.Shot(_BulletInfo);
        //
        gameObject.SetActive(false);
    }
}

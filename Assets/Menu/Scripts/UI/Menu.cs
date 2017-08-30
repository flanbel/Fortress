using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    //メニューのタイトル。
    [SerializeField]
    private string _Title;
    public string title { get { return _Title; } }

    //メインメニューかどうか？
    [SerializeField]
    private bool _isMainMenu;
    public bool isMain { get { return _isMainMenu; } }

    //順番。
    [SerializeField]
    private int _Order = 0;
    public int order { get { return _Order; } set { _Order = value; } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//確認ボックス。
public class ConfirmBox : MonoBehaviour {

    //テキスト。
    [SerializeField]
    private Text _Title, _Message;
    //ボタン。
    [SerializeField]
    private Button _YES, _No;

    //アニメーション。
    private Animator _Anim;

    private void Start()
    {
        if (_Anim == null)
        {
            _Anim = GetComponent<Animator>();
        }
        _Anim.Play(Animator.StringToHash("Open"));
    }

    //確認ボックスを呼び出す関数。
    //[in] [YES]ボタンを押した時に実行する関数。
    //[in] [NO]ボタンを押したときに実行する関数。
    public void CallConfirmBox(UnityAction YES, UnityAction NO, string title, string messeage)
    {
        //テキスト設定。
        _Title.text = title;
        _Message.text = messeage;

        //ボタンの関数を設定。
        _YES.onClick.RemoveAllListeners();
        _YES.onClick.AddListener(YES);
        _YES.onClick.AddListener(() => this.Close());

        _No.onClick.RemoveAllListeners();
        _No.onClick.AddListener(NO);
        _YES.onClick.AddListener(() => this.Close());
    }

    //ボックスを閉じる処理。
    private void Close()
    {
        //オープンアニメーションの逆再生。
        _Anim.speed = -1;
        _Anim.Play(Animator.StringToHash("Open"));
    }
}

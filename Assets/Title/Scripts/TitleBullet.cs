using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイトル画面でとばす砲弾。
public class TitleBullet : MonoBehaviour {
    
    Animator _Anim;
    //爆発のエフェクト。
    [SerializeField]
    GameObject _Bomber;

    public bool _Ready = false;

    // Use this for initialization
    void Start () {
        _Anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //準備ＯＫ &&
        //タップされた。
        if (_Ready && Input.GetMouseButtonDown(0))
        {
            //発射。
            Firing();
        }
    }

    //弾発射。
    public void Firing()
    {
        _Anim.SetTrigger("Firing");
    }
    //爆発。
    public void Bomber()
    {
        _Bomber.SetActive(true);
        foreach(TitleText text in FindObjectsOfType<TitleText>())
        {
            text.Excute();
        }
        //n秒後にシーン切り替え。
        Invoke("NextScene", 2);
    }
    //メニューシーンに遷移。
    private void NextScene()
    {
        FadeManager.Instance.LoadScene("Menu", 2.0f);
    }
}

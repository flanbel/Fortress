using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DamageText : MonoBehaviour {

    private Animator _Animator;
    private Animator animator
    {
        get
        {
            if (_Animator == null)
            {
                _Animator = GetComponent<Animator>();
            }
            return _Animator;
        }
    }
    //テキスト。
    private Text _Text;
    //アニメーションが終了したか？
    private bool _EndAnim = false;
    //要塞への参照。
    public Fortress fortress;

    private void Start()
    {
        _Text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        _EndAnim = false;
        animator.Play("DamageText");
    }

    // Update is called once per frame
    void Update () {
        CheckEndAnimation();
    }

    private void CheckEndAnimation()
    {
        if (gameObject.activeSelf &&
            _EndAnim == false &&
           animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Animator.StringToHash("End Animation"))
        {
            _EndAnim = true;
            StartCoroutine(Move());
        }
    }

    //要塞にダメージ適用。
    private void ApplyDamage()
    {
        fortress.ApplyDamage(Convert.ToInt16(_Text.text));
    }

    private IEnumerator Move()
    {
        float timer = 0.0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = fortress._HPText.transform.position;

        do
        {
            //時間加算。
            timer = Mathf.Min(1.0f, timer + (Time.deltaTime / 1.5f));
            //移動
            transform.position = Vector3.Slerp(startPos, targetPos, timer);

            yield return new WaitForEndOfFrame();
        } while (timer < 1.0f);
        //ダメージ適用。
        ApplyDamage();
        //非アクティブにする。
        gameObject.SetActive(false);
    }
}

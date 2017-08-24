using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

    Animator _Animator;
    Text _Text;
    private bool _Move = false;

    private void Start()
    {
        _Animator = GetComponent<Animator>();
        _Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        CheckEndAnimation();
    }

    private void CheckEndAnimation()
    {
        if (_Move == false &&
           _Animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Animator.StringToHash("End Animation"))
        {
            _Move = true;
            StartCoroutine(Move());
        }
    }

    //要塞にダメージ適用。
    private void ApplyDamage()
    {
        //要塞検索。
        GameObject.Find(tag).GetComponent<Fortress>().ApplyDamage(Convert.ToInt16(_Text.text));
    }

    private IEnumerator Move()
    {
        float timer = 0.0f;
        Vector3 startPos = transform.position;
        GameObject _Target = GameObject.Find(tag + "HPText");
        Vector3 targetPos = _Target.transform.position;

        while (true)
        {
            timer += Time.deltaTime / 1.5f;
            transform.position = Vector3.Slerp(startPos, targetPos, timer);

            if(timer >= 1.0f)
            {
                ApplyDamage();
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}

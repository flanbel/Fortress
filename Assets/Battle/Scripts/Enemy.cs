using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵。
public class Enemy : MonoBehaviour {

    Fortress _Fortress;

    bool _isRotation;

    private void Start()
    {
        _Fortress = transform.GetComponentInChildren<Fortress>();
        //要塞の情報を設定。
        _Fortress.Info.deck = SaveData.GetClass("BulletsDeck_0", new BulletsDeck());
    }

    private void Update()
    {
        if(_Fortress.armory.Count > 0)
        {
            StartCoroutine(Rotation());
        }
    }

    private IEnumerator Rotation()
    {
        if (!_isRotation)
        {
            _isRotation = true;
            float angle = Random.Range(-20.0f, 0.0f);
            float timer = 0.0f;

            float start = _Fortress.cannon.transform.eulerAngles.z;
            while (180.0f < start)
                start -= 360;

            while (timer < 1.0f)
            {
                //時間加算
                timer = Mathf.Min(1.0f, timer + Time.deltaTime);
                _Fortress.cannon.transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(start, angle, timer));

                yield return new WaitForEndOfFrame();
            }
            //回転終了したら発射。
            _Fortress.Shot(Data.GetBulletInfo(_Fortress.armory[0]), Vector3.left * 100);

            _isRotation = false;
        }
    }
}

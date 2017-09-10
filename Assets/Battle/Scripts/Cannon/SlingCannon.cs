//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;

//public class SlingCannon : Cannon
//{
//    public override void Shot(BulletInfo info, Vector3 vec)
//    {
//        GameObject obj = SetBullet(info);

//        Rigidbody2D rigid = obj.GetComponent<Rigidbody2D>();
//        力を加える。
//        rigid.AddForce(vec, ForceMode2D.Impulse);
//    }


//    //ドラッグの始まった座標。
//    private Vector3 startPos = Vector2.zero;

//    //発射ベクトル。
//    [SerializeField]
//    Vector2 _Vector;

//    [SerializeField]
//    GameObject _Image;

//    public void OnBeginDrag(PointerEventData eventData)
//    {
//        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        _Image.SetActive(true);
//        _Image.transform.localScale = Vector3.one;
//    }

//    public void OnDrag(PointerEventData eventData)
//    {
//        _Vector = startPos - Camera.main.ScreenToWorldPoint(Input.mousePosition);

//        _Image.transform.localScale = new Vector3(1 + _Vector.magnitude / 100, 1);
//        _Image.transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(_Vector.x, -_Vector.y) - 90);
//    }

//    public void OnEndDrag(PointerEventData eventData)
//    {
//        Shot(Data.GetBulletInfo(0));
//        _Image.SetActive(false);
//    }
//}

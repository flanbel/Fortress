using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErasureObject : MonoBehaviour {

    //ゲームオブジェクトを削除
	public void Delete()
    {
        Destroy(gameObject);
    }
    //ゲームオブジェクトを非アクティブにする。
    public void Inactive()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplement : MonoBehaviour {

    //要塞。
    private Fortress _Fortress;

    //補給間隔。
    [SerializeField]
    private float _Interval = 1.0f;
    
    //補給済みかどうか？
    private List<bool> _Supplied = new List<bool>();
    //弾丸の数。
    private int _BulletNum = 0;

	// Use this for initialization
	void Start () {
        //要塞取得。
        _Fortress = GetComponent<Fortress>();

        //デッキに入っている弾丸の数。
        _BulletNum = _Fortress.fortressInfo.deck.bulletNum;
        for (int i = 0; i < _BulletNum; i++)
            _Supplied.Add(false);

        StartCoroutine(Supply());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //補給する。
    public IEnumerator Supply()
    {
        while (true)
        {
            yield return new WaitForSeconds(_Interval);
            //弾薬補給。
            AmmunitionSupplement();
        }
    }

    //補給済みにする。
    private void Supplied(int idx)
    {
        _Supplied[idx] = true;
        int count = 0;
        for (;count < _BulletNum; count++)
        {
            //未補給を探す。
            if (_Supplied[count] == false)
                break;
        }

        //全て補給した。
        if(count == _BulletNum)
        {
            //リセット。
            for(int i = 0; i < _BulletNum; i++)
            {
                _Supplied[i] = false;
            }
        }
    }

    //弾薬補給。
    public void AmmunitionSupplement()
    {
        int idx = 0;
        //未補給であった場合のみ補給。
        do
        {
            //補給する弾薬をランダムで決定。
            idx = Random.Range(0, _BulletNum);
        } while (_Supplied[idx] == true);

        if(_Fortress.AddAmmo(Data.GetBulletInfo(_Fortress.fortressInfo.deck.bullets[idx])))
        {
            Supplied(idx);
        }
    }
}

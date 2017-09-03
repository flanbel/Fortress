using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplement : MonoBehaviour {

    //要塞。
    private Fortress _Fortress;    
    //補給済みかどうか？
    private List<bool> _Supplied = new List<bool>();
    //弾丸の数。
    private int bulletNum { get { return _Fortress.Info.deck.bulletNum; } } 

	// Use this for initialization
	void Start () {
        //要塞取得。
        _Fortress = GetComponent<Fortress>();

        //補給確認リスト制作。
        for (int i = 0; i < bulletNum; i++)
            _Supplied.Add(false);

        //補給開始。
        StartCoroutine(Supplying());
    }

    //一定時間が毎に補給する。
    public IEnumerator Supplying()
    {
        while (true)
        {
            //補給間隔分待つ。
            yield return new WaitForSeconds(_Fortress.Info.param.SupplyInterval);
            //弾薬補給。
            Supply();
        }
    }

    //弾薬補給。
    public void Supply()
    {
        //補給する弾薬の添え字。
        int idx = 0;
        int count = 0;
        //未補給であった場合のみ補給。
        while (_Supplied[idx = Random.Range(0, bulletNum)] == true)
        {
            if (count++ > 1000)
                break;
        }

        //デッキから要塞の弾薬庫へ補給する。
        if (_Fortress.AddAmmo(_Fortress.Info.deck.bullets[idx]))
        {
            //補給済みにする。
            Supplied(idx);
        }
    }

    //補給済みにする。
    //全てが補給済みならリセット。
    private void Supplied(int idx)
    {
        //補給済みにする。
        _Supplied[idx] = true;
        
        //全て補給し終えたか？
        if(_Supplied.TrueForAll((a) => a == true))
        {
            //リセット。
            for (int i=0;i<_Supplied.Count;i++)
            {
                _Supplied[i] = false;
            }
        }
    }
}

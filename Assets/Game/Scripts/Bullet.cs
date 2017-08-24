using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    //弾丸の情報。
    [SerializeField]
    private BulletInfo _BulletInfo;
    public BulletInfo bulletInfo { get { return _BulletInfo; } set { _BulletInfo = value; } }
    
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        CheckLimit();
    }

    //耐久値の減算。
    private void SubEndurance(int sub)
    {
        //減算した結果、耐久値が0以下になったなら。
        if ((_BulletInfo.Endurance -= sub) <= 0)
        {
            _Break();
        }        
    }

    //弾丸破壊。
    private void _Break()
    {
        GameObject bom = Instantiate(Resources.Load("Prefab/Explosion")as GameObject);
        bom.transform.position = transform.position;
        Destroy(gameObject);
    }

    //範囲外に出た。
    private void CheckLimit()
    {
        if (transform.position.x > 200 ||
            transform.position.y < -10)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //異なるタグと衝突した。
        if (tag != collision.gameObject.tag)
        {
            SubEndurance(1);
        }
    }
}

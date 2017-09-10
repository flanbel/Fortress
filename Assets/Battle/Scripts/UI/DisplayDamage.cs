using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDamage : SingletonMonoBehaviour<DisplayDamage> {

    //ダメージを表示するテキストのプレハブ。
    private GameObject _DisplayTextPrefab;
    private GameObject prefab
    {
        get
        {
            if (_DisplayTextPrefab == null)
            {
                _DisplayTextPrefab = Resources.Load("Prefab/UI/DamageText") as GameObject;
            }
            return _DisplayTextPrefab;
        }
    }

    private List<Text> _TextList = new List<Text>();

	// Use this for initialization
	void Start () {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        for(int i = 0; i < 30; i++)
        {
            //生成。
            GameObject damageT = Instantiate(prefab, transform);
            _TextList.Add(damageT.GetComponent<Text>());
            damageT.SetActive(false);
        }
    }

    //ダメージテキストを表示。
    //[in] 表示するダメージ。
    //[in] 表示する場所の基点。
    //[in] タグ。
    public void DisplayDamageText(int damage,Vector3 pos,Fortress fortress)
    {
        //非アクティブなテキストを取得。
        Text text = _TextList.Find((a) => a.gameObject.activeSelf == false);
        text.gameObject.SetActive(true);
        //少しずらす。
        int range = 100;
        text.transform.position = pos + new Vector3(Random.Range(-range, range), Random.Range(-range, range));
        //ダメージ設定。
        text.text = damage.ToString();

        //
        text.gameObject.GetComponent<DamageText>().fortress = fortress;
    }
}

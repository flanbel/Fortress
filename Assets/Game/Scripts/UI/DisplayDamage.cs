using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDamage : MonoBehaviour {

    //ダメージを表示するテキストのプレハブ。
    private GameObject _DisplayTextPrefab;

    //インスタンス。
    private static DisplayDamage _Instance;

    public static DisplayDamage GetInstance()
    {
        if (_Instance == null)
        {
            //生成。
            GameObject obj = new GameObject("DisplayDamage");
            obj.transform.SetParent(GameObject.Find("Canvas").transform);
            obj.transform.SetAsLastSibling();
            _Instance = obj.AddComponent<DisplayDamage>();
        }
        return _Instance;
    }

	// Use this for initialization
	void Awake () {
		if(_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            //削除？
            Destroy(this);
        }
	}

    private void Start()
    {
        _DisplayTextPrefab = Resources.Load("Prefab/UI/DamageText")as GameObject;
    }

    public void CreateDamageText(int damage,Vector3 pos,string tag)
    {
        //生成。
        GameObject TEXT = Instantiate(_DisplayTextPrefab,transform);
        TEXT.tag = tag;
        int range = 30;
        TEXT.transform.position = Camera.main.WorldToScreenPoint(pos) + new Vector3(Random.Range(-range, range), Random.Range(-range, range));

        Text TextC = TEXT.GetComponent<Text>();
        TextC.text = damage.ToString();
    }
}

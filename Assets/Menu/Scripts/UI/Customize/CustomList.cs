using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomList : MonoBehaviour {

    //要素数。
    [SerializeField]
    private int _ItemNum;

    //ページの最大数。
    [SerializeField]
    private int _MaxPageNum;

    //今のページ番号。
    [SerializeField]
    private int _NowPage;
    public int nowPage
    {
        set
        {
            _NowPage = value;
            _PageText.text = _NowPage + "/" + _MaxPageNum;
        }
    }

    //アイテムを生成するコールバック関数。
    public delegate GameObject CreateItemCB(int idx,Transform page);
    private CreateItemCB _CreateItemCB;
    public CreateItemCB createItemCB { set { _CreateItemCB = value; } }

    //ページを表示するテキスト。
    [SerializeField]
    private Text _PageText;

    //ページを登録する親。
    [SerializeField]
    private Transform _PageList; 

	// Use this for initialization
	void Start () {
        //ページ数を切り上げ。
        _MaxPageNum = (int)Mathf.Ceil(_ItemNum / 8.0f);
        nowPage = 1;

        //ページ生成。
        CreatePage();
    }

    //ページを生成する関数。
    private void CreatePage()
    {
        GameObject PagePrefab = Resources.Load("Prefab/UI/CustomPage") as GameObject;

        int Count = 0;
        //ページ分ループする。
        for (int i = 0; i < _MaxPageNum; i++)
        {
            GameObject page = Instantiate(PagePrefab, _PageList);
            page.name = "Page_" + (i + 1).ToString();
            //ページ内のアイテムを生成。
            for (int j = 0; (j < 8) && (Count < _ItemNum); j++, Count++)
            {
                //アイテム生成。
                if (_CreateItemCB != null)
                {
                    _CreateItemCB(Count, page.transform);
                }
                else
                {
                    Instantiate(new GameObject(), page.transform);
                }
            }
        }
    }

    //ページ移動。
    private IEnumerator MovingPage()
    {
        RectTransform rect = _PageList.gameObject.GetComponent<RectTransform>();
        Vector3 start = rect.anchoredPosition3D;
        float timer = 0.0f;
        while ((timer += Time.deltaTime * 3) < 1.0f)
        {
            rect.anchoredPosition3D = Vector3.Lerp(start, new Vector3(-rect.sizeDelta.x * (_NowPage - 1), start.y), timer);
            
            yield return new WaitForEndOfFrame();
        }

        rect.anchoredPosition3D = Vector3.Lerp(start, new Vector3(-rect.sizeDelta.x * (_NowPage - 1), start.y), 1.0f);
    }

    //次のページへ。
    public void NextPage()
    {
        nowPage = (++_NowPage > _MaxPageNum) ? 1 : _NowPage;
        //ページ移動。
        StartCoroutine(MovingPage());
    }
    //前のページへ。
    public void PrevPage()
    {
        nowPage = (--_NowPage <= 0) ? _MaxPageNum : _NowPage;
        //ページ移動。
        StartCoroutine(MovingPage());
    }
}

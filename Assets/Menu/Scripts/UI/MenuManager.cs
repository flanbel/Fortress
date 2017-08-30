using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : SingletonMonoBehaviour<MenuManager>
{

    //切り替え中。
    private bool _Switching = false;
    //現在表示しているメニュー。
    private Menu _Showing = null;

    //利用したメニューの履歴。
    private List<Menu> _MenuHistory = new List<Menu>();

    //タイトルUI。
    private RectTransform _TitleUI;
    //テキスト。
    private Text _TitleText;

    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        //シーンをまたいでも破棄されないように。
        DontDestroyOnLoad(this.gameObject);

        _TitleUI = GameObject.Find("CanvasRoot/Front/Title").GetComponent<RectTransform>();
        _TitleText = _TitleUI.transform.GetChild(0).GetComponent<Text>();

        //子供たち(各メニュー)を非アクティブにする。
        int idx = 0;
        foreach (Menu child in GetComponentsInChildren<Menu>(true))
        {
            child.order = idx++;
            child.gameObject.SetActive(false);
        }

        //とりあえず最初のシーン呼び出し。
        Switch(transform.GetChild(0).GetComponent<Menu>());
    }

    //メニューを切り替える。
    private IEnumerator SwitchingMenu(Menu menu)
    {
        //切り替え開始。
        _Switching = true;
        
        //メニュー表示。
        menu.gameObject.SetActive(true);
        //タイトル設定。
        _TitleText.text = menu.title;

        //移動コルーチンが終わるまで待つ。
        yield return StartCoroutine(MovingMenu(menu));

        //前のメニューを非表示にする。
        if (_Showing)
            _Showing.gameObject.SetActive(false);

        //履歴更新。
        UpdateMenuHistory(menu);

        //メニュー更新。
        _Showing = menu;

        //切り替え終了。
        _Switching = false;
    }

    //メニューの移動。
    private IEnumerator MovingMenu(Menu menu)
    {
        float timer = 0.0f;
        //メニューの幅。
        float width = 510;

        int dir = 1;
        if (_Showing)
            dir = (_Showing.order < menu.order) ? 1 : -1;

        float UIPosY = _TitleUI.anchoredPosition.y;
        //時間の加算。
        while ((timer += Time.deltaTime * 3) < 1.0f)
        {            
            //移動。
            if (_Showing)
                _Showing.transform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(-dir * width, 0), timer);
            menu.transform.localPosition = Vector3.Lerp(new Vector3(dir * width, 0), Vector3.zero, timer);
            //タイトルの移動。
            _TitleUI.anchoredPosition = Vector3.Lerp(new Vector3(-_TitleUI.sizeDelta.x, UIPosY), new Vector3(0, UIPosY), timer);
            
            yield return new WaitForEndOfFrame();
        }

        //移動。
        if (_Showing)
            _Showing.transform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(-width, 0), 1.0f);
        menu.transform.localPosition = Vector3.Lerp(new Vector3(width, 0), Vector3.zero, 1.0f);
        _TitleUI.anchoredPosition = Vector3.Lerp(new Vector3(-_TitleUI.sizeDelta.x, UIPosY), new Vector3(0, UIPosY), 1.0f);

    }

    //履歴。
    private void UpdateMenuHistory(Menu menu)
    {
        //次のメニューがメインメニューかどうか？
        if (menu.isMain)
        {
            //履歴の破棄。
            _MenuHistory.Clear();
        }
        else
        {
            //履歴に追加。
            _MenuHistory.Add(_Showing);
        }
    }

    //キャンバスの切り替えを行う。
    //[in] 切り替えたいキャンバス。
    public void Switch(Menu menu)
    {
        //切り替え中でない &&
        //同じキャンバスでない。
        if (_Switching == false && _Showing != menu)
        {
            StartCoroutine(SwitchingMenu(menu));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : SingletonMonoBehaviour<MenuManager>
{

    //切り替え中。
    private bool _Switching = false;
    //現在表示しているメニュー。
    private Menu _NowMenu = null;

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

    //キャンバスの切り替えを行う。
    //[in] 切り替えたいキャンバス。
    public void Switch(Menu next)
    {
        //切り替え中でない &&
        //同じキャンバスでない。
        if (_Switching == false && _NowMenu != next)
        {
            StartCoroutine(SwitchingMenu(next));
        }
    }

    //メニューを切り替える。
    private IEnumerator SwitchingMenu(Menu next)
    {
        //切り替え開始。
        _Switching = true;
        
        //メニュー表示。
        next.gameObject.SetActive(true);
        //タイトル設定。
        _TitleText.text = next.title;

        //移動コルーチンが終わるまで待つ。
        yield return StartCoroutine(MovingMenu(next));

        //前のメニューを非表示にする。
        if (_NowMenu)
            _NowMenu.gameObject.SetActive(false);

        //履歴更新。
        UpdateMenuHistory(next);

        //メニュー更新。
        _NowMenu = next;

        //切り替え終了。
        _Switching = false;
    }

    //メニューの移動。
    private IEnumerator MovingMenu(Menu next)
    {
        float timer = 0.0f;
        //メニューの幅。
        float width = next.GetComponent<RectTransform>().sizeDelta.x;

        //Rect.
        RectTransform nowR = null, nextR = next.gameObject.GetComponent<RectTransform>();

        //移動する向き
        int dir = 1;
        if (_NowMenu)
        {
            dir = (_NowMenu.order < next.order) ? 1 : -1;
            nowR = _NowMenu.gameObject.GetComponent<RectTransform>();
        }

        float UIPosY = _TitleUI.anchoredPosition.y;
        //時間の加算。
        do
        {
            timer = Mathf.Min(1.0f, (timer + Time.deltaTime * 3));

            //移動。
            if (_NowMenu) nowR.anchoredPosition = Vector3.Lerp(Vector3.zero, new Vector3(-dir * width, 0), timer);
            nextR.anchoredPosition = Vector3.Lerp(new Vector3(dir * width, 0), Vector3.zero, timer);
            //タイトルの移動。
            if(timer <= 0.5f)
            {
                _TitleUI.anchoredPosition = Vector3.Lerp(new Vector3(0, UIPosY), new Vector3(-_TitleUI.sizeDelta.x, UIPosY), timer * 2);
            }
            else
            {
                _TitleUI.anchoredPosition = Vector3.Lerp(new Vector3(-_TitleUI.sizeDelta.x, UIPosY), new Vector3(0, UIPosY), timer);
            }
            //
            yield return new WaitForEndOfFrame();
        } while (timer < 1.0f);
    }

    //履歴。
    private void UpdateMenuHistory(Menu next)
    {
        //次のメニューがメインメニューかどうか？
        if (next.isMain)
        {
            //履歴の破棄。
            _MenuHistory.Clear();
        }
        else
        {
            //履歴に追加。
            _MenuHistory.Add(_NowMenu);
        }
    }
}

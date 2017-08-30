using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryLine : MonoBehaviour {

    //ラインを識別する番号。
    [SerializeField]
    private int _LineID = -1;
    private int lineID
    {
        get
        {
            if(_LineID < 0)
            {
                //自分が何番目の子か取得。
                _LineID = transform.GetSiblingIndex();
                name = "Line_" + _LineID;
            }
            return _LineID;
        }
    }

    //かかる時間。(分)
    private const int _TakeTime = 1;

    //ラインの情報。
    [SerializeField]
    private FactoryLineInfo _LineInfo;
    private FactoryLineInfo lineInfo
    {
        get
        {
            if (_LineInfo == null || _LineInfo.state == FactoryLineInfo.LineState.Initial)
            {
                _LineInfo = SaveData.GetClass("FactoryLine_" + lineID, new FactoryLineInfo());

                if (_LineInfo == null)
                {
                    _LineInfo = new FactoryLineInfo();
                }

                if (_LineInfo.state == FactoryLineInfo.LineState.Initial)
                {
                    _LineInfo.state = FactoryLineInfo.LineState.Idle;
                }
            }
            return _LineInfo;
        }
    }

    private FactoryLineInfo.LineState state
    {
        get { return lineInfo.state; }
        set
        {
            lineInfo.state = value;
            SetTextGUI();
        }
    }

    private int timer
    {
        get { return lineInfo.timer; }
        set
        {
            lineInfo.timer = value;
            SetDisplayGUI();
        }
    }

    public BulletRecipe recipe
    {
        get { return lineInfo.recipe; }
        set
        {
            lineInfo.recipe = value;
            SetIcon();
            SetButtonGUI();
            SetDisplayGUI();
        }
    }

    //コルーチン保持。
    IEnumerator _Coroutine;
    IEnumerator coroutine
    {
        get
        {
            if (_Coroutine == null)
            {
                _Coroutine = TimerDecrease();
            }
            return _Coroutine;
        }
        set
        {
            _Coroutine = value;
        }
    }

    [SerializeField]
    Image _Gauge;

    [SerializeField]
    Text _DisplayText;

    [SerializeField]
    Text _ButtonText;

    [SerializeField]
    Image _Icon;

    [SerializeField]
    Button _Button;

    [SerializeField]
    Toggle _RepeatToggle;

    private void OnEnable()
    {
        //データ読み込み。
        LoadData();
    }

    //非アクティブ。
    private void OnDisable()
    {
        SaveData.SetClass("FactoryLine_" + lineID, _LineInfo);
        //まだ、セーブのタイミングががばい。
        SaveData.Save();
    }

    private void Update()
    {
        //現在の時刻を保存。
        SaveData.SetString("LastTime", DateTime.Now.ToString());
    }

    //リピートを逆転させる。
    public void SetToggle()
    {
        lineInfo.repeat = _RepeatToggle.isOn;
    }

    //データを読み込む。
    private void LoadData()
    {
        if (state > FactoryLineInfo.LineState.Initial)
        {
            //値の変更をGUIに適用。
            SetDisplayGUI();
            SetTextGUI();
            SetIcon();
            SetButtonGUI();
            _RepeatToggle.isOn = lineInfo.repeat;

            if (state == FactoryLineInfo.LineState.Generating)
            {
                //最後にアクセスしていた時間を取得。
                string lastTime = SaveData.GetString("LastTime", null);

                //経過した時間を計算。
                TimeSpan span = DateTime.Now - DateTime.Parse(lastTime);
                //未完成の場合はコルーチンを呼ぶ。
                if (PassageOfTime((int)span.TotalSeconds) == false)
                {
                    StartCoroutine(coroutine);
                }
            }
        }
    }

    //タイマー更新。
    private void SetDisplayGUI()
    {
        float time = _LineInfo.timer;
        //タイマー更新。
        if (time >= 0)
        {
            float max = _TakeTime * 60.0f;
            //ゲージ更新。
            _Gauge.fillAmount = Mathf.Max(0.0f, 1.0f - (time / max));

            _DisplayText.text = (time == 0) ? "完成" : String.Format("{0:00}:{1:00}", Mathf.Floor(time / 60), (time % 60));
        }
        else
        {
            //ゲージ更新。
            _Gauge.fillAmount = 0.0f;
            BulletInfo info = recipe.bulletInfo;
            _DisplayText.text = string.Format("名前：{0}\n生産コスト:{1}\n生産個数：{2:####}", info.Name, recipe.cost, recipe.generateNum);
        }
    }

    //テキスト更新。
    private void SetTextGUI()
    {
        //
        switch (lineInfo.state)
        {
            case FactoryLineInfo.LineState.Initial:
                _ButtonText.text = "準備中";
                break;
            case FactoryLineInfo.LineState.Idle:
                _ButtonText.text = "ＯＫ";
                break;
            case FactoryLineInfo.LineState.Generating:
                _ButtonText.text = "キャンセル";
                break;
            case FactoryLineInfo.LineState.Completed:
                _ButtonText.text = "受け取る";
                break;
        }
    }

    //ボタン更新。
    private void SetButtonGUI()
    {
        int mycost = 0;
        //レシピが設定されていない　||
        //存在しない弾丸。

        _Button.interactable = (recipe != null && recipe.bulletInfo != null);
        
        //コスト比較。
    }

    //アイコン設定。
    private void SetIcon()
    {
        _Icon.sprite = recipe.bulletInfo.Icon;
    }

    //ボタンを押したときの処理。
    public void ButtonProcess()
    {
        switch (state)
        {
            case FactoryLineInfo.LineState.Idle:
                //開始処理。
                InvokeLine();
                break;
            case FactoryLineInfo.LineState.Generating:
                //キャンセル処理。
                StopLine();
                break;
            case FactoryLineInfo.LineState.Completed:
                //弾丸受け取り。
                CompletedLine();
                break;
        }
    }

    //生産ライン起動。
    private void InvokeLine()
    {
        state = FactoryLineInfo.LineState.Generating;
        timer = _TakeTime * 60;
        
        //ストップで止めるためにコルーチン保持。
        coroutine = TimerDecrease();
        //新しいコルーチンを開始。
        StartCoroutine(coroutine);
    }

    //生産中止。
    private void StopLine()
    {
        state = FactoryLineInfo.LineState.Idle;
        timer = -1;
        StopCoroutine(coroutine);
    }

    //生産終了。
    private void CompletedLine()
    {
        state = FactoryLineInfo.LineState.Idle;
        timer = -1;
        StopCoroutine(coroutine);
        //弾丸を追加。
        Data.AddBulletStock(recipe.bulletID, recipe.generateNum);
    }

    //時間を減らす。
    private IEnumerator TimerDecrease()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            PassageOfTime(1);
        }
    }

    //時間を経過させる。
    //[in] 経過した秒数
    //[out] 完成したかどうか
    private bool PassageOfTime(int sub)
    {
        if (state == FactoryLineInfo.LineState.Generating)
        {
            //0未満にはならない。
            timer = Mathf.Max(0, timer - sub);

            //時間経過。
            if (timer <= 0)
            {
                //完成。
                state = FactoryLineInfo.LineState.Completed;
                return true;
            }
        }
        return false;
    }
}

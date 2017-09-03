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

    //生産にかかる時間。(分)
    private const int _TakeTime = 1;

    //ラインの情報。
    [SerializeField]
    private FactoryLineInfo _LineInfo = new FactoryLineInfo();
    private FactoryLineInfo lineInfo
    {
        get
        {
            if (_LineInfo.state == FactoryLineInfo.LineState.Initial)
            {
                _LineInfo = SaveData.GetClass("FactoryLine_" + lineID, new FactoryLineInfo());

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
            SetDisplayGUI();
            SetIcon();
            CheckExecutable();
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
        set { _Coroutine = value; }
    }

    //ボタンが押された時に呼び出される関数。
    delegate void ButtonFunction();
    ButtonFunction _ButtonFunc = null;

    //ラインを構成するUI。
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
    
    //アクティブになった。
    private void OnEnable()
    {
        //データ読み込み。
        LoadData();
    }
    //非アクティブになった。
    private void OnDisable()
    {
        SaveData.SetClass("FactoryLine_" + lineID, _LineInfo);
        //まだ、セーブのタイミングががばい。
        SaveData.Save();
    }

    private void Update()
    {
        //現在の時刻を設定。
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
            SetIcon();
            SetButtonGUI();
            CheckExecutable();
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
        //残り時間を取得。
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
            if (info != null)
            {
                //情報表示。
                _DisplayText.text = string.Format("名前：{0}\n" +
                    "生産コスト:{1}\n" +
                    "生産個数：{2:####}", info.Name, recipe.cost, recipe.generateNum);
            }
            else
            {
                //情報表示。
                _DisplayText.text = "レシピが設定されていません。";
            }
        }
    }

    //ボタンに各ステートに対応した設定をセット。
    private void SetButtonGUI()
    {
        switch (state)
        {
            case FactoryLineInfo.LineState.Initial:
                _ButtonText.text = "準備中";
                break;
            case FactoryLineInfo.LineState.Idle:
                _ButtonText.text = "ＯＫ";
                //生産開始処理。
                _ButtonFunc = InvokeLine;
                break;
            case FactoryLineInfo.LineState.Generating:
                _ButtonText.text = "キャンセル";
                //生産中止処理。
                _ButtonFunc = StopLine;
                break;
            case FactoryLineInfo.LineState.Completed:
                _ButtonText.text = "受け取る";
                //弾丸受け取り。
                _ButtonFunc = CompletedLine;
                break;
        }
    }

    //実行可能かチェック。
    private void CheckExecutable()
    { 
        //存在しない弾丸。
        _Button.interactable = (recipe.bulletInfo != null);
        //(recipe.cost < 0)
    }

    //アイコン設定。
    private void SetIcon()
    {
        if (recipe.bulletInfo != null)
            _Icon.sprite = recipe.bulletInfo.Icon;
    }

    //ボタンを押したときの処理。
    public void ButtonProcess()
    {
        _ButtonFunc();
    }

    //生産ライン起動。
    private void InvokeLine()
    {
        //ステート切り替え。
        ChangeState(FactoryLineInfo.LineState.Generating);
    }

    //生産中止。
    private void StopLine()
    {
        //ステート切り替え。
        ChangeState(FactoryLineInfo.LineState.Idle);
    }

    //生産終了。
    private void CompletedLine()
    {
        //ステート切り替え。
        ChangeState(FactoryLineInfo.LineState.Idle);
        //弾丸を補充。
        Data.AddBulletStock(recipe.bulletID, recipe.generateNum);
    }

    private void ChangeState(FactoryLineInfo.LineState next)
    {
        switch (next)
        {
            case FactoryLineInfo.LineState.Idle:
                //タイマー初期化。
                timer = -1;
                StopCoroutine(coroutine);
                break;
            case FactoryLineInfo.LineState.Generating:
                //タイマー設定。
                timer = _TakeTime * 60;
                //ストップで止めた後も再開できるようにコルーチン保持。
                coroutine = TimerDecrease();
                StartCoroutine(coroutine);
                break;
            case FactoryLineInfo.LineState.Completed:
                timer = 0;
                StopCoroutine(coroutine);
                break;
        }
        lineInfo.state = next;
        SetButtonGUI();
    }

    //時間を減らす。
    private IEnumerator TimerDecrease()
    {
        while (true)
        {
            //1秒待つ。
            yield return new WaitForSecondsRealtime(1);
            //1秒経過させる。
            PassageOfTime(1);
        }
    }

    //時間を経過させる。
    //[in] 経過した秒数
    //[out] 完成したかどうか
    private bool PassageOfTime(int sub)
    {
        //0未満にならない。
        timer = Mathf.Max(0, timer - sub);
        
        if (timer <= 0)
        {
            //完成。
            ChangeState(FactoryLineInfo.LineState.Completed);
            return true;
        }
        return false;
    }
}

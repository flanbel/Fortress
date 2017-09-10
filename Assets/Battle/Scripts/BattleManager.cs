using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//バトルシーンを管理するマネージャー。
public class BattleManager : SingletonMonoBehaviour<BattleManager> {


    //試合終了。
    public void GameOver()
    {
        //FadeManager.Instance.LoadScene("Menu", 2.0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeScene : MonoBehaviour {

    [SerializeField]
    string _SceneName;
    [SerializeField]
    AudioSource _Audio;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Change();
        }
	}

    public void Change()
    {
        if (_Audio)
            _Audio.Play();
        //シーン切り替え。
        FadeManager.Instance.LoadScene(_SceneName, 3.0f);
    }
}

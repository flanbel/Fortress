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
            _Audio.Play();
            FadeManager.Instance.LoadScene(_SceneName,2.0f);
        }
	}
}

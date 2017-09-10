using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchMenuButton : MonoBehaviour {

    [SerializeField]
    private Text _Text;

    [SerializeField]
    private Menu _Menu;

    private void Start()
    {
        _Text.text = _Menu.title;
    }

    //キャンバスの切り替えを行う。
    //[in] 切り替えたいキャンバス。
    public void Switch()
    {
        if (_Menu)
            MenuManager.Instance.Switch(_Menu);
    }
}

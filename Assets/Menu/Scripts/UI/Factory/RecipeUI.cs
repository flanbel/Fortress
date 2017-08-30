using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour {
    //何の弾丸のレシピか示すID。
    [SerializeField]
    private int _BulletID = -1;
    public int bulletID
    {
        set
        {
            _BulletID = value;
            //情報を表示。
            BulletInfo info = Data.GetBulletInfo(_BulletID);
            BulletRecipe recipe = Data.GetBulletRecipe(_BulletID);
            if (info != null)
            {
                _Icon.sprite = info.Icon;
                _Text.text = string.Format(
                    "名前:{0} [生産コスト：{5:####}]\n" +
                    "攻撃力：{1:###} 速度：{2:###}\n" +
                    "耐久値：{3:###} 所持数：{4:###}",
                    info.Name, info.Power, info.Speed, info.Endurance, 1, recipe.cost);
            }
        }
    }

    [SerializeField]
    Image _Icon;

    [SerializeField]
    Text _Text;

    //レシピメニューへの参照。
    [SerializeField]
    private RecipeMenu _RecipeMenu;
    private RecipeMenu recipeMenu
    {
        get
        {
            if(_RecipeMenu == null)
            {
                _RecipeMenu = FindObjectOfType<RecipeMenu>();
            }
            return _RecipeMenu;
        }
    }

    private void Start()
    {
        
    }

    //ボタンを押された時に呼ぶ、レシピセット関数。
    public void SetRecipe()
    {
        BulletRecipe recipe = Data.GetBulletRecipe(_BulletID);
        if (recipe != null)
        {
            recipeMenu.SetRecipe(recipe);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//弾丸のレシピを設定するメニューのスクリプト。
public class RecipeMenu : MonoBehaviour {

    //レシピメニューを開いたラインを保持。
    [SerializeField]
    private FactoryLine _OpenMenuLine;

    //メニューを開く
    public void OpenRecipeMenu(FactoryLine line)
    {
        _OpenMenuLine = line;
        gameObject.SetActive(true);
    }
    //メニューを閉じる。
    public void CloseRecipeMenu()
    {
        _OpenMenuLine = null;
        gameObject.SetActive(false);
    }

    //レシピをセット。
    public void SetRecipe(BulletRecipe recipe)
    {
        _OpenMenuLine.recipe = recipe;
        CloseRecipeMenu();
    }
}

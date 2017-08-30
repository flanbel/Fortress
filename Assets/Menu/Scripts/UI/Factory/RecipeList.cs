using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeList : MonoBehaviour {

    GameObject _RecipeUIPrefab;

	// Use this for initialization
	void Start () {
        _RecipeUIPrefab = Resources.Load("Prefab/UI/Factory/Recipe") as GameObject;
        //レシピUI作成
        BulletRecipe[] Recipes = Data.bulletsRecipe;
        foreach(BulletRecipe recipe in Recipes)
        {
            if(recipe.cost > 0)
            {
                //生成。
                GameObject recipeui = Instantiate(_RecipeUIPrefab,transform);
                RecipeUI ui = recipeui.GetComponent<RecipeUI>();
                //配列の何番目にあるか設定。
                ui.bulletID = recipe.bulletID;
            }
        }
	}
}

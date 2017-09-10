using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Create : Editor
{
    //ファイルパスを取得。
    private static string GetFilePath()
    {
        //フルパス取得。
        string fullPath = EditorUtility.OpenFilePanel("開くファイルを指定してください。", "", "json");

        //Assets より前のパスを無視。
        int idx = fullPath.IndexOf("Assets");
        return fullPath.Substring(idx);
    }

    //JSONファイルを読み込む。
    private static T LoadJSON<T>(string filePath)
    {      
        //JSONテキストをロード。
        TextAsset textAssets = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);

        //JSONをデシリアライズ。
        return JsonUtility.FromJson<T>(textAssets.text);
    }

    //アセットを作って保存するよ。
    private static void CreateAsset(Object asset,string filePath)
    {
        //拡張子を外す。
        string filename = filePath.Replace(".json", "");
        //ScriptedObjectのシリアライズを行ってUnityのアセット化。
        //「SO(ScriptedObjectの略)」は他の外部ファイルと名前を区別するための物。
        AssetDatabase.CreateAsset(asset, filename + "SO" + ".asset");
        AssetDatabase.SaveAssets();
        //変更があったアセットをインポートしなおす。
        AssetDatabase.Refresh();
    }

    //ScriptableObject作るよ。
    private static void CreateScriptableObjectFromJSON<T>(TemplateSO<T> asset)
    {
        //ファイルパス取得。
        string filePath = GetFilePath();
        if (!(string.IsNullOrEmpty(filePath)))
        {
            //Jsonをデシリアライズ。
            //ScriptableObjectを継承したクラスはデシリアライズできないので、
            //中間クラスのJsonに代入するという、ワンクッションを挟む必要がある。
            TemplateSO<T>.Json json = LoadJSON<TemplateSO<T>.Json>(filePath);

            //配列を設定。
            asset.array = json.array;

            //アセット作成。
            CreateAsset(asset, filePath);
        }
    }

    [MenuItem("Assets/CreateScriptableObject/BulletInfo")]
    static void BulletInfo()
    {
        CreateScriptableObjectFromJSON(new BulletInfoSO());
    }

    [MenuItem("Assets/CreateScriptableObject/BulletRecipe")]
    static void BulletRecipe()
    {
        CreateScriptableObjectFromJSON(new BulletRecipeSO());
    }

    [MenuItem("Assets/CreateScriptableObject/FortressParts")]
    static void FortressParts()
    {
        CreateScriptableObjectFromJSON(new FortressPartsSO());
    }
}

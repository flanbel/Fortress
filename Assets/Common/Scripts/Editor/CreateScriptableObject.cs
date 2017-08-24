using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Create : Editor
{
    //JSONファイルを読み込む。
    private static T LoadJSON<T>(string filePath)
    {      
        //JSONテキストをロード。
        TextAsset textAssets = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);

        //JSONをデシリアライズ。
        return JsonUtility.FromJson<T>(textAssets.text);
    }

    private static void CreateAsset(Object asset,string filePath)
    {
        //拡張子を外す。
        string filename = filePath.Replace(".json", "");
        //ScriptedObjectのシリアライズを行ってUnityのアセット化。
        AssetDatabase.CreateAsset(asset, filename + ".asset");
        AssetDatabase.SaveAssets();
        //変更があったアセットをインポートしなおす。
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/CreateScriptableObject/BulletInfo")]
    static void BulletInfo()
    {
        //フルパス取得。
        string fullPath = EditorUtility.OpenFilePanel("開くファイルを指定してください。", "", "json");
        //Assets より前のパスを無視。
        int idx = fullPath.IndexOf("Assets");
        string filePath = fullPath.Substring(idx);
        //Jsonをデシリアライズ。
        //ScriptableObjectを継承したクラスはデシリアライズできないので、
        //中間クラスのJsonに代入するという、ワンクッションを挟む必要がある。
        BulletInfoSO.Json json = LoadJSON<BulletInfoSO.Json>(filePath);

        //初期化する。
        BulletInfoSO asset = new BulletInfoSO()
        {
            //中間クラスから本データへ値をコピー。
            array = json.array
        };
        //アセット作成。
        CreateAsset(asset, filePath);
    }
}

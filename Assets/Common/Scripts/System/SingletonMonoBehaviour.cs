using UnityEngine;

//MonoBehaviourを継承したシングルトンを作るための基底クラス。
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _Instance;
    public static T Instance
    {
        get
        {
            if (_Instance == null)
            {
                //hierarchyにオブジェクトがないか検索。
                _Instance = (T)FindObjectOfType(typeof(T));

                //hierarchyにもなかった。
                if (_Instance == null)
                {
                    //オブジェクト生成。
                    GameObject obj = new GameObject("Singleton" + typeof(T).Name);
                    _Instance = obj.AddComponent<T>();
                }
            }

            return _Instance;
        }
    }

}
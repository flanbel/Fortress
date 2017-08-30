using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ScriptableObjectを楽に作りたい。
//こいつを継承してね。
public class TemplateSO<T> : ScriptableObject {
    //中間クラス
    public class Json
    {
        public T[] array;
    }
    public T[] array;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfoSO : ScriptableObject
{
    //JSON用中間クラス
    public class Json
    {
        public BulletInfo[] array;
    }
    public BulletInfo[] array;
}
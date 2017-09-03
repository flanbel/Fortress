using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Firing : MonoBehaviour
{
    //角度。
    [SerializeField, Range(-80, 80)]
    private float _Angle = 0.0f;
    //距離。
    [SerializeField, Range(100, 800)]
    private float _Dist = 0.0f;
    //発射するパワー。
    [SerializeField]
    private int _Power = 1;
    //重力の影響力。
    [SerializeField]
    private float _GravityScale = 1.0f;
    public float gravityScale { get { return _GravityScale; } }

    //ターゲット。
    [SerializeField]
    private Transform _TargetPoint;

    private Cannon _Cannon;
    private Cannon cannon
    {
        get
        {
            if(_Cannon == null)
            {
                _Cannon = transform.GetComponentInParent<Cannon>();
            }
            return _Cannon;
        }
    }

    private void Start()
    {
        _TargetPoint.gameObject.GetComponent<SpriteRenderer>().enabled = false; 
    }

    //ベクトル取得。
    public Vector3 GetVectorToTarget()
    {
        return (_TargetPoint.position - cannon.firingPos).normalized * _Power;
    }

    //変更を検出。
    void OnValidate()
    {
        if (_TargetPoint && cannon)
        {
            Quaternion q = Quaternion.identity;
            q.eulerAngles = new Vector3(0, 0, _Angle);

            //回転と距離をかける。
            Vector3 vec = (q * Vector3.right) * _Dist;
            _TargetPoint.position = cannon.firingPos + new Vector3(vec.x * transform.lossyScale.x,vec.y);
        }
    }
}
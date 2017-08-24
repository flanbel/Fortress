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
    [SerializeField, Range(5, 100)]
    private float _Dist = 0.0f;
    //重力の影響力。
    [SerializeField]
    private float _GravityScale = 1.0f;
    public float gravityScale { get { return _GravityScale; } }
    //発射口。
    [SerializeField]
    private Transform _FiringPoint;
    public Transform firingPoint { get { return _FiringPoint; } }
    //ターゲット。
    [SerializeField]
    private Transform _TargetPoint;

    private void Start()
    {
        _FiringPoint.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        _TargetPoint.gameObject.GetComponent<SpriteRenderer>().enabled = false; 
    }

    //ベクトル取得。
    public Vector3 GetVectorToTarget()
    {
        return _TargetPoint.position - _FiringPoint.position;
    }

    //変更を検出。
    void OnValidate()
    {
        if (_TargetPoint)
        {
            Quaternion q = Quaternion.identity;
            q.eulerAngles = new Vector3(0, 0, _Angle);

            Vector3 vec = q * Vector3.right;
            _TargetPoint.position = _FiringPoint.position + (vec * _Dist);
        }
    }
}
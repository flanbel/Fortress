using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TitleText : MonoBehaviour {

    Rigidbody2D _Rigid;

    // Use this for initialization
    void Start () {
        _Rigid = GetComponent<Rigidbody2D>();
        _Rigid.simulated = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
           //Excute();
        }
        
	}

    public void Excute()
    {
        _Rigid.simulated = true;
        _Rigid.velocity = Vector2.zero;
        Vector2 vec = new Vector2(transform.position.x, 0).normalized + new Vector2(0, (Random.value * 2) - 1);
        _Rigid.AddForce(vec * 2500, ForceMode2D.Impulse);
    }
}

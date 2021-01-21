using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        Observable.EveryUpdate().Subscribe(t =>
        {
            transform.Translate(transform.forward * Speed * Time.deltaTime);
        }).AddTo(this);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}

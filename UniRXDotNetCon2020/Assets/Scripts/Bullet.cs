using System;
using UnityEngine;
using UniRx;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem bulletTrail;
    [SerializeField]
    private ParticleSystem explodeEfx;
    [SerializeField]
    private Collider collider;

    public Owner Owner;
    public float Speed;
    public int Damage;
    public float LifeTime = 3;


    void Start()
    {
        Observable.EveryUpdate()
         .Subscribe(t => transform.Translate(transform.forward * Speed * Time.deltaTime))
         .AddTo(this);

        Observable.Timer(TimeSpan.FromSeconds(LifeTime))
        .Subscribe(t => Kill())
        .AddTo(this);
    }
    public void Explode()
    {
        bulletTrail.Stop();
        explodeEfx.gameObject.SetActive(true);
        explodeEfx.Play();
        collider.enabled = false;
        float delayDestroy = explodeEfx.main.startLifetime.constant * 2;
        Observable.Timer(TimeSpan.FromSeconds(delayDestroy))
             .Subscribe(t => Kill())
             .AddTo(this);
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}

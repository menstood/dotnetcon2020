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

    private IDisposable updateDP;
    private IDisposable autoKillDP;
    private IDisposable deleyExplodeDp;
    public Owner Owner;
    public float Speed;
    public int Damage;
    public float LifeTime = 3;


    // Start is called before the first frame update
    void Start()
    {
        updateDP = Observable.EveryUpdate()
        .Subscribe(t => transform.Translate(transform.forward * Speed * Time.deltaTime))
        .AddTo(this);

        autoKillDP = Observable.Timer(TimeSpan.FromSeconds(LifeTime))
        .Subscribe(t => Kill())
        .AddTo(this);
    }
    public void Explode()
    {
        bulletTrail.Stop();
        explodeEfx.Play();
        collider.enabled = false;
        float delayDestroy = explodeEfx.main.startLifetime.constant * 2;
        deleyExplodeDp = Observable.Timer(TimeSpan.FromSeconds(delayDestroy))
            .Subscribe(t => Kill())
            .AddTo(this);
    }

    private void Kill()
    {
        //Dispose();
        Destroy(gameObject);
    }
    private void Dispose()
    {
        updateDP.Dispose();
        autoKillDP.Dispose();
        deleyExplodeDp?.Dispose();
    }
}

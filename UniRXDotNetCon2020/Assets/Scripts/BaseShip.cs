using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;

public abstract class BaseShip : MonoBehaviour
{
    [SerializeField]
    protected string bulletTag = "Bullet";
    [SerializeField]
    protected Bullet bulletTemplate;
    [SerializeField]
    protected Transform gunLocator;
    [SerializeField]
    protected Transform shipModel;
    //[SerializeField]
    //private Collider shipCollider;
    [SerializeField]
    private ParticleSystem explode;
    [SerializeField]
    private Owner owner;

    private IDisposable triggerObserver;
    protected GameData gameData;

    protected virtual void Start()
    {
        triggerObserver = this.OnTriggerEnterAsObservable()
            .Where(collision => collision.CompareTag(bulletTag))
            .Select(bulletObject => bulletObject.GetComponent<Bullet>())
            .Where(bullet => bullet.Owner != owner)
            .Subscribe(bullet =>
            {
                GetHit(bullet);
                bullet.Explode();
            }).AddTo(this);

        //When we dont want to use this event anymore
        //triggerObserver.Dispose();
    }
    public abstract void GetHit(Bullet bullet);

    #region Old Fashion Way

    //bool isGethit;

    //protected virtual void OnTriggerEnter(Collider col)
    //{
    //    if (!isGethit)
    //    {
    //        if (col.CompareTag(bulletTag))
    //        {
    //            Bullet bullet = col.GetComponent<Bullet>();
    //            if (bullet.Owner != owner)
    //            {

    //                GetHit(bullet);
    //                isGethit = true;
    //                bullet.Explode();
    //            }
    //        }
    //    }
    //}
    #endregion

    public virtual void Init(GameData gameData)
    {
        this.gameData = gameData;
    }

    protected virtual void Fire()
    {
        Bullet bullet = Instantiate(bulletTemplate, gunLocator.position, gunLocator.rotation);
        bullet.gameObject.SetActive(true);
    }
    protected virtual void Dead()
    {
        triggerObserver?.Dispose();
        // shipCollider.enabled = false;
        explode.Play();
        float delayKill = explode.main.duration;
        Observable.Timer(TimeSpan.FromSeconds(delayKill))
            .Subscribe(t => Kill())
            .AddTo(this);

    }
    protected virtual void Kill()
    {
        Destroy(gameObject);
    }




}

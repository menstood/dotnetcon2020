using System;
using UnityEngine;
using UniRx;

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
    [SerializeField]
    private Collider shipCollider;
    [SerializeField]
    private ParticleSystem explode;
 
    [SerializeField]
    private Owner owner;

    protected GameData gameData;

    protected abstract void Start();
    public abstract void GetHit(Bullet bullet);

    protected virtual void OnTriggerEnter(Collider col)
    {
        if (col.tag == bulletTag)
        {
            Bullet bullet = col.GetComponent<Bullet>();
            if (bullet.Owner != owner)
            {
                GetHit(bullet);
                bullet.Explode();
            }
        }
    }

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
        shipCollider.enabled = false;
        explode.Play();
        float delayKill = explode.main.duration ;
        Observable.Timer(TimeSpan.FromSeconds(delayKill))
            .Subscribe(t => Kill())
            .AddTo(this);

    }
    protected virtual void Kill()
    {
        Destroy(gameObject);
    }
}

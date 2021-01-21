using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    protected GameData gameData;

    protected abstract void Start();
    public abstract void GetHit(Bullet bullet);

    protected virtual void OnTriggerEnter(Collider col)
    {
        if (col.tag == bulletTag)
        {
            Bullet bullet = col.GetComponent<Bullet>();
            GetHit(bullet);
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

}

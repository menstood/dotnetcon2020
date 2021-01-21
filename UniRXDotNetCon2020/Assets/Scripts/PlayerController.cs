using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
public class PlayerController : BaseShip
{

    [SerializeField]
    private ParticleSystem explode;
    public float BulletPerSecond;
    public float Speed;

    protected override void Start()
    {
        DateTimeOffset lastFire;
        Observable.EveryUpdate()
            .Where(t => Input.GetKey(KeyCode.Space))
            .Timestamp()
            .Where(t => t.Timestamp > lastFire.AddSeconds(BulletPerSecond))
            .Subscribe(t=>
            {
                Fire();
                lastFire = t.Timestamp;
            })
            .AddTo(this);
    }

    public override void Init(GameData gameData)
    {
        base.Init(gameData);
        gameData.PlayerHP
            .Where(hp => hp <= 0)
            .Subscribe(hp => Dead())
            .AddTo(this);
    }

    private void Dead()
    {
        explode.Play();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(horizontal * Time.deltaTime * Speed, 0f, 0f);
        shipModel.rotation = Quaternion.Euler(0, shipModel.rotation.eulerAngles.y, Mathf.Floor(horizontal) * 45f);
    }

    protected override void Fire()
    {
        base.Fire();
    }
    public override void GetHit(Bullet bullet)
    {
        gameData.HpChange(-bullet.Damage);
    }
}

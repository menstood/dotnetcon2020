using System;
using UniRx;
using UnityEngine;

public class EnemyController : BaseShip
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int score;


    protected override void Start()
    {
        DateTimeOffset lastFire;
        Observable.EveryUpdate()
                 .Timestamp()
                 .Where(t => t.Timestamp > lastFire.AddSeconds(fireRate))
                 .Subscribe(t =>
                 {
                     Fire();
                     lastFire = t.Timestamp;
                 });
    }

    public override void GetHit(Bullet bullet)
    {
        gameData.AddScore(score);
    }
}

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
    private float steeringSpeed;
    [SerializeField]
    private int score;

    [SerializeField]
    private float range;

    private float startXPos;

    protected override void Start()
    {
        startXPos = transform.position.x;
        DateTimeOffset lastFire;
        Observable.EveryUpdate()
                .Do(t => Move())
                 .Timestamp()
                 .Where(t => t.Timestamp > lastFire.AddSeconds(fireRate) && hp>0)
                 .Subscribe(t =>
                 {
                     Fire();
                     lastFire = t.Timestamp;
                 })
                 .AddTo(this);

    }

    public override void GetHit(Bullet bullet)
    {
        hp -= bullet.Damage;
        if (hp <= 0)
        {
            Dead();
            gameData.AddScore(score);
        }
    }


    void Move()
    {
        var x = startXPos + (Mathf.Sin(Time.time * steeringSpeed) * range);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        transform.Translate(-transform.forward * speed * Time.deltaTime);
        //transform.Translate(x, 0, -speed* Time.deltaTime);
    }
}

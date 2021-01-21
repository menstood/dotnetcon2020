using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameData 
{
    public int PlayerMaxHP = 100;
    public ReactiveProperty<int> PlayerHP;
    public ReactiveProperty<int> Score; // Subscribe ครั้งแรกจะได้ Value : Notify เมื่อมีค่าใหม่ != ค่าปัจจุบัน

    public GameData()
    {
        PlayerHP = new ReactiveProperty<int>(PlayerMaxHP);
        Score = new ReactiveProperty<int>(0);
    }

    public void HpChange(int value)
    {
        PlayerHP.Value += value;
    }

    public void AddScore(int value)
    {
        Score.Value += value;
    }
}

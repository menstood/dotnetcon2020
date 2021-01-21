using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameData 
{
    public int PlayerMaxHP = 100;
    public ReactiveProperty<int> PlayerHP;
    public ReactiveProperty<int> Score; // Subscribe ครั้งแรกจะได้ Value : Notify เมื่อมีค่าใหม่ != ค่าปัจจุบัน
    public ReactiveProperty<bool> isPause;

    public GameData()
    {
        PlayerHP = new ReactiveProperty<int>(PlayerMaxHP);
        Score = new ReactiveProperty<int>(0);
        isPause = new ReactiveProperty<bool>(true);
    }

    public void HpChange(int value)
    {
        PlayerHP.Value += value;
    }

    public void AddScore(int value)
    {
        Score.Value += value;
    }

    public void TogglePause()
    {
        isPause.Value = !isPause.Value;
    }
    public void Dispose()
    {
        PlayerHP.Dispose();
        Score.Dispose();
        isPause.Dispose();
    }
}

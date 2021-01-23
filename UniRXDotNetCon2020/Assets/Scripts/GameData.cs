using UniRx;

public class GameData
{
    public int PlayerMaxHP = 100;
    public ReactiveProperty<Unit> Gamestart;
    public ReactiveProperty<int> PlayerHP;
    public ReactiveProperty<int> Score;
    public Subject<Unit> OnGameEnd;
    public bool isGameEnd;

    public GameData()
    {
        PlayerHP = new ReactiveProperty<int>(PlayerMaxHP);
        Score = new ReactiveProperty<int>(0);
        OnGameEnd = new Subject<Unit>();
        PlayerHP.Where(hp => hp <= 0)
            .Subscribe(x =>
            {
                OnGameEnd.OnNext(Unit.Default);
                isGameEnd = true;
            });
    }

    public void HpChange(int value)
    {
        PlayerHP.Value += value;

        #region Old Fashion Way
        //UIManager.UpdateHPLabel(PlayerHP,PlayerMaxHP);
        #endregion
    }

    public void AddScore(int value)
    {
        Score.Value += value;
    }

    public void Dispose()
    {
        PlayerHP.Dispose();
        Score.Dispose();
        OnGameEnd.Dispose();
    }
}

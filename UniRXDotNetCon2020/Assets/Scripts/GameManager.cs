using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameData GameData;
    public UIManager UIManager;
    public PlayerController PlayerController;
    public EnemySpawner EnemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        GameData = new GameData();
        UIManager.Init(GameData);
        PlayerController.Init(GameData);
        EnemySpawner.Init(GameData);
        Observable.EveryUpdate()
            .Where(t => GameData.isPause.Value)
            .Where(t => Input.anyKey)
            .Subscribe(t =>
            {
                GameData.TogglePause();
            })
            .AddTo(this);
    }


}

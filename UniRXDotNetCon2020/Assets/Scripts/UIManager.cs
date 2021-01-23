using UniRx;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private Text hpLabel;
    [SerializeField]
    private Text scoreLabel;
    [SerializeField]
    private GameOverPanel gameOverPanel;

    public void Init(GameData gamedata)
    {
        int maxHp = gamedata.PlayerMaxHP;
        hpSlider.maxValue = maxHp;

        gamedata.PlayerHP
            .Subscribe(hp =>
            {
                hpSlider.value = hp;
                hpLabel.text = string.Format("{0}/{1}", hp, maxHp);
            });

        gamedata.Score
            .Subscribe(score =>
            {
                scoreLabel.text = string.Format("Score : {0}", score);
            });

        gamedata.OnGameEnd
            .Subscribe(isPause =>
            {
                gameOverPanel.gameObject.SetActive(true);
                gameOverPanel.Init(gamedata.Score.Value);
            });
    }


    #region OldFashion Way



    //public static void UpdateScore(int score)
    //{
    //    //scoreLabel.text = string.Format("Score : {0}", score);
    //}





    #endregion
}

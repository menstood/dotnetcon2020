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

    public void Init(GameData gamedata)
    {
        int maxHp = gamedata.PlayerMaxHP;
        hpSlider.maxValue = maxHp;
        gamedata.PlayerHP.Subscribe(hp =>
        {

          
            hpSlider.value = hp;
            hpLabel.text = string.Format("{0}/{1}", hp, maxHp);
        });
        gamedata.Score.Subscribe(score =>
        {
            scoreLabel.text = string.Format("Score : {0}", score);
        });

    }
}

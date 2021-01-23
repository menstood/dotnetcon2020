using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private Text scoreLabel;

    public void Init(int score)
    {
        scoreLabel.text = string.Format("Your score is {0}", score);
    }
}

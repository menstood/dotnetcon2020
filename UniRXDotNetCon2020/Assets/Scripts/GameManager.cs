using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameData GameData;
    public UIManager UIManager;
    public PlayerController PlayerController;
    public EnemyController EnemyController;
    // Start is called before the first frame update
    void Start()
    {
        GameData = new GameData();
        UIManager.Init(GameData);
        PlayerController.Init(GameData);
        EnemyController.Init(GameData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

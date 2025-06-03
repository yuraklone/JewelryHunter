using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static string gameState; //ゲームの状態を管理する ※静的変数

    public GameObject stageTitle; //ステージタイトルのUIオブジェクト
    public Sprite gameClearSprite; //ゲームクリアの絵
    public Sprite gameOverSprite; //ゲームオーバーの絵

    public GameObject buttonPanel; //ボタンパネルのUIオブジェクト


    // Start is called before the first frame update
    void Start()
    {
        //スタートと同時にステータスをplayingにする
        gameState = "playing";

        Invoke("InactiveImage", 1.0f); //第一引数に指定したメソッドを第二引数秒後に発動
        
        buttonPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム状態がクリアorオーバーの時、
        if (gameState == "gameclear" || gameState == "gameover")
        {
            //ステージタイトルを復活
            stageTitle.SetActive(true);
            stageTitle.GetComponent<Image>().sprite = gameClearSprite;


            //ボタンの復活
            buttonPanel.SetActive(true);
        }

        if(gameState == "gameclear")
        {
            stageTitle.GetComponent<Image>().sprite = gameClearSprite;
        }
        
        if (gameState == "gameover")
        {
            stageTitle.GetComponent<Image>().sprite = gameOverSprite;
        }
    }

    void InactiveImage()　//ステージタイトルを非表示にするメソッド
    {
        stageTitle.SetActive(false); //オブジェクトを非表示
    }
}

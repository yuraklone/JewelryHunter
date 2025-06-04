using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static string gameState; //ゲームの状態を管理する ※静的変数

    public GameObject stageTitle; //ステージタイトルのUIオブジェクト
    public Sprite gameClearSprite; //ゲームクリアの絵
    public Sprite gameOverSprite; //ゲームオーバーの絵

    public GameObject buttonPanel; //ボタンパネルのUIオブジェクト
    public GameObject restartButton; //リスタートボタン
    public GameObject nextButton; //ネクストボタン

    TimeController timeCnt; //TimeControllerスクリプトを取得する

    public TextMeshProUGUI timeText; //TimeTextオブジェクトが持っているTextMeshProコンポーネントを取得

    // Start is called before the first frame update
    void Start()
    {
        //スタートと同時にステータスをplayingにする
        gameState = "playing";

        Invoke("InactiveImage", 1.0f); //第一引数に指定したメソッドを第二引数秒後に発動
        
        buttonPanel.SetActive(false);

        //TiomeContorollerコンポーネントの情報を取得
        timeCnt = GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == "playing")
        {
            //カウントダウンの変数currentTimeをUIに連動
            
            //timeCntのcurrentTime(float型)をint型に変換し、
            //ToStringでStringに変換してからtimeText(TextMeshPro)のtext欄に代入
            timeText.text = ((int)timeCnt.currentTime).ToString();

            //もしcurrentTimeが0になったらゲームオーバー
            if(timeCnt.currentTime <= 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player"); //プレイヤーを探す
                //PlayerControllerコンポーネントのGameOverメソッドを発動
                player.GetComponent<PlayerController>().GameOver(); 
            }

        }

        //ゲーム状態がクリアorオーバーの時、
        else if (gameState == "gameclear" || gameState == "gameover")
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
            //restartButtonオブジェクトが持っているButtonコンポーネントの値であるinteractableをfalse→機能を停止
            restartButton.GetComponent<Button>().interactable = false;


        }
        
        if (gameState == "gameover")
        {
            stageTitle.GetComponent<Image>().sprite = gameOverSprite;
            //nextButtonオブジェクトが持っているButtonコンポーネントの値であるinteractableをfalse→機能を停止
            nextButton.GetComponent<Button>().interactable = false;
        }
    }

    void InactiveImage()　//ステージタイトルを非表示にするメソッド
    {
        stageTitle.SetActive(false); //オブジェクトを非表示
    }
}

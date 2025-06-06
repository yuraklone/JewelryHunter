using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool isToRight; //true = 右向き、false = 左向き
    int groundContactCount; //地面にどのくらい接触したか　
    
    public LayerMask groundLayer;

    public bool isTimeTurn; //時間で反転させるかどうかのフラグ
    public float turnTime; //反転のインターバル
    float pastTime; //経過時間

    // Start is called before the first frame update
    void Start()
    {
        if(isToRight == true)
        {
            transform.localScale = new Vector2(-1, 1);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeTurn == true)
        {
            pastTime += Time.deltaTime;
            if (pastTime >= turnTime)
            {
                pastTime = 0;
                Turn();
            }
        }
    }

    private void FixedUpdate()
    {
        if(GameController.gameState == "gameClear")
        {
            return;
        }


        bool onGround = Physics2D.CircleCast
            (
            transform.position, //センサーの発生位置
            0.5f, //円の半径
            Vector2.down,//どの方角に向けるか
            0, //センサーを飛ばす距離
            groundLayer //調査対象のレイヤー
            );

        if(onGround== true)
        {
            Rigidbody2D rbody = GetComponent<Rigidbody2D>();
            if(isToRight == true)
            {
                rbody.velocity = new Vector2(speed, rbody.velocity.y);
            }
            else
            {
                rbody.velocity = new Vector2(-speed, rbody.velocity.y);
            }


        }
        
    }

    void Turn() //右か左かの切り替えメソッド
    {
        isToRight = !isToRight;

        if (isToRight)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2 (1, 1);
        }

    }
    //Groundタグ以外のものとぶつかったら反転
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Ground")
        {
            Turn();　//何かとぶつかったら反転
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundContactCount++; //新しい地面と接触したら1カウントプラス
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundContactCount--; //何かの地面から離れたら1カウントマイナス

            if (groundContactCount <= 0)  //マイナスした結果、新たに接触する地面もないとき
            {
                groundContactCount = 0; //念のためカウントを０に戻す
                Turn(); //崖際と思われるので反転
            }


        }
    }


}

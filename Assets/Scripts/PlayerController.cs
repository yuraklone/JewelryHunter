using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float axisH; //左右のキーの値を格納する変数
    Rigidbody2D rbody; //Rigidbody2Dを扱うための媒体
    Animator animator; //Animatorの情報を扱うための媒体
    public float speed = 3.0f; //歩くスピード
    public float jump; //ジャンプ力
    bool isJump; //ジャンプ中かどうか
    bool onGround; //地面判定
    public LayerMask groundLayer; //地面判定の対象のレイヤーが何かを決めておく

    AudioSource audio;
    public AudioClip jumpSE;

    bool isMobileInput;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerについているRigidbody2Dコンポーネントを変数rbodyに宿す
        rbody = GetComponent<Rigidbody2D>();
        //PlayerについているAnimatorコンポーネントを変数animatorに宿す
        animator = GetComponent<Animator>();

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.gameState != "playing") //状態がplayingでなければ
        {
            return;　//Updateの処理を強制終了
        }

        //左右のキーが押されたら、どちらの値緒だったのかをaxisHに格納
        //Horisontal：水平方向のキーが押された場合の引数
        //左なら-1,右なら1,何も押されていないなら0を返すメソッド

        if (!isMobileInput)
        {
            axisH = Input.GetAxisRaw("Horizontal");
        }


        //もしaxisHが正なら右向き
        if(axisH > 0)
        {
            transform.localScale = new Vector3(1,1,1);
            //this.gameObject.GetComponent<Transform>().localScaleの略形
            animator.SetBool("run", true); //担当しているコント委ローラーのパラメータを変える
        }


        //もしaxisHが負なら左向き
        else if(axisH < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("run", true); //担当しているコント委ローラーのパラメータを変える
        }

        else
        {
            animator.SetBool("run", false); //担当しているコント委ローラーのパラメータを変える
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }



    private void FixedUpdate()
    {
        if(GameController.gameState != "playing")
        {
            return;
        }

        //地面にいるかどうかをサークルキャストを使って判別
        onGround = Physics2D.CircleCast
            (
            transform.position, //Playerの基準点 
            0.2f, //チェック円の半径
            Vector2.down, //指定した点からのチェック方向:down=Vector2(0,-1)
            0.0f, //指定した点からのチェック距離
            groundLayer //指定したレイヤー
            );

        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
    
        //ジャンプ中フラグが立ったらAddForceメソッドによって上に押し出す
        if (isJump) //==trueは省略できる
        {
            rbody.AddForce(new Vector2(0,jump), ForceMode2D.Impulse);
            isJump = false;
        }
    
    }

    public void Jump()
    {
        if (onGround)
        {
            isJump = true;
            animator.SetTrigger("jump"); //ジャンプアニメのためのトリガー発動
            audio.PlayOneShot(jumpSE);
        }
    }
    //何かとぶつかったら発動するメソッド
    //ぶつかった相手のCollider情報を引数collisionに入れる
    //※相手にColliderがついていないと意味がない、かつ相手のColliderがIsTriggerであること
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        if(collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
        if (collision.gameObject.tag == "Item") //Itemに触れたとき
        {
            ItemData itemData = collision.gameObject.GetComponent<ItemData>(); //ぶつかったItemのスクリプトを取得
            //ぶつかったItemのスクリプトに記されているvalueの値をstageScoreに加算
            GameController.stageScore += itemData.value;
            Destroy(collision.gameObject); //ポイントを取ったアイテムの本体を消す
        }

    }
    public void Goal()
    {
        GameController.gameState = "gameclear";
        animator.SetBool("gameClear", true); //PlayerClearアニメをON
        PlayerStop();
    }

    public void GameOver()
    {
        GameController.gameState = "gameover";
        animator.SetBool("gameOver",true); //PlayerClearアニメをON
        PlayerStop();

        //プレイヤーを上に跳ね上げる
        rbody.AddForce(new Vector2(0,5) , ForceMode2D.Impulse);

        //当たり判定もカット
        GetComponent<CapsuleCollider2D>().enabled = false;


    }
    //プレイヤーの動きを停止
    public void PlayerStop()
    {
        //速度を0にして止める
        rbody.velocity = new Vector2(0, 0);
    }

    public void MobileAxis(float axis)
    {
        axisH = axis; //MobileStick.cs経由で与えられた引数xの値が入る(1か-1か)
        
        //axisに値が入っていればUIが触れている→モバイル入力フラグをON
        if(axisH ==0) isMobileInput = false;
        else isMobileInput = true;
    }


}

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
    bool isJump; //ジャンプ中かどうか
    bool onGround; //地面判定
    public LayerMask groundLayer; //地面判定の対象のレイヤーが何かを決めておく
    public float jump; //ジャンプ力

    // Start is called before the first frame update
    void Start()
    {
        //PlayerについているRigidbody2Dコンポーネントを変数rbodyに宿す
        rbody = GetComponent<Rigidbody2D>();
        //PlayerについているAnimatorコンポーネントを変数animatorに宿す
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //左右のキーが押されたら、どちらの値緒だったのかをaxisHに格納
        //Horisontal：水平方向のキーが押された場合の引数
        //左なら-1,右なら1,何も押されていないなら0を返すメソッド
        axisH = Input.GetAxisRaw("Horizontal");

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
        }
    }
    //何かとぶつかったら発動するメソッド
    //ぶつかった相手のCollider情報を引数collisionに入れる
    //※相手にColliderがついていないと意味がない、かつ相手のColliderがIsTriggerであること
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            GameController.gameState = "gameclear";
        }
        if(collision.gameObject.tag == "Dead")
        {
            GameController.gameState = "gameover";
        }
    }


}

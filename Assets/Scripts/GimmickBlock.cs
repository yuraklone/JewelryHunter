using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    public float length = 0.0f; //自動落下検知
    public bool isDelete = false; //落下後の削除するかどうかのフラグ
    public GameObject deadObj; //死亡に関するあたり判定
    bool isFell = false; //落下したのち消滅させるかどうかのフラグ
    float fadeTime = 0.5f; //消えるまでの時間


    Rigidbody2D rbody; //Rigidbody2Dコンポーネント情報の取得
    GameObject player; //Player情報の取得
    float distance; //ブロックとプレイヤーの距離の取得

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();　//rbodyに該当するRigidbody2D情報を取得
        rbody.bodyType = RigidbodyType2D.Static; //RigidbodyType2d.Staticを代入し、挙動を停止
        deadObj.SetActive(false); //deadObjを非アクティブにする

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //プレイヤー情報を取得
        if (player != null) //もしプレイヤーがnullでないなら
        {
            //プレイヤーとブロックの距離を取得する
            distance = Vector2.Distance(this.transform.position , player.transform.position);
            if (distance <= length )　//感知範囲内に入ったら
            {
                if (rbody.bodyType == RigidbodyType2D.Static)
                {
                    rbody.bodyType = RigidbodyType2D.Dynamic; //重力を復活させて落下
                    deadObj.SetActive(true); //当たったら死亡判定も復活

                }
                
                
            }


        }

        if(isFell == true)
        {
            fadeTime -= Time.deltaTime;
            Color col = GetComponent<SpriteRenderer>().color;
            col.a = fadeTime; //透明度を0に向かうfadeTimeとリンク
            GetComponent<SpriteRenderer>().color = col; 
            if(fadeTime <= 0.0f)
            {
                Destroy(this.gameObject); //指定したオブジェクトを消滅　※プレイ中
            }

        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDelete == true)
        {
            isFell = true;
        }
    }


    void OnDrawGizmosSelected()
    {
        //編集画面でlengthの半径の円を描く
        Gizmos.DrawWireSphere(transform.position, length);
    }


}

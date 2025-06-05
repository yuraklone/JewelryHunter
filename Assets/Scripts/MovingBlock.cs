using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 3.0f; //X方向への移動距離
    public float moveY = 3.0f; //Y方向への移動距離
    public float times = 3.0f; //何秒で移動するか
    public float wait = 1.0f; //折り返しのインターバル

    float distance; //開始地点と移動予定地点の差
    float secoindsDistance; //1秒あたりの移動予定距離
    float framsDistance; //1フレームあたりの移動距離
    float movePercentage = 0; //目標までどのくらい動いているのか

    bool isCanMove = true; //動いてOKかどうかのフラグ
    Vector3 startPos; //ブロックの初期位置
    Vector3 endPos; //移動後の予定位置
    bool isReverse; //方向反転フラグ


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; //初期位置を代入
        endPos = new Vector2(startPos.x + moveX, startPos.y + moveY);

    }

    // Update is called once per frame
    void Update()
    {
        if (isCanMove == false)
        {
            return;
        }

        else
        {
            distance = Vector2.Distance(startPos, endPos); //初期位置と移動後位置の距離を代入
            secoindsDistance = distance / times ; //移動する秒速を算出、代入
            framsDistance = secoindsDistance*Time.deltaTime; //1フレームあたりの移動距離を算出
            movePercentage += framsDistance / distance;

            if(isReverse == false)
            {
                transform.position = Vector2.Lerp(startPos, endPos, movePercentage);
            }
            else
            {
                transform.position = Vector2.Lerp(endPos, startPos, movePercentage);
            }

            if (movePercentage >= 1)
            {
                isCanMove = false; //動きを止める
                isReverse = !isReverse; //移動方向フラグを反転
                movePercentage = 0;
                Invoke("Move", wait);


            }

        }

    }

    public void Move()
    {
        isCanMove = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Playerの親を自分に指定
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Playerの親をなくす＝nullで解放
            collision.transform.SetParent(null);
        }
    }

    //移動範囲表示
    void OnDrawGizmosSelected()
    {
        Vector2 fromPos;
        if (startPos == Vector3.zero)
        {
            fromPos = transform.position;
        }
        else
        {
            fromPos = startPos;
        }
        //移動線
        Gizmos.DrawLine(fromPos, new Vector2(fromPos.x + moveX, fromPos.y + moveY));
        //スプライトのサイズ
        Vector2 size = GetComponent<SpriteRenderer>().size;
        //初期位置
        Gizmos.DrawWireCube(fromPos, new Vector2(size.x, size.y));
        //移動位置
        Vector2 toPos = new Vector3(fromPos.x + moveX, fromPos.y + moveY);
        Gizmos.DrawWireCube(toPos, new Vector2(size.x, size.y));
    }　



}

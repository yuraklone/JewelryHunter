using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float axisH; //左右のキーの値を格納する変数
    Rigidbody2D rbody; //Rigidbody2Dを扱うための媒体
    public float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerについているRigidbody2Dコンポーネントを変数rbodyに宿す
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //左右のキーが押されたら、どちらの値緒だったのかをaxisHに格納
        //Horisontal：水平方向のキーが押された場合の引数
        //左なら-1,右なら1,何も押されていないなら0を返すメソッド
        axisH = Input.GetAxisRaw("Horisontal");

        //velocityに2軸の方向データ(Vector2)を代入
        rbody.velocity = new Vector2(axisH * speed,rbody.velocity.y);

    }

    private void FixedUpdate()
    {
        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
    }
}

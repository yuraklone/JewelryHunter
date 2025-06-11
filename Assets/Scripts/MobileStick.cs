using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileStick : MonoBehaviour
{
    public float MaxLength = 0.5f; //タブが届く最大距離
    PlayerController player; //PlayerControllerスクリプトの情報

    Vector2 defPos; //モバイルスティックの初期座標
    Vector2 downPos; //タップした時の座標

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーについているコンポーネントを取得
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //モバイルスティックの初期座標
        defPos = this.gameObject.GetComponent<RectTransform>().localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PadDown()
    {
        //タップした座標
        downPos = Input.mousePosition;
    }

    public void PadDrag()
    {
        //ずらした段階でのタップの座標
        Vector2　mouseposition = Input.mousePosition;
        
        //新しいタブの位置(どれだけずらしたか)を求める
        Vector2 newTabPos = mouseposition - downPos;
        

        //方向ベクトルを作成
        Vector2 axis = newTabPos.normalized; //正規化する

        newTabPos.y = 0;


        //パッドが移動限界で留まるようにする
        float len = Vector2.Distance(defPos, newTabPos);
        if(len > MaxLength)
        {
            newTabPos.y = axis.x * MaxLength;
        }

        //実際にスティックを移動させる
        GetComponent<RectTransform>().localPosition = newTabPos;

        player.MobileAxis(axis.x);

    }

    public void PadUp()
    {
        //指が離れたら最初の場所に戻る
        GetComponent<RectTransform>().localPosition = defPos;   

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{


    public float deleteTime = 3.0f; //消滅するまでの時間設定
    public bool isDelete; //ぶつかったら消えるかどうかのフラグ 　



    // Start is called before the first frame update
    void Start()
    {

        //時間差で消滅
        Destroy(gameObject, deleteTime); //deleteTime後に消滅
    }

    // Update is called once per frame
    void Update()
    {
        if(isDelete == true)
        {
            Destroy(gameObject);
        }

    }
}


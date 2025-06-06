using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float leftLimit; //カメラの左の移動限界
    public float  rightLimit; //カメラの右の移動限界
    public float topLimit; //カメラの上の移動限界
    public float bottomLimit; //カメラの下の移動限界

    public bool isForceScrollX; //強制スクロールフラグ
    public float forceScrollSpeedX = 0.5f; //スクロールスピード

    public bool isForceScrollY; //強制スクロールフラグ
    public float forceScrollSpeedY = 0.5f; //スクロールスピード

    public GameObject subScreen; //サブスクリーンを取得



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float currentX = player.transform.position.x;
            float currentY = player.transform.position.y;

            //Xが強制スクロールならば、forceScrollSpeedXの分だけ自動で加算されている
            if (isForceScrollX) currentX = transform.position.x + (forceScrollSpeedX * Time.deltaTime);

            //Xが強制スクロールならば、forceScrollSpeedXの分だけ自動で加算されている
            if (isForceScrollY) currentY = transform.position.x + (forceScrollSpeedY * Time.deltaTime);

            if (currentX < leftLimit)
            {
                currentX = leftLimit;
            }

            else if (currentX > rightLimit)
            {
                currentX = rightLimit;
            }

            if (currentY < topLimit)
            {
                currentY = topLimit;
            }

            else if (currentY > bottomLimit)
            {
                currentY = bottomLimit;
            }

        
            transform.position = new Vector3(currentX, currentY,transform.position.z);

            if (subScreen != null)
            {
                //サブスクリーンはカメラより鈍い動きで連動させる
                subScreen.transform.position 
                    = new Vector3(currentX / 2.0f,subScreen.transform.position.y, subScreen.transform.position.z);
            }
        }

        

    }
}

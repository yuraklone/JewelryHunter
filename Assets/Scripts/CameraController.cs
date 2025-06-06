using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float leftLimit; //�J�����̍��̈ړ����E
    public float  rightLimit; //�J�����̉E�̈ړ����E
    public float topLimit; //�J�����̏�̈ړ����E
    public float bottomLimit; //�J�����̉��̈ړ����E

    public bool isForceScrollX; //�����X�N���[���t���O
    public float forceScrollSpeedX = 0.5f; //�X�N���[���X�s�[�h

    public bool isForceScrollY; //�����X�N���[���t���O
    public float forceScrollSpeedY = 0.5f; //�X�N���[���X�s�[�h

    public GameObject subScreen; //�T�u�X�N���[�����擾



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

            //X�������X�N���[���Ȃ�΁AforceScrollSpeedX�̕����������ŉ��Z����Ă���
            if (isForceScrollX) currentX = transform.position.x + (forceScrollSpeedX * Time.deltaTime);

            //X�������X�N���[���Ȃ�΁AforceScrollSpeedX�̕����������ŉ��Z����Ă���
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
                //�T�u�X�N���[���̓J�������݂������ŘA��������
                subScreen.transform.position 
                    = new Vector3(currentX / 2.0f,subScreen.transform.position.y, subScreen.transform.position.z);
            }
        }

        

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float gameTime = 60.0f; //�����
    public float currentTime; //���݂̎c�莞��
    float pastTime = 0; //�o�ߎ���

    public bool isTimeOver; //�J�E���g�_�E�����~�߂�t���O ��bool�^�͏����l�ȗ��ŏ����false

    // Start is called before the first frame update
    void Start()
    {
        currentTime = gameTime; //�c�莞�ԂɊ���Ԃ��Z�b�g
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeOver)
        {
            return;//�^�C���I�[�o�[�̃t���O���o������J�E���g�_�E�����Ȃ�
        }
        //�o�ߎ���
        pastTime += Time.deltaTime; //1�t���[��������ɂ����鎞��

        //�c�莞�Ԃ̌v�Z
        currentTime = gameTime - pastTime;

        //�c�莞�Ԃ�0�b�ȉ��ł����
        if(currentTime <= 0)
        {
            isTimeOver = true; //�J�E���g�_�E�����~�߂�t���O��ON
            currentTime = 0f; //�c�莞�Ԃ�0�ɂ҂����葵����
        }

        //�R���\�[���ɏ��o��
        //Debug.Log("�c�莞��" + currentTime);

    }
}

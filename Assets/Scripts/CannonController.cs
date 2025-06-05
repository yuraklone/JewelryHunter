using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float searchLength = 8.0f; //���G�͈�

    GameObject player; //�v���C���[���

    public float delayTime = 3.0f; //���˃C���^�[�o��
    float pastTime = 0; //�o�ߎ���

    public GameObject objPrefab; //�������ׂ�Prefab�f�[�^
    public Transform gateTransform; //�Q�[�g�̈ʒu���

    public float fireSpeed = 4.0f; //���ˑ��x

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //�o�ߎ��Ԃ̊ϑ�
        pastTime = Time.deltaTime;


        //Player�Ƃ̋����`�F�b�N
        if (CheckLength(player.transform.position))
        {
            if(pastTime > delayTime)
            {
                pastTime = 0; //�o�ߎ��Ԃ̃��Z�b�g

                //�C�e�̐���
                GameObject obj = Instantiate(objPrefab,gateTransform.position,Quaternion.identity);
                
                //�������������Rigidbody���w��ł���悤�ɂ��Ă���
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
                
                //�L���m���̌X�����擾
                float angleZ = transform.localEulerAngles.z;
                float x = Mathf.Cos(angleZ*Mathf.Deg2Rad);
                float y = Mathf.Sin(angleZ*Mathf.Deg2Rad);
                Vector2 v = new Vector2(x, y) * fireSpeed;

                //�C�e���g��Rigidbody�̗͂ŖC�e���΂�
                rbody.AddForce(v, ForceMode2D.Impulse);

            }
        }


    }


    bool CheckLength(Vector2 targetPos) //�_���^�̃v���C���[�ƖC��̋����m�F
    {
        float distance = Vector2.Distance(this.transform.position, targetPos);
        return searchLength >= distance; //���ʂ�^�U�ŕԂ�

    }


}

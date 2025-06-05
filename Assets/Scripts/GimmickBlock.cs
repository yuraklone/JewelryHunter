using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    public float length = 0.0f; //�����������m
    public bool isDelete = false; //������̍폜���邩�ǂ����̃t���O
    public GameObject deadObj; //���S�Ɋւ��邠���蔻��
    bool isFell = false; //���������̂����ł����邩�ǂ����̃t���O
    float fadeTime = 0.5f; //������܂ł̎���


    Rigidbody2D rbody; //Rigidbody2D�R���|�[�l���g���̎擾
    GameObject player; //Player���̎擾
    float distance; //�u���b�N�ƃv���C���[�̋����̎擾

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();�@//rbody�ɊY������Rigidbody2D�����擾
        rbody.bodyType = RigidbodyType2D.Static; //RigidbodyType2d.Static�������A�������~
        deadObj.SetActive(false); //deadObj���A�N�e�B�u�ɂ���

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //�v���C���[�����擾
        if (player != null) //�����v���C���[��null�łȂ��Ȃ�
        {
            //�v���C���[�ƃu���b�N�̋������擾����
            distance = Vector2.Distance(this.transform.position , player.transform.position);
            if (distance <= length )�@//���m�͈͓��ɓ�������
            {
                if (rbody.bodyType == RigidbodyType2D.Static)
                {
                    rbody.bodyType = RigidbodyType2D.Dynamic; //�d�͂𕜊������ė���
                    deadObj.SetActive(true); //���������玀�S���������

                }
                
                
            }


        }

        if(isFell == true)
        {
            fadeTime -= Time.deltaTime;
            Color col = GetComponent<SpriteRenderer>().color;
            col.a = fadeTime; //�����x��0�Ɍ�����fadeTime�ƃ����N
            GetComponent<SpriteRenderer>().color = col; 
            if(fadeTime <= 0.0f)
            {
                Destroy(this.gameObject); //�w�肵���I�u�W�F�N�g�����Ł@���v���C��
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
        //�ҏW��ʂ�length�̔��a�̉~��`��
        Gizmos.DrawWireSphere(transform.position, length);
    }


}

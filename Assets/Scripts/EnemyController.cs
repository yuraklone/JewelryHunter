using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool isToRight; //true = �E�����Afalse = ������
    int groundContactCount; //�n�ʂɂǂ̂��炢�ڐG�������@
    
    public LayerMask groundLayer;

    public bool isTimeTurn; //���ԂŔ��]�����邩�ǂ����̃t���O
    public float turnTime; //���]�̃C���^�[�o��
    float pastTime; //�o�ߎ���

    // Start is called before the first frame update
    void Start()
    {
        if(isToRight == true)
        {
            transform.localScale = new Vector2(-1, 1);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeTurn == true)
        {
            pastTime += Time.deltaTime;
            if (pastTime >= turnTime)
            {
                pastTime = 0;
                Turn();
            }
        }
    }

    private void FixedUpdate()
    {
        if(GameController.gameState == "gameClear")
        {
            return;
        }


        bool onGround = Physics2D.CircleCast
            (
            transform.position, //�Z���T�[�̔����ʒu
            0.5f, //�~�̔��a
            Vector2.down,//�ǂ̕��p�Ɍ����邩
            0, //�Z���T�[���΂�����
            groundLayer //�����Ώۂ̃��C���[
            );

        if(onGround== true)
        {
            Rigidbody2D rbody = GetComponent<Rigidbody2D>();
            if(isToRight == true)
            {
                rbody.velocity = new Vector2(speed, rbody.velocity.y);
            }
            else
            {
                rbody.velocity = new Vector2(-speed, rbody.velocity.y);
            }


        }
        
    }

    void Turn() //�E�������̐؂�ւ����\�b�h
    {
        isToRight = !isToRight;

        if (isToRight)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2 (1, 1);
        }

    }
    //Ground�^�O�ȊO�̂��̂ƂԂ������甽�]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Ground")
        {
            Turn();�@//�����ƂԂ������甽�]
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundContactCount++; //�V�����n�ʂƐڐG������1�J�E���g�v���X
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundContactCount--; //�����̒n�ʂ��痣�ꂽ��1�J�E���g�}�C�i�X

            if (groundContactCount <= 0)  //�}�C�i�X�������ʁA�V���ɐڐG����n�ʂ��Ȃ��Ƃ�
            {
                groundContactCount = 0; //�O�̂��߃J�E���g���O�ɖ߂�
                Turn(); //�R�ۂƎv����̂Ŕ��]
            }


        }
    }


}

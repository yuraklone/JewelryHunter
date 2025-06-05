using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 3.0f; //X�����ւ̈ړ�����
    public float moveY = 3.0f; //Y�����ւ̈ړ�����
    public float times = 3.0f; //���b�ňړ����邩
    public float wait = 1.0f; //�܂�Ԃ��̃C���^�[�o��

    float distance; //�J�n�n�_�ƈړ��\��n�_�̍�
    float secoindsDistance; //1�b������̈ړ��\�苗��
    float framsDistance; //1�t���[��������̈ړ�����
    float movePercentage = 0; //�ڕW�܂łǂ̂��炢�����Ă���̂�

    bool isCanMove = true; //������OK���ǂ����̃t���O
    Vector3 startPos; //�u���b�N�̏����ʒu
    Vector3 endPos; //�ړ���̗\��ʒu
    bool isReverse; //�������]�t���O


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; //�����ʒu����
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
            distance = Vector2.Distance(startPos, endPos); //�����ʒu�ƈړ���ʒu�̋�������
            secoindsDistance = distance / times ; //�ړ�����b�����Z�o�A���
            framsDistance = secoindsDistance*Time.deltaTime; //1�t���[��������̈ړ��������Z�o
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
                isCanMove = false; //�������~�߂�
                isReverse = !isReverse; //�ړ������t���O�𔽓]
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
            //Player�̐e�������Ɏw��
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Player�̐e���Ȃ�����null�ŉ��
            collision.transform.SetParent(null);
        }
    }

    //�ړ��͈͕\��
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
        //�ړ���
        Gizmos.DrawLine(fromPos, new Vector2(fromPos.x + moveX, fromPos.y + moveY));
        //�X�v���C�g�̃T�C�Y
        Vector2 size = GetComponent<SpriteRenderer>().size;
        //�����ʒu
        Gizmos.DrawWireCube(fromPos, new Vector2(size.x, size.y));
        //�ړ��ʒu
        Vector2 toPos = new Vector3(fromPos.x + moveX, fromPos.y + moveY);
        Gizmos.DrawWireCube(toPos, new Vector2(size.x, size.y));
    }�@



}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float axisH; //���E�̃L�[�̒l���i�[����ϐ�
    Rigidbody2D rbody; //Rigidbody2D���������߂̔}��
    public float speed = 3.0f; //�����X�s�[�h
    bool isJump; //�W�����v�����ǂ���
    bool onGround; //�n�ʔ���
    public LayerMask groundLayer; //�n�ʔ���̑Ώۂ̃��C���[�����������߂Ă���
    public float jump; //�W�����v��

    // Start is called before the first frame update
    void Start()
    {
        //Player�ɂ��Ă���Rigidbody2D�R���|�[�l���g��ϐ�rbody�ɏh��
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //���E�̃L�[�������ꂽ��A�ǂ���̒l���������̂���axisH�Ɋi�[
        //Horisontal�F���������̃L�[�������ꂽ�ꍇ�̈���
        //���Ȃ�-1,�E�Ȃ�1,����������Ă��Ȃ��Ȃ�0��Ԃ����\�b�h
        axisH = Input.GetAxisRaw("Horizontal");

        //����axisH�����Ȃ�E����
        if(axisH > 0)
        {
            transform.localScale = new Vector3(1,1,1);
            //this.gameObject.GetComponent<Transform>().localScale�̗��`
        }


        //����axisH�����Ȃ獶����
        else if(axisH < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }



    private void FixedUpdate()
    {
        //�n�ʂɂ��邩�ǂ������T�[�N���L���X�g���g���Ĕ���
        onGround = Physics2D.CircleCast
            (
            transform.position, //Player�̊�_ 
            0.2f, //�`�F�b�N�~�̔��a
            Vector2.down, //�w�肵���_����̃`�F�b�N����:down=Vector2(0,-1)
            0.0f, //�w�肵���_����̃`�F�b�N����
            groundLayer //�w�肵�����C���[
            );

        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
    
        //�W�����v���t���O����������AddForce���\�b�h�ɂ���ď�ɉ����o��
        if (isJump) //==true�͏ȗ��ł���
        {
            rbody.AddForce(new Vector2(0,jump), ForceMode2D.Impulse);
            isJump = false;
        }
    
    }

    public void Jump()
    {
        if(onGround)
        isJump = true;
    }
}

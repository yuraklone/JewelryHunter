using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float axisH; //���E�̃L�[�̒l���i�[����ϐ�
    Rigidbody2D rbody; //Rigidbody2D���������߂̔}��
    public float speed = 3.0f;

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
        axisH = Input.GetAxisRaw("Horisontal");

        //velocity��2���̕����f�[�^(Vector2)����
        rbody.velocity = new Vector2(axisH * speed,rbody.velocity.y);

    }

    private void FixedUpdate()
    {
        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
    }
}

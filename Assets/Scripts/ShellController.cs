using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{


    public float deleteTime = 3.0f; //���ł���܂ł̎��Ԑݒ�
    public bool isDelete; //�Ԃ�����������邩�ǂ����̃t���O �@



    // Start is called before the first frame update
    void Start()
    {

        //���ԍ��ŏ���
        Destroy(gameObject, deleteTime); //deleteTime��ɏ���
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


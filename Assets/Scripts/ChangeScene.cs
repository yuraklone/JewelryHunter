using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //SceneManager�N���X���g���̂ɕK�v
public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    public void Load()
    {
        //SceneManager�N���X�������Ă��郁�\�b�h���g���ăV�[���؂�ւ�
        SceneManager.LoadScene(sceneName);
    }
}

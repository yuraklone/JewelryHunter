using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static string gameState; //�Q�[���̏�Ԃ��Ǘ����� ���ÓI�ϐ�

    public GameObject stageTitle; //�X�e�[�W�^�C�g����UI�I�u�W�F�N�g
    public Sprite gameClearSprite; //�Q�[���N���A�̊G
    public Sprite gameOverSprite; //�Q�[���I�[�o�[�̊G

    public GameObject buttonPanel; //�{�^���p�l����UI�I�u�W�F�N�g


    // Start is called before the first frame update
    void Start()
    {
        //�X�^�[�g�Ɠ����ɃX�e�[�^�X��playing�ɂ���
        gameState = "playing";

        Invoke("InactiveImage", 1.0f); //�������Ɏw�肵�����\�b�h��������b��ɔ���
        
        buttonPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[����Ԃ��N���Aor�I�[�o�[�̎��A
        if (gameState == "gameclear" || gameState == "gameover")
        {
            //�X�e�[�W�^�C�g���𕜊�
            stageTitle.SetActive(true);
            stageTitle.GetComponent<Image>().sprite = gameClearSprite;


            //�{�^���̕���
            buttonPanel.SetActive(true);
        }

        if(gameState == "gameclear")
        {
            stageTitle.GetComponent<Image>().sprite = gameClearSprite;
        }
        
        if (gameState == "gameover")
        {
            stageTitle.GetComponent<Image>().sprite = gameOverSprite;
        }
    }

    void InactiveImage()�@//�X�e�[�W�^�C�g�����\���ɂ��郁�\�b�h
    {
        stageTitle.SetActive(false); //�I�u�W�F�N�g���\��
    }
}

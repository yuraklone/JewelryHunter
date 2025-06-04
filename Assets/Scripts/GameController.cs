using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static string gameState; //�Q�[���̏�Ԃ��Ǘ����� ���ÓI�ϐ�

    public GameObject stageTitle; //�X�e�[�W�^�C�g����UI�I�u�W�F�N�g
    public Sprite gameClearSprite; //�Q�[���N���A�̊G
    public Sprite gameOverSprite; //�Q�[���I�[�o�[�̊G

    public GameObject buttonPanel; //�{�^���p�l����UI�I�u�W�F�N�g
    public GameObject restartButton; //���X�^�[�g�{�^��
    public GameObject nextButton; //�l�N�X�g�{�^��

    TimeController timeCnt; //TimeController�X�N���v�g���擾����

    public TextMeshProUGUI timeText; //TimeText�I�u�W�F�N�g�������Ă���TextMeshPro�R���|�[�l���g���擾

    // Start is called before the first frame update
    void Start()
    {
        //�X�^�[�g�Ɠ����ɃX�e�[�^�X��playing�ɂ���
        gameState = "playing";

        Invoke("InactiveImage", 1.0f); //�������Ɏw�肵�����\�b�h��������b��ɔ���
        
        buttonPanel.SetActive(false);

        //TiomeContoroller�R���|�[�l���g�̏����擾
        timeCnt = GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == "playing")
        {
            //�J�E���g�_�E���̕ϐ�currentTime��UI�ɘA��
            
            //timeCnt��currentTime(float�^)��int�^�ɕϊ����A
            //ToString��String�ɕϊ����Ă���timeText(TextMeshPro)��text���ɑ��
            timeText.text = ((int)timeCnt.currentTime).ToString();

            //����currentTime��0�ɂȂ�����Q�[���I�[�o�[
            if(timeCnt.currentTime <= 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player"); //�v���C���[��T��
                //PlayerController�R���|�[�l���g��GameOver���\�b�h�𔭓�
                player.GetComponent<PlayerController>().GameOver(); 
            }

        }

        //�Q�[����Ԃ��N���Aor�I�[�o�[�̎��A
        else if (gameState == "gameclear" || gameState == "gameover")
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
            //restartButton�I�u�W�F�N�g�������Ă���Button�R���|�[�l���g�̒l�ł���interactable��false���@�\���~
            restartButton.GetComponent<Button>().interactable = false;


        }
        
        if (gameState == "gameover")
        {
            stageTitle.GetComponent<Image>().sprite = gameOverSprite;
            //nextButton�I�u�W�F�N�g�������Ă���Button�R���|�[�l���g�̒l�ł���interactable��false���@�\���~
            nextButton.GetComponent<Button>().interactable = false;
        }
    }

    void InactiveImage()�@//�X�e�[�W�^�C�g�����\���ɂ��郁�\�b�h
    {
        stageTitle.SetActive(false); //�I�u�W�F�N�g���\��
    }
}

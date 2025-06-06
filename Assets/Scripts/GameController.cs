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

    public TextMeshProUGUI scoreText; //UI�̃e�L�X�g
    public static int totalScore; //�Q�[���S�̂̍��v�X�R�A
    public static int stageScore; //�X�e�[�W���Ɏ�ɓ��ꂽ�X�R�A


    // Start is called before the first frame update
    void Start()
    {
        stageScore = 0; //�X�e�[�W�X�R�A�����Z�b�g

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
            UpdateScore();

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

            if (gameState == "gameclear")
            {
                stageTitle.GetComponent<Image>().sprite = gameClearSprite;
                //restartButton�I�u�W�F�N�g�������Ă���Button�R���|�[�l���g�̒l�ł���interactable��false���@�\���~
                restartButton.GetComponent<Button>().interactable = false;

                totalScore += stageScore; //�g�[�^���X�R�A�̍X�V
                stageScore = 0; //���ɔ�����0�Ƀ��Z�b�g
                totalScore += (int)(timeCnt.currentTime * 10); //�^�C���{�[�i�X���g�[�^���ɉ�����


            }

            else if (gameState == "gameover")
            {
                stageTitle.GetComponent<Image>().sprite = gameOverSprite;
                //nextButton�I�u�W�F�N�g�������Ă���Button�R���|�[�l���g�̒l�ł���interactable��false���@�\���~
                nextButton.GetComponent<Button>().interactable = false;



            }

            gameState = "gameend"; //�d�����ď������s��Ȃ��悤�ɏ�Ԃ�J��

        }


    }

    void InactiveImage()�@//�X�e�[�W�^�C�g�����\���ɂ��郁�\�b�h
    {
        stageTitle.SetActive(false); //�I�u�W�F�N�g���\��
    }

    void UpdateScore()
    {
        //UI�ł���X�R�A�e�L�X�g�Ƀg�[�^���ƃX�e�[�W�̍��v�l����
        scoreText.text = (stageScore + totalScore).ToString();

    }
}

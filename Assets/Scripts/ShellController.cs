using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{


    public float deleteTime = 3.0f; //Á–Å‚·‚é‚Ü‚Å‚ÌŠÔİ’è
    public bool isDelete; //‚Ô‚Â‚©‚Á‚½‚çÁ‚¦‚é‚©‚Ç‚¤‚©‚Ìƒtƒ‰ƒO @



    // Start is called before the first frame update
    void Start()
    {

        //ŠÔ·‚ÅÁ–Å
        Destroy(gameObject, deleteTime); //deleteTimeŒã‚ÉÁ–Å
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


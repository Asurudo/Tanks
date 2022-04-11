using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _dropMono : MonoBehaviour
{
    //��ȡ����
    private GameObject GM;

    protected void dropMonoStart()
    {
        GM = GameObject.Find("GameManager");
    }
    
    //�����ײ������ң��������Ʒ
    protected void pickUp(Collider collider, string callBackFunc)
    {
        if (collider.tag == "player")
        {
            GM.SendMessage(callBackFunc, SendMessageOptions.DontRequireReceiver);
            GameObject.Destroy(this.gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

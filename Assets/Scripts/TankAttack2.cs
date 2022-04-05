using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack2 : MonoBehaviour
{
    //����λ��
    private Transform FirePosition;
    //�ӵ�Ԥ����
    public GameObject shellPrefab;
    //�����
    public KeyCode fireKey;
    public float shellSpeed = 10;

    public AudioClip shotAudio;

    //��ʱ��
    private float trrigerlastTime;   
    private float trrigercurTime;

    private float attacklastTime;
    private float attackcurTime;
    public float attackgapTime;
    void Start()
    {
        FirePosition = this.transform.Find("FirePosition");
        attackgapTime = 1.0f;
    }

    
    void Update()
    {
        attackcurTime = Time.time;
        if (attackcurTime - attacklastTime < attackgapTime)
            return;

        attacklastTime = Time.time;
        AudioSource.PlayClipAtPoint(shotAudio, this.transform.position);
        GameObject go = GameObject.Instantiate(shellPrefab, FirePosition.position, FirePosition.rotation) as GameObject;
        go.GetComponent<Shell>().fromwhere = 2;
        go.GetComponent<Rigidbody>().velocity = go.transform.forward * shellSpeed;
        
    }

    //�������
    public void OnTriggerEnter(Collider collider)
    {
        trrigercurTime = Time.time;
        if(trrigercurTime - trrigerlastTime < 1)
             return ;
        trrigerlastTime = Time.time; 

        float[] message = new float[2];  
        message[0] = 10;  
        message[1] = 20;

        float[] message2 = new float[2];
        message2[0] = 2;
        message2[1] = 5;

        if (collider.tag == "tank" && this.tag != "tank")
        { 
            collider.SendMessage("TakeDamage", message, SendMessageOptions.DontRequireReceiver);
            collider.SendMessage("Backnow");
        }
        else if (collider.tag == "player")
        {
            collider.SendMessage("TakeDamage", message2, SendMessageOptions.DontRequireReceiver);
            //this.transform.position -= transform.forward * 1.0f;
        }
        else if (collider.tag == "building")
            this.GetComponent<TankHealth>().TakeDamage(message2);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
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
    private float lastTime;   
    private float curTime;


    void Start()
    {
        FirePosition = transform.Find("FirePosition");
    }

    
    void Update()
    {
        //������°���
        if(Input.GetKeyDown(fireKey))
        {
            AudioSource.PlayClipAtPoint(shotAudio, transform.position);
            GameObject go = GameObject.Instantiate(shellPrefab, FirePosition.position, FirePosition.rotation) as GameObject;
            go.GetComponent<Rigidbody>().velocity = go.transform.forward * shellSpeed;
        }
    }

    //�������
    public void OnTriggerEnter(Collider collider)
    {
        curTime = Time.time;
        if(curTime - lastTime < 1)
             return ;
        lastTime = Time.time; 

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
        if (collider.tag == "player")
        {
            collider.SendMessage("TakeDamage", message2, SendMessageOptions.DontRequireReceiver);
            //this.transform.position -= transform.forward * 1.0f;
        }
        else if (collider.tag == "building")
            this.GetComponent<TankHealth>().TakeDamage(message2);
    }

    public void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "tank" && this.tag != "tank")
            collider.SendMessage("Backnow");
    }
}

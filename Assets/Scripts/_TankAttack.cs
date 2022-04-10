using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _TankAttack : MonoBehaviour
{
    /*
         �������
                   */
    //����λ��
    protected Transform tankFirePosition;
    //�ӵ�Ԥ����
    public GameObject shellPrefab;
    public float tankshellSpeed;
    //�����
    public KeyCode tankFireKey;
    //������Ч
    public AudioClip tankShotAudio;

    /*
         ��ײ��ʱ��
                   */
    //��ʱ��
    private float tankCollisionLastTime;
    private float tankCollisionCurTime;

    protected void tankAttackStart()
    {
        tankFirePosition = this.transform.Find("tankFirePosition");
    }

    protected void tankCollision(Collider collider)
    {
        tankCollisionCurTime = Time.time;
        if (tankCollisionCurTime - tankCollisionLastTime < 1)
            return;
        tankCollisionLastTime = Time.time;

        float[] message = new float[2];
        message[0] = 10;
        message[1] = 20;

        float[] message2 = new float[2];
        message2[0] = 2;
        message2[1] = 5;

        if (collider.tag == "tank" && this.tag != "tank")
            collider.SendMessage("TakeDamage", message, SendMessageOptions.DontRequireReceiver);
        else if (collider.tag == "player")
            collider.SendMessage("TakeDamage", message2, SendMessageOptions.DontRequireReceiver);
        else if (collider.tag == "building")
            this.GetComponent<TankHealth>().TakeDamage(message2);
    }

    public abstract void tankFire();

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

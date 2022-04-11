using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _TankAttack : MonoBehaviour
{
    //����λ��
    protected Transform tankFirePosition;
    //�ӵ�Ԥ����
    public GameObject shellPrefab;
    public float tankshellSpeed;
    //�����
    public KeyCode tankFireKey;
    //������Ч
    public AudioClip tankShotAudio;

    //��ʱ��
    private float tankCollisionLastTime;
    private float tankCollisionCurTime;

    //װ�䷵��һ����Χ����
    float[] damageRange(float minDamage, float maxDamage)
    {
        float[] damageRange = new float[2];
        damageRange[0] = minDamage; 
        damageRange[1] = maxDamage;
        return damageRange;
    }

    protected void tankAttackStart()
    {
        tankFirePosition = this.transform.Find("tankFirePosition");
    }

    protected void tankCollision(Collider collider)
    {
        //������ײ�������ֹ��ײ����Ƶ��
        tankCollisionCurTime = Time.time;
        if (tankCollisionCurTime - tankCollisionLastTime < 1.0f)
            return;
        tankCollisionLastTime = Time.time;

        //����ǵ�����ײ��һ��߽�����������ܵ�[10,20]���˺�
        if (collider.tag == "tank" && this.tag != "tank")
            collider.SendMessage("TakeDamage", damageRange(10, 20), SendMessageOptions.DontRequireReceiver);
        //������Լ�ֻ�ܵ�[2,5]���˺�
        else if (collider.tag == "player")
            collider.SendMessage("TakeDamage", damageRange(2, 5), SendMessageOptions.DontRequireReceiver);
        //������������������ܵ�[2,5]���˺�
        else if (collider.tag == "building")
            this.GetComponent<TankHealth>().TakeDamage(damageRange(2, 5));
    }

    public abstract void tankFire();

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

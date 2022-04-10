using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _TankMovement : MonoBehaviour
{
    /*
          �ٶȱ���
                    */
    //�н��ٶ�  
    public float tankSpeed;
    //��ת�ٶ�
    public float tankAngularSpeeed;

    /*
          ��Ƶ
                    */
    //����ʱ��Ƶ
    public AudioClip tankIdleAudio;
    //��ʻʱ��Ƶ
    public AudioClip tankDrivingAudio;
    //�������ŵ���Ƶ
    protected AudioSource tankRunningAudio;

    /*
          ����
                    */
    //�������
    protected Rigidbody tankRigidbody;

    public abstract void tankRunning();
    protected void tankMovementStart()
    {
        tankRigidbody = this.GetComponent<Rigidbody>();
        tankRunningAudio = this.GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    
}

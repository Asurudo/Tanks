using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttackTeki2 : _TankAttackTeki
{
    //������˹������
    private float tankAttackLastTime;
    public float tankAttackGapTime;

    public override void tankFire()
    {
        //��ֹ���ڹ���Ƶ��
        if (Time.time - tankAttackLastTime < tankAttackGapTime)
            return;

        tankAttackLastTime = Time.time;

        AudioSource.PlayClipAtPoint(tankShotAudio, this.transform.position);
        GameObject shell = GameObject.Instantiate(shellPrefab, tankFirePosition.position, tankFirePosition.rotation) as GameObject;
        shell.GetComponent<Shell>().setFromWhere("teki");
        shell.GetComponent<Rigidbody>().velocity = shell.transform.forward * tankshellSpeed;
    }
    void Start()
    {
        tankAttackStart();
        tankAttackLastTime = Time.time;
    }

    
    void Update()
    {
        
    }

    //�������
    public void OnTriggerEnter(Collider collider)
    {
        tankCollision(collider);
    }
}

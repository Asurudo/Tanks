using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttackTeki1: _TankAttackTeki
{
    public override void tankFire()
    {
        ;
    }
    void Start()
    {
        tankAttackStart();
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

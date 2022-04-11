using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform playerTransform;

    private Vector3 cameraOffset;
    private Camera mainCamera;

    void Start()
    {
        //��ȡһ��ʼ�������ƫ����
        cameraOffset = transform.position - playerTransform.position;
        mainCamera = this.GetComponent<Camera>();
    }

    
    void Update()
    {
        //��ұ�����
        if(playerTransform == null)
            return ;
        transform.position = playerTransform.position + cameraOffset;
        mainCamera.orthographicSize = 15.0F;
    }
}

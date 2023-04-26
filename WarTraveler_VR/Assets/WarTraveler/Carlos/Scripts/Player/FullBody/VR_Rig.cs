using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VR_Map
{
    [SerializeField] private Transform vrTarget;
    [SerializeField] private Transform rigTarget;
    [SerializeField] private Vector3 trackingPositionOffset;
    [SerializeField] private Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VR_Rig : MonoBehaviour
{
    [SerializeField] private VR_Map head;
    [SerializeField] private VR_Map leftHand;
    [SerializeField] private VR_Map rightHand;

    [SerializeField] private Transform headConstraint;
    [SerializeField] private Vector3 headBodyOffset;
    [SerializeField] private float turnSmoothness;

    private void Start()
    {
        //headBodyOffset = transform.position - headConstraint.position;
    }

    private void FixedUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward,Vector3.ProjectOnPlane(headConstraint.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);
        
        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}

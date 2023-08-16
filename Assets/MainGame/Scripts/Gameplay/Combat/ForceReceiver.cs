using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float drag = 0.5f;
    private                  float               verticalVelocity;

    private Vector3 impact;
    private Vector3 dampingVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;
    private void Update()
    {
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

        if (impact.sqrMagnitude < .2f * .2f && agent != null)
        {
            impact        = Vector3.zero;
            agent.enabled = true;
        }
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
        if (agent != null)
        {
            agent.enabled = false;
        }
    }
}

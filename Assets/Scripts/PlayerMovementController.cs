using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    Vector3 m_Movement;
    Animator m_Animator;
    Rigidbody m_RigidBody;
    Quaternion m_Rotation = Quaternion.identity;
    AudioSource m_FootStepsAudio;

    public float turnSpeed = 20f;
    public float movementSpeed = -3f;


    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody>();
        m_FootStepsAudio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal * movementSpeed, 0f, vertical * movementSpeed);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        m_Animator.SetBool("IsWalking", isWalking);

        HandleFootStepsAudio(isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed, 0);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void HandleFootStepsAudio(bool isWalking)
    {
        if (isWalking)
        {
            if (!m_FootStepsAudio.isPlaying)
            {
                m_FootStepsAudio.Play();
            }
        }
        else
        {
            m_FootStepsAudio.Stop();
        }
    }

    void OnAnimatorMove()
    {
        m_RigidBody.MovePosition(m_RigidBody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_RigidBody.MoveRotation(m_Rotation);
    }
}

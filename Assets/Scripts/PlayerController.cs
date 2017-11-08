using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour {

    [SerializeField] float m_AnimSpeedMultiplier = 1f;
    [SerializeField] float m_GroundCheckDistance = 0.1f;
    [Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;
    public float m_movementSpeed = 2;
    [SerializeField] float m_dodgeForce = 500;

    private Rigidbody m_Rigidbody;
    private Animator m_Animator;
    private Vector3 m_GroundNormal;
    private float m_TurnAmount;
    private float m_ForwardAmount;
    private bool m_IsGrounded;
    private const float k_Half = 0.5f;
    private float m_OrigGroundCheckDistance;

    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        m_OrigGroundCheckDistance = m_GroundCheckDistance;
    }

    public void Move(Vector3 move, ref int dodge)
    {

        // convert the world relative moveInput vector into a local-relative
        // turn amount and forward amount required to head in the desired
        // direction.
        if (move.magnitude > 1f) move.Normalize();
        //move = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_ForwardAmount = move.z;

        // control and velocity handling is different when grounded and airborne:
        if (m_IsGrounded)
        {
        }
        else
        {
            HandleAirborneMovement();
        }

        // send input and other state parameters to the animator
        //UpdateAnimator(move);

        // move the player
        MovePlayer(move);
        RotatePlayer(move);
        if (dodge == 1)
            Dodge(ref dodge);
    }

    void UpdateAnimator(Vector3 move)
    {
        // update the animator parameters
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("OnGround", m_IsGrounded);
        if (!m_IsGrounded)
        {
            m_Animator.SetFloat("Falling", m_Rigidbody.velocity.y);
        }

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which affects the movement speed because of the root motion.
        if (m_IsGrounded && move.magnitude > 0)
        {
            m_Animator.speed = m_AnimSpeedMultiplier;
        }
        else
        {
            // don't use that while airborne
            m_Animator.speed = 1;
        }
    }

    void Dodge(ref int dodge)
    {
        if (m_Rigidbody != null)
        {
            dodge = 2;
            Vector3 directionToDodge = transform.forward;
            m_Rigidbody.AddForce(directionToDodge * m_dodgeForce, ForceMode.Impulse);
        }
    }
    void MovePlayer(Vector3 move)
    {
        if (m_Rigidbody != null)
        {
            m_Rigidbody.MovePosition(transform.position + move * m_movementSpeed * Time.deltaTime);
        }
    }

    void RotatePlayer(Vector3 rotate)
    {
        if (m_Rigidbody != null)
        {
            transform.LookAt(transform.position + rotate);
        }
    }

    void HandleAirborneMovement()
    {
        if (m_Rigidbody != null)
        {

            // apply extra gravity from multiplier:
            Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
            m_Rigidbody.AddForce(extraGravityForce);

            m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
        }
    }
}

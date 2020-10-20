using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        Direct
    }

    [SerializeField] private float m_moveSpeed = 3;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 500;

    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;

    private float m_currentV = 0;
    private float m_currentH = 0;
    private readonly float m_interpolation = 100;
    private readonly float m_runScale = 1.5f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;
    private bool m_jumpInput = false;

    private bool m_isGrounded;

    private List<Collider> m_collisions = new List<Collider>();
    float walkAudioSpeed = 0.3f;
    float runAudioSpeed;
    float walkAudioTimer = 0;    
    float runAudioTimer = 0;
    bool isWalking = false;
    bool isRunning = false;

AudioManager audioManager;
    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
        runAudioSpeed = walkAudioSpeed / m_runScale;
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void Reset(){
        m_currentDirection = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                if(!m_isGrounded) {
                    GridController grid = GameObject.FindGameObjectWithTag("World").GetComponent<GridController>();
                    if(grid) grid.DropHex(collision.gameObject);
                    Debug.Log("grounded with " + collision.gameObject.name);
                }
                m_isGrounded = true;
                
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            if(!m_isGrounded) Debug.Log("grounded1");
            m_isGrounded = true;
            
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { if(m_isGrounded) Debug.Log("not grounded"); m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) {  
            if(m_isGrounded) {
                
                Debug.Log("not grounded");
             } m_isGrounded = false; 
        }
    }

    private void Update()
    {
        if(m_isGrounded){
            PlayFootsteps();
        }
        else {
            walkAudioTimer = 0;
            runAudioTimer = 0;
        }

        if (!m_jumpInput && Input.GetKey(KeyCode.Space))
        {
            m_jumpInput = true;
        }        
    }

    private void FixedUpdate()
    {
        m_animator.SetBool("Grounded", m_isGrounded);

        switch (m_controlMode)
        {
            case ControlMode.Direct:
                DirectUpdate();
                break;

            case ControlMode.Tank:
                TankUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }

        m_wasGrounded = m_isGrounded;
        m_jumpInput = false;
    }

    private void TankUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0)
        {
            if (walk) { v *= m_backwardsWalkScale; }
            else { v *= m_backwardRunScale; }
        }
        else if (walk)
        {
            v *= m_runScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        m_animator.SetFloat("MoveSpeed", m_currentV);

        JumpingAndLanding();
    }

    private void DirectUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_runScale;
            h *= m_runScale;
        }
        float interpol = m_interpolation;
        if (!m_isGrounded)
        {
            interpol = 1;            
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * interpol);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * interpol);    

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * interpol);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
            
        }

        JumpingAndLanding();
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && m_jumpInput)
        {
            m_jumpTimeStamp = Time.time;            
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            AudioManager.Instance.PlayJumpEnd();
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            AudioManager.Instance.PlayJumpStart();
            m_animator.SetTrigger("Jump");
        }
    }

    void PlayFootsteps() 
    {
        if ( Input.GetButton( "Horizontal" ) || Input.GetButton( "Vertical" ) )
        {
            if ( Input.GetKey( "left shift" ) || Input.GetKey( "right shift" ) )
            {
            // Running
            isWalking = false;
            isRunning = true;
            }
            else
            {
            // Walking
            isWalking = true;
            isRunning = false;
            }
        }
        else
        {
            // Stopped
            isWalking = false;
            isRunning = false;
        }
        
        // Play Audio
        if ( isWalking )
        {
               
            //if ( !audio.isPlaying )
            if ( walkAudioTimer > walkAudioSpeed)
            {
            //audioSource.Stop();
            //audioSource.Play();
            audioManager.PlayFootStep();

            walkAudioTimer = 0;
            }
        }
        else if ( isRunning )
        {
               
            //if ( !audio.isPlaying )
            if ( runAudioTimer > runAudioSpeed )
            {
            
            audioManager.PlayFootStep();
            runAudioTimer = 0;
            }
        }
        else
        {
            //audioSource.Stop();
        }
        
        // increment timers
        walkAudioTimer += Time.deltaTime;
        runAudioTimer += Time.deltaTime;    
    }

}

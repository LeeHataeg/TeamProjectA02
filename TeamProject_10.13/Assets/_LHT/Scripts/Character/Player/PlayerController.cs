using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public Rigidbody2D rigidbody { get; private set; }
    public Animator animator { get; private set; }
    public PlayerInput playerInput { get; private set; }
    private void Awake()
    {
        

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
       
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    Transform player;
    [SerializeField]
    CharacterController controller;    
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask groundmask, obstacleMask, exitMask;
    [SerializeField]
    bool doubleJumpEnabled = false;
    float speed = 12f, gravity = -20f, jumpHeight = 3f, groundDistance = .4f, obstacleDistance = .6f;
    Vector3 velocity;
    bool isGrounded, hitObstacle, doubleJumpAvailable;
    GameHandler gameHandler;

    void Start() {
        Time.timeScale = 1;
        print("start " + Time.timeScale);
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();

        
    }
    void Update() {
        hitObstacle = Physics.CheckSphere(groundCheck.position, obstacleDistance, obstacleMask);
        if (hitObstacle) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        bool hitExit = Physics.CheckSphere(groundCheck.position, obstacleDistance, exitMask);
        if (hitExit) {
            gameHandler.LevelComplete();
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);
        
            
        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
        }
        

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (doubleJumpEnabled)
            DoubleJump();
    }

    void DoubleJump() {
        if (isGrounded && !doubleJumpAvailable)
            doubleJumpAvailable = true;

        if(Input.GetButtonDown("Jump") && !isGrounded && doubleJumpAvailable) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
            doubleJumpAvailable = false;
    }
    }

}
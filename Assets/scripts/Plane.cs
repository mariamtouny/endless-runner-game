using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Plane : MonoBehaviour
{
    public AudioManager audioManager;
    private Rigidbody rb;
    private Vector3 direction;
    private float forwardSpeed =6;
    private int desiredLane = 1;
    private float laneDistance = 3;
    private Vector3 targetPosition;
    public LayerMask groundLayer;
    public float jumpDistance = 1.5f;
    public float score;
    private float fuel = 50;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI fuelText;
    public TextMeshProUGUI speedText;
    private bool boost = false;
    private bool burning = false;
    private bool pause = false;


    public GameObject gameOverPanel;
    public GameObject pausePanel;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();

    }
    void Start()
    {
        audioManager.PlayMusic(audioManager.play);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;
        UpdateLanePosition();
        UpdateScreen();

    }

    void Update()
    {
        score += Time.deltaTime;
        fuel = burning? fuel -= Time.deltaTime * 10 : fuel -= Time.deltaTime;
        UpdateScreen();
        if (transform.position.y <= -3 && transform.position.y >= -12.5)
        {
            audioManager.PlaySFX(audioManager.fall, 1f);
            //GameOver();
        }
        else if (transform.position.y < -12.5) GameOver();

        
        if (fuel<=0) GameOver();
        Vector3 forwardMovement = transform.forward * -forwardSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + forwardMovement);

        direction.z = -forwardSpeed;

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (IsGrounded()) Jump(); else audioManager.PlaySFX(audioManager.error, 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
            if (desiredLane > 0) desiredLane--;
            else audioManager.PlaySFX(audioManager.error, 1.5f);
            UpdateLanePosition();

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (desiredLane <2) desiredLane++;
            else audioManager.PlaySFX(audioManager.error, 1.5f);
            UpdateLanePosition();

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                audioManager.UnPauseSFX();
                audioManager.UnPauseMusic();
                Time.timeScale = 1;
                pausePanel.SetActive(false);
                pause = false;
            }
            else
            {
                audioManager.PauseSFX();
                audioManager.PauseMusic();
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                pause = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.H) && forwardSpeed/2 > 0) forwardSpeed = forwardSpeed/2;
        if (Input.GetKeyDown(KeyCode.F)) fuel = 50;

    }

    public void Resume()
    {
        audioManager.UnPauseSFX();
        audioManager.UnPauseMusic();
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pause = false;
    }

    private void FixedUpdate()
    {
        //Vector3 forwardMovement = transform.forward * -forwardSpeed * Time.fixedDeltaTime;
        //rb.MovePosition(rb.position + forwardMovement);
    }

    private void UpdateLanePosition()
    {
        Vector3 newPosition = transform.position;

        newPosition.x = (desiredLane - 1) * laneDistance; 
        transform.position = newPosition;
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpDistance, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.1f, groundLayer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            audioManager.PlaySFX(audioManager.crash, 1f);
            GameOver();
        }
        if (collision.gameObject.CompareTag("Burning"))
        {
            audioManager.PlaySFX(audioManager.burning, 1f);
            burning = true;
        }
        
        if (collision.gameObject.CompareTag("Supplies"))
        {
            audioManager.PlaySFX(audioManager.supplies, 1f);
            fuel = 50;
        }
        
        if (collision.gameObject.CompareTag("Sticky"))
        {
            audioManager.PlaySFX(audioManager.sticky, 1.5f);
            forwardSpeed = boost ? forwardSpeed / 2 : forwardSpeed;
            boost = false;
        }
        if (collision.gameObject.CompareTag("Boost"))
        {
            audioManager.PlaySFX(audioManager.boost, 1.5f);
            forwardSpeed = boost ? forwardSpeed : forwardSpeed * 2;
            boost = true;
        }



    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Burning")) burning = false;

    }

    void UpdateScreen() { 
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString() ;
        scoreText2.text = "Score: " + Mathf.FloorToInt(score).ToString();
        fuelText.text = "Fuel: " + Mathf.FloorToInt(fuel).ToString();
        speedText.text = boost ? "Speed: High" : "Speed: Normal";
    }
    private void GameOver()
    {
        //audioManager.StopSFX();
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        audioManager.PlayMusic(audioManager.gameover);
    }
}

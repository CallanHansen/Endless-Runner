using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Values")]
    public float BaseSpeed = 50.0f;
    public float CurrentSpeed = 50.0f;
    public float BaseSpeedIncreaseMultiplier = 5.0f;
    public float SpeedIncreaseMultiplier = 5.0f;
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float timeToSwitchLane = 1f;

    private Rigidbody rb = null;
    private Vector3 rbVelocity = Vector3.zero;

    [Header("Lanes")]
    [SerializeField] private Transform[] playerLaneLocations;
    [SerializeField] private int currentLaneIndex = 1;

    [Header("Jumping/Ground Checking")]
    [SerializeField] private Transform groundChecker = null;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    private bool grounded = true;

    public bool CanMove = false;

    [Header("Sliding")]
    [SerializeField] private float slideTime = 1.2f;
    [SerializeField] private TextMeshProUGUI slidingDebugText = null;
    private bool sliding = false;

    [Header("Collider")]
    [SerializeField] private CapsuleCollider playerCollision = null;
    [SerializeField] private float playerNormalCollisionHeight = 0.8f;
    [SerializeField] private Vector3 playerNormalCollisionCenter = Vector3.zero;

    [SerializeField] private float playerSlideCollisionHeight = 0.2f;
    [SerializeField] private Vector3 playerSlideCollisionCenter = Vector3.zero;
    
    public static PlayerController Instance;

    // Awake is called before start
    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Assign rb variable to player rigidbody component
        playerCollision = GetComponent<CapsuleCollider>();

        if(Instance == null) // If there is no instance set
        {
            Instance = this; // Set this to be the instance
        } else
        {
            Destroy(gameObject); // Remove the additional player from the scene. There should only be one player at once!
        }       
    }

    void Start()
    {
        playerCollision.height = playerNormalCollisionHeight;
        playerCollision.center = playerNormalCollisionCenter;

        SpeedIncreaseMultiplier = BaseSpeedIncreaseMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        // Creates an invisible sphere around the groundChecker position at a set radius. If the ground collides with the invisible sphere, then true, the player is grounded, otherwise false.
        grounded = Physics.CheckSphere(groundChecker.position, groundCheckRadius, groundLayer);

        if (CanMove)
        {
            CurrentSpeed += Time.deltaTime * SpeedIncreaseMultiplier; // Slowly increase the currentSpeed variable over time

            #region Inputs

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (grounded)
                {
                    Jump();
                }
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentLaneIndex > 0) // Ensure the player doesn't go outside array bounds
                {
                    currentLaneIndex--;
                }
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (currentLaneIndex < playerLaneLocations.Length - 1) // Ensuring the player doesnt go outside array bounds
                {
                    currentLaneIndex++;
                }
            }

            if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(grounded)
                {
                    Slide();
                }
            }

            if(!sliding)
            {
                playerCollision.height = playerNormalCollisionHeight;
                playerCollision.center = playerNormalCollisionCenter;
            } else
            {
                playerCollision.height = playerSlideCollisionHeight;
                playerCollision.center = playerSlideCollisionCenter;
            }

            #endregion
        }
        else
        {
            CurrentSpeed = 0;
            SpeedIncreaseMultiplier = 0;
        }

        transform.position = Vector3.Lerp(transform.position, playerLaneLocations[currentLaneIndex].position, timeToSwitchLane);
        slidingDebugText.text = "Is player sliding: " + sliding;
    }

    void FixedUpdate()
    {
        rbVelocity = new Vector3(0, rb.velocity.y, CurrentSpeed * Time.deltaTime); // Move player forward at currentSpeed
        rb.velocity = rbVelocity; // Set the velocity
    }

    void Jump()
    {
        sliding = false;
        rb.velocity = new Vector3(0, jumpForce, CurrentSpeed * Time.deltaTime);
    }

    void Slide()
    {
        StartCoroutine(SlideCoroutine());
    }

    IEnumerator SlideCoroutine()
    {
        sliding = true;
  
        yield return new WaitForSeconds(slideTime);       

        sliding = false;

        yield break;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            GameManager.Instance.GameOver();
        }

        if (other.gameObject.CompareTag("NextTrack"))
        {
            TrackGeneration.Instance.ChooseNextTrack();
        }
    }
}

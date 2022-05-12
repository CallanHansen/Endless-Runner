using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Values")]
    public float CurrentSpeed = 0.2f;
    public float SpeedIncreaseMultiplier = 0.5f;
    [SerializeField] private float jumpForce = 10;

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

    public static PlayerController Instance;

    // Awake is called before start
    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Assign rb variable to player rigidbody component

        if(Instance == null) // If there is no instance set
        {
            Instance = this; // Set this to be the instance
        } else
        {
            Destroy(gameObject); // Remove the additional player from the scene. There should only be one player at once!
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Creates an invisible sphere around the groundChecker position at a set radius. If the ground collides with the invisible sphere, then true, the player is grounded, otherwise false.
        grounded = Physics.CheckSphere(groundChecker.position, groundCheckRadius, groundLayer); 

        CurrentSpeed += Time.deltaTime * SpeedIncreaseMultiplier; // Slowly increase the currentSpeed variable over time

        #region Inputs

        if(Input.GetKeyDown(KeyCode.W))
        {
            if (grounded)
            {
                Jump();
            }    
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            if (currentLaneIndex > 0) // Ensure the player doesn't go outside array bounds
            {
                currentLaneIndex--;
                UpdatePlayerPosition();
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentLaneIndex < playerLaneLocations.Length - 1) // Ensuring the player doesnt go outside array bounds
            {
                currentLaneIndex++;
                UpdatePlayerPosition();
            }        
        }

        #endregion
    }

    void FixedUpdate()
    {
        rbVelocity = new Vector3(0, rb.velocity.y, CurrentSpeed * Time.deltaTime); // Move player forward at currentSpeed
        rb.velocity = rbVelocity; // Set the velocity
    }

    void Jump()
    {
        rb.velocity = new Vector3(0, jumpForce, CurrentSpeed * Time.deltaTime);
    }

    void UpdatePlayerPosition()
    {
        transform.position = playerLaneLocations[currentLaneIndex].position;
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

using UnityEngine;

public class TrackGeneration : MonoBehaviour
{ 
    [SerializeField] private GameObject[] trackToSpawn;
    [SerializeField] private Transform nextTrackPosition = null;

    public static TrackGeneration Instance;
    private const int TRACKS_TO_SPAWN_ON_START = 3;

    private int randomTrackChoice = 0;
    private int previousTrack = 0;

    void Start()
    {
        #region  Singleton
        if (Instance == null)
        {
            Instance = this;
        }  else
        {
            Destroy(gameObject);
        }
        #endregion

        for(int i = 0; i < TRACKS_TO_SPAWN_ON_START; i++) 
        {
            ChooseNextTrack(false); // Spawn a few empty tracks in front of the player to give the player time to begin reacting 
        }     
    }

    public void ChooseNextTrack(bool _randomTrack = true) // Choose next track to spawn. Random or empty?
    {
        Debug.Log("Spawn the next track!");

        previousTrack = randomTrackChoice;
        randomTrackChoice = Random.Range(0, trackToSpawn.Length);

        if (_randomTrack) 
        {
            SpawnRandomTrack();
        }
        else 
        {
            GameObject temp = Instantiate<GameObject>(trackToSpawn[0].gameObject, nextTrackPosition.transform.position, Quaternion.identity); // Create an empty track tile 
            nextTrackPosition = temp.transform.GetChild(0).transform;
        }       
    }

    void SpawnRandomTrack() // Spawn a random track
    {
        randomTrackChoice = Random.Range(0, trackToSpawn.Length); // Select a new random track

        if (!IsTrackSameAsPrevious()) // Ensuring that the track isnt the same as the previous track that was spawned
        {
            GameObject temp = Instantiate<GameObject>(trackToSpawn[randomTrackChoice].gameObject, nextTrackPosition.transform.position, Quaternion.identity);
            nextTrackPosition = temp.transform.GetChild(0).transform;
        } else
        {
            SpawnRandomTrack(); // Using recursion to spawn a different track if the track is the same as the previous track
        }    
    }

    bool IsTrackSameAsPrevious() // Checking if the track was the same as the previous 
    {
        if (randomTrackChoice != previousTrack)
        {
            return false;           
        } else
        {
            Debug.Log("Track was the same. Spawn a different track!");
            return true;
        }
    }
}


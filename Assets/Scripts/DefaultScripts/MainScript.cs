using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public bool cheats = false;
    public bool endSpawn = false;
    public List<GameObject> kindOfWeapons = new List<GameObject>();
    public List<GameObject> kindOfEnemies = new List<GameObject>();
    public List<AudioClip> kindOfMusicClips = new List<AudioClip>();

    [Header("Now exists")]
    public List<GameObject> rooms = new List<GameObject>();
    public List<IEnemy> enemies = new List<IEnemy>();
    public new Camera camera;

    public Joystick attackJoystick;

    private AudioSource soundPlay;
    private int roomsCount;
    private RoomVariants variants;
    private GameObject roomWithMaxPosition;

    void Start()
    {
        StaticClass.mainScript = GetComponent<MainScript>();
        if (StaticClass.player == null)
        {
            StaticClass.player = GameObject.FindGameObjectWithTag("Player");
        }
        StaticClass.playerCharacteristic = StaticClass.player.GetComponent<PlayerCharacteristic>();
        StaticClass.weaponsInventory = StaticClass.player.GetComponent<WeaponsInventory>();

        soundPlay = GetComponent<AudioSource>();
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        roomsCount = rooms.Count;
        StartCoroutine(CheckEndSpawn());
        StartCoroutine(StartMusic());
    }

    void Update()
    {
        Cheats();
    }

    IEnumerator StartMusic()
    {
        soundPlay.Play();
        yield return new WaitForSeconds(6f);
        MusicPlay();
    }

    public void MusicPlay()
    {
        soundPlay.Stop();
        soundPlay.clip = kindOfMusicClips[Random.Range(0, kindOfMusicClips.Count)];
        soundPlay.Play();
    }

    public void MusicStop()
    {
        soundPlay.Stop();
    }

    IEnumerator CheckEndSpawn()
    {
        yield return new WaitForSeconds(2f);
        if (rooms.Count != roomsCount)
        {
            roomsCount = rooms.Count;
            StartCoroutine(CheckEndSpawn());
        }
        else if(rooms.Count == roomsCount)
        {
            endSpawn = true;
            SpawnBossRoom();
        }
    }

    private void SpawnBossRoom()
    {
        roomWithMaxPosition = rooms[0];
        float maxDistance = 0;
        for(int i = 1; i < rooms.Count; i++)
        {
            float distance = DistanceBetween2dPoints(rooms[i].transform.position, rooms[0].transform.position);
            if (distance > maxDistance)
            {
                roomWithMaxPosition = rooms[i];
                maxDistance = distance;
            }
        }
        Vector3 pos = roomWithMaxPosition.transform.position;
        int k = roomWithMaxPosition.transform.Find("EnemySpawner").gameObject.GetComponent<EnemySpawner>().doors[0].GetComponent<Door>().number;
        rooms.Remove(roomWithMaxPosition);
        Destroy(roomWithMaxPosition);
        var obj = Instantiate(variants.bossRooms[k-1], pos, Quaternion.identity);
        obj.transform.Find("EnemySpawner").gameObject.GetComponent<EnemySpawner>().OpenDoors();
    }

    public void SetToAllEnemiesAlivePlayerOrDead(bool value)
    {
        foreach(IEnemy enemy in enemies)
        {
            enemy.SetPlayerIsAlive(value);
        }
    }

    private float DistanceBetween2dPoints(Vector2 vec1, Vector2 vec2)
    {
        float distance;

        distance = Mathf.Pow(Mathf.Pow(vec2.x-vec1.x,2) + Mathf.Pow(vec2.y - vec1.y, 2), 0.5f);

        return distance;
    }

    private void Cheats()
    {
        if (cheats)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 0;
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                GameObject[] obj = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < obj.Length; i++)
                {
                    Destroy(obj[i]);
                }

                GameObject gm = GameObject.FindGameObjectWithTag("Boss");
                if (gm != null)
                {
                    gm.GetComponent<Boss>().BossDie();
                }
            }
        }
    }
}

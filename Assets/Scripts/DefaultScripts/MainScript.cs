using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public bool cheats = false;
    public bool endSpawn = false;
    public List<GameObject> kindOfWeapons = new List<GameObject>();
    public List<AudioClip> kindOfMusicClips = new List<AudioClip>();
    public List<AudioClip> kindOfBossMusic = new List<AudioClip>();

    public List<GameObject> kindOfEnemies1Wave = new List<GameObject>();
    public List<GameObject> kindOfEnemies2Wave = new List<GameObject>();
    public List<GameObject> kindOfEnemies3Wave = new List<GameObject>();

    [Header("Now exists")]
    public List<GameObject> rooms = new List<GameObject>();
    public List<Enemy> enemies = new List<Enemy>();
    public new Camera camera;

    public Joystick attackJoystick;

    private AudioSource soundPlay;
    private int roomsCount;
    private RoomVariants variants;

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
        try {variants =  GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>(); } catch { }
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
        MusicPlay(kindOfMusicClips[Random.Range(0, kindOfMusicClips.Count)]);
    }

    public void MusicPlay(AudioClip audioClip)
    {
        soundPlay.Stop();
        soundPlay.clip = audioClip;
        soundPlay.Play();
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
            if (variants != null)
            {
                SpawnBossRoomAndShop();
            }
        }
    }

    private void SpawnBossRoomAndShop()
    {
        List<GameObject> rms1Doors = new List<GameObject>();
        GameObject roomForBoss = rooms[0];
        GameObject roomForShop = null;
        float distance = 0;
        float maxDistance = 0;
        for(int i = 1; i < rooms.Count; i++)
        {
            if (rooms[i].GetComponentInChildren<EnemySpawner>().doors.Count == 1) 
            { 
                rms1Doors.Add(rooms[i]);
                distance = Vector3.Distance(rooms[i].transform.position, rooms[0].transform.position);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    roomForBoss = rooms[i];
                }
            }
        }

        rms1Doors.Remove(roomForBoss);

        Vector3 pos = roomForBoss.transform.position;
        int k = roomForBoss.transform.Find("EnemySpawner").gameObject.GetComponent<EnemySpawner>().doors[0].GetComponent<Door>().number;
        rooms.Remove(roomForBoss);
        Destroy(roomForBoss);
        var obj = Instantiate(variants.bossRooms[k-1], pos, Quaternion.Euler(-90,0,0));

        if (rms1Doors.Count > 1) 
        { 
            roomForShop = rms1Doors[Random.Range(0, rms1Doors.Count)];
            rooms.Remove(roomForShop);
            Destroy(roomForShop);
            k = roomForShop.transform.Find("EnemySpawner").gameObject.GetComponent<EnemySpawner>().doors[0].GetComponent<Door>().number;
            pos = roomForShop.transform.position;
            var shop = Instantiate(variants.shopRooms[k - 1], pos, Quaternion.Euler(-90, 0, 0));
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
                    gm.GetComponent<Boss>().EnemyHasBeenKilled();
                }
            }
        }
    }
}

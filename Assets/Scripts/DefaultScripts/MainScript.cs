using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MainScript : MonoBehaviour
{
    public event Action OnGameLoad;

    [SerializeField] private bool _cheats = false;
    [SerializeField] private bool _endSpawn = false;

    [SerializeField] private List<GameObject> _kindOfWeapons = new List<GameObject>();
    [SerializeField] private List<AudioClip> _kindOfMusicClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _kindOfBossMusic = new List<AudioClip>();
    [SerializeField] private List<GameObject> _kindOfEnemies1Wave = new List<GameObject>();
    [SerializeField] private List<GameObject> _kindOfEnemies2Wave = new List<GameObject>();
    [SerializeField] private List<GameObject> _kindOfEnemies3Wave = new List<GameObject>();
    [SerializeField] private List<GameObject> _kindOfBoss = new List<GameObject>();

    [Header("Now exists")]
    [SerializeField] private List<GameObject> _rooms = new List<GameObject>();
    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();

    private Camera _mainCamera;
    private Joystick _joystickMove;
    private Joystick _joystickAttack;
    private AudioSource _soundPlay;
    private int _roomsCount;
    private RoomVariants _variants;

    public Joystick JoystickAttack => _joystickAttack;
    public Camera MainCamera => _mainCamera;

    private void Awake()
    {
        StaticClass.mainScript = this;
        _mainCamera = Camera.main;

        if(StaticClass.typeOfDevice == StaticClass.TypeOfDevice.Phone)
        {
            _joystickMove = GameObject.FindGameObjectWithTag("JoystickMove").GetComponent<FixedJoystick>();
            _joystickAttack = GameObject.FindGameObjectWithTag("JoystickAttack").GetComponent<FixedJoystick>();
        }

        if (StaticClass.player == null)
            StaticClass.player = GameObject.FindGameObjectWithTag("Player");

        StaticClass.playerCharacteristic = StaticClass.player.GetComponent<PlayerCharacteristic>();
        StaticClass.weaponsInventory = StaticClass.player.GetComponent<WeaponsInventory>();
    }

    private void Start()
    {
        _soundPlay = GetComponent<AudioSource>();
        try { _variants =  FindObjectOfType<RoomVariants>(); } catch { }
        _roomsCount = _rooms.Count;
        StartCoroutine(CheckEndSpawn());
    }

    private void Update()
    {
        Cheats();
    }

    public void MusicPlay(AudioClip audioClip)
    {
        _soundPlay.Stop();
        _soundPlay.clip = audioClip;
        _soundPlay.Play();
    }

    private IEnumerator CheckEndSpawn()
    {
        yield return new WaitForSeconds(1.1f);
        if (_rooms.Count != _roomsCount)
        {
            _roomsCount = _rooms.Count;
            StartCoroutine(CheckEndSpawn());
        }
        else if(_rooms.Count == _roomsCount)
        {
            _endSpawn = true;
            if (_variants != null)
            {
                SpawnBossRoomAndShop();
            }
        }
    }

    private void SpawnBossRoomAndShop()
    {
        List<GameObject> rms1Doors = new List<GameObject>();
        GameObject roomForBoss = _rooms[0];
        GameObject roomForShop = null;
        float distance = 0;
        float maxDistance = 0;
        for(int i = 1; i < _rooms.Count; i++)
        {
            if (_rooms[i].GetComponentInChildren<EnemySpawner>().DoorsCount == 1) 
            { 
                rms1Doors.Add(_rooms[i]);
                distance = Vector3.Distance(_rooms[i].transform.position, _rooms[0].transform.position);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    roomForBoss = _rooms[i];
                }
            }
        }

        rms1Doors.Remove(roomForBoss);

        Vector3 pos = roomForBoss.transform.position;
        int k = roomForBoss.transform.GetComponentInChildren<EnemySpawner>().FirstDoor;
        _rooms.Remove(roomForBoss);
        Destroy(roomForBoss);
        var obj = Instantiate(_variants.GetBossRoomByIndex(k-1), pos, Quaternion.Euler(-90,0,0));

        if (rms1Doors.Count > 1) 
        { 
            roomForShop = rms1Doors[Random.Range(0, rms1Doors.Count)];
            _rooms.Remove(roomForShop);
            Destroy(roomForShop);
            k = roomForShop.transform.GetComponentInChildren<EnemySpawner>().FirstDoor;
            pos = roomForShop.transform.position;
            var shop = Instantiate(_variants.GetShopRoomByIndex(k - 1), pos, Quaternion.Euler(-90, 0, 0));
        }

        OnGameLoad?.Invoke();
        StartCoroutine(StartMusic());
    }

    private void Cheats()
    {
        if (_cheats)
        {
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
                    Destroy(obj[i].transform.root.gameObject);
                }

                GameObject gm = GameObject.FindGameObjectWithTag("Boss");
                if (gm != null)
                {
                    gm.GetComponent<Boss>().EnemyHasBeenKilled();
                }
            }
        }
    }

    private IEnumerator StartMusic()
    {
        _soundPlay.Play();
        yield return new WaitForSeconds(6f);
        MusicPlay(_kindOfMusicClips[Random.Range(0, _kindOfMusicClips.Count)]);
    }

    public GameObject GetRandomWeapon()
    {
        if (_kindOfWeapons.Count < 1)
            throw new NullReferenceException("Not have weapons");
        return _kindOfWeapons[Random.Range(0, _kindOfWeapons.Count)];
    }

    public AudioClip GetRandomMusic()
    {
        if (_kindOfMusicClips.Count < 1)
            throw new  NullReferenceException("Not have music clips");
        return _kindOfMusicClips[Random.Range(0, _kindOfMusicClips.Count)];
    }

    public AudioClip GetRandomBossMusic()
    {
        if (_kindOfBossMusic.Count < 1)
            throw new NullReferenceException("Not have boss music clips");
        return _kindOfBossMusic[Random.Range(0, _kindOfBossMusic.Count)];
    }

    public GameObject GetRandomEnemyWave1()
    {
        if (_kindOfEnemies1Wave.Count < 1)
            throw new NullReferenceException("Not have enemy wave1");
        return _kindOfEnemies1Wave[Random.Range(0, _kindOfEnemies1Wave.Count)];
    }

    public GameObject GetRandomEnemyWave2()
    {
        if (_kindOfEnemies2Wave.Count < 1)
            throw new NullReferenceException("Not have enemy wave1");
        return _kindOfEnemies2Wave[Random.Range(0, _kindOfEnemies2Wave.Count)];
    }

    public GameObject GetRandomEnemyWave3()
    {
        if (_kindOfEnemies3Wave.Count < 1)
            throw new NullReferenceException("Not have enemy wave1");
        return _kindOfEnemies3Wave[Random.Range(0, _kindOfEnemies3Wave.Count)];
    }

    public GameObject GetFirstBoss()
    {
        if (_kindOfBoss.Count < 1)
            throw new NullReferenceException("Not have boss");
        return _kindOfBoss[0];
    }

    public void AddRoomToList(GameObject room)
    {
        _rooms.Add(room);
    }

    public void RemoveRoomFromList(GameObject room)
    {
        _rooms.Remove(room);
    }

    public void AddEnemyToList(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
}

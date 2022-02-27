using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> doors = new List<GameObject>();

    [SerializeField] private int _secondsBetweenWaves = 2;
    [SerializeField] private int _numberOfSpawnPoints = 5;
    [SerializeField] private int _numberOfWaves = 2;
    [SerializeField] private TypeOfWave _type1 = TypeOfWave.Single;
    [SerializeField] private TypeOfWave _type2;
    [SerializeField] private TypeOfWave _type3;
    [SerializeField] private GameObject chest;
    [SerializeField] private bool boss = false;
    [SerializeField] private GameObject enemySpawnMarker;
    private Vector2[] _points;
    private bool _playerTrigger = false;
    private bool _isOpen = false;
    [SerializeField] private List<GameObject> _enemy1 = new List<GameObject>();
    private List<GameObject> _enemy2 = new List<GameObject>();
    private List<GameObject> _enemy3 = new List<GameObject>();
    private List<GameObject> _wave1 = new List<GameObject>();
    private List<GameObject> _wave2 = new List<GameObject>();
    private List<GameObject> _wave3 = new List<GameObject>();
    private List<GameObject> enemyMarkers = new List<GameObject>();
    private bool _firstWaveEnd = false;
    private bool _secondWaveEnd = false;
    private bool _thirdWaveEnd = false;

    private MainScript mainScript;
    public enum TypeOfWave
    {
        None,
        Single,
        Double,
        Triple
    }
    // Start is called before the first frame update
    void Start()
    {
        mainScript = FindObjectOfType<MainScript>();
        _points = new Vector2[_numberOfSpawnPoints];
        StartCoroutine(DelayBeforeTriggerPlayer());
        RandomEnemys();
    }

    private void Update()
    {
        if (_playerTrigger)
        {
            Menu();
        }
    }

    private void RandomEnemys()
    {
        if (!boss)
        {
            for (int i = 0; i < _numberOfSpawnPoints * 3; i++) 
            {
                if (_numberOfWaves == 1)
                {
                    _enemy1.Add(mainScript.kindOfEnemies1Wave[Random.Range(0, mainScript.kindOfEnemies1Wave.Count)]);
                }
                else if(_numberOfWaves == 2)
                {
                    _enemy1.Add(mainScript.kindOfEnemies2Wave[Random.Range(0, mainScript.kindOfEnemies2Wave.Count)]);
                    _enemy2.Add(mainScript.kindOfEnemies2Wave[Random.Range(0, mainScript.kindOfEnemies2Wave.Count)]);
                }
                else if (_numberOfWaves == 3)
                {
                    _enemy1.Add(mainScript.kindOfEnemies3Wave[Random.Range(0, mainScript.kindOfEnemies3Wave.Count)]);
                    _enemy2.Add(mainScript.kindOfEnemies3Wave[Random.Range(0, mainScript.kindOfEnemies3Wave.Count)]);
                    _enemy3.Add(mainScript.kindOfEnemies3Wave[Random.Range(0, mainScript.kindOfEnemies3Wave.Count)]);
                }
            }
        }
    }

    private void Menu()
    {
        if (_firstWaveEnd)
        {
            DeleteNull(_wave1);
            if (_wave1.Count == 0 && _numberOfWaves !=1)
            {
                StartCoroutine(SpawnSecondWave());
            }
            else if (_wave1.Count == 0 && _numberOfWaves == 1)
            {
                OpenDoors();
                _playerTrigger = true;
                Destroy(gameObject);  
            }
        }
        else if (_secondWaveEnd)
        {
            DeleteNull(_wave2);
            if (_wave2.Count == 0 && _numberOfWaves != 2)
            {
                StartCoroutine(SpawnThirdWave());
            }
            else if (_wave2.Count == 0 && _numberOfWaves == 2)
            {
                OpenDoors();
                _playerTrigger = true;
                Destroy(gameObject);
            }
        }
        else if (_thirdWaveEnd)
        {
            DeleteNull(_wave3);
            if (_wave3.Count == 0)
            {
                OpenDoors();
                _playerTrigger = true;
                Instantiate(chest, transform.root.gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public void AddDoorsToList(GameObject gm)
    {
        doors.Add(gm);
    }

    private void CloseDoors()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].SetActive(true);
        }
    }

    public void OpenDoors()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].gameObject.SetActive(false);
        }
    }

    IEnumerator SpawnFirstWave()
    {
        RecreatePoints();
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < _points.GetLength(0); i++)
        {
            if (_enemy1.Count != 0)
            {
                _wave1.Add(Instantiate(_enemy1[0], _points[i], Quaternion.identity));
                _enemy1.RemoveAt(0);
                Destroy(enemyMarkers[i]);
            }
            if (boss && _enemy1.Count != 0)
            {
                _wave1.Add(Instantiate(_enemy1[0], _points[i], Quaternion.identity));
                _enemy1.RemoveAt(0);
                Destroy(enemyMarkers[i]);
            }
        }
        _firstWaveEnd = true;
    }

    IEnumerator SpawnSecondWave()
    {
        RecreatePoints();
        _firstWaveEnd = false;
        yield return new WaitForSeconds(_secondsBetweenWaves);
        for (int i = 0; i < _points.GetLength(0); i++)
        {
            if (_enemy2.Count != 0)
            {
                _wave2.Add(Instantiate(_enemy2[0], _points[i], Quaternion.identity));
                _enemy2.RemoveAt(0);
                Destroy(enemyMarkers[i]);
            }
        }
        if (_type2 == TypeOfWave.Double)
        {
            RecreatePoints();
            yield return new WaitForSeconds(1);
            for (int i = 0; i < _points.GetLength(0); i++)
            {
                if (_enemy2.Count != 0)
                {
                    _wave2.Add(Instantiate(_enemy2[0], _points[i], Quaternion.identity));
                    _enemy2.RemoveAt(0);
                    Destroy(enemyMarkers[i]);
                }
            }
        }
        if (_type2 == TypeOfWave.Triple)
        {
            RecreatePoints();
            yield return new WaitForSeconds(1);
            for (int i = 0; i < _points.GetLength(0); i++)
            {
                if (_enemy2.Count != 0)
                {
                    _wave2.Add(Instantiate(_enemy2[0], _points[i], Quaternion.identity));
                    _enemy2.RemoveAt(0);
                    Destroy(enemyMarkers[i]);
                }
            }
        }
        _secondWaveEnd = true;
    }

    IEnumerator SpawnThirdWave()
    {
        RecreatePoints();
        _secondWaveEnd = false;
        yield return new WaitForSeconds(_secondsBetweenWaves);
        for (int i = 0; i < _points.GetLength(0); i++)
        {
            if (_enemy3.Count != 0)
            {
                _wave3.Add(Instantiate(_enemy3[0], _points[i], Quaternion.identity));
                _enemy3.RemoveAt(0);
                Destroy(enemyMarkers[i]);
            }
        }
        if (_type2 == TypeOfWave.Double)
        {
            RecreatePoints();
            yield return new WaitForSeconds(1);
            for (int i = 0; i < _points.GetLength(0); i++)
            {
                if (_enemy3.Count != 0)
                {
                    _wave3.Add(Instantiate(_enemy3[0], _points[i], Quaternion.identity));
                    _enemy3.RemoveAt(0);
                    Destroy(enemyMarkers[i]);
                }
            }
        }
        if (_type2 == TypeOfWave.Triple)
        {
            RecreatePoints();
            yield return new WaitForSeconds(1);
            for (int i = 0; i < _points.GetLength(0); i++)
            {
                if (_enemy3.Count != 0)
                {
                    _wave3.Add(Instantiate(_enemy3[0], _points[i], Quaternion.identity));
                    _enemy3.RemoveAt(0);
                    Destroy(enemyMarkers[i]);
                }
            }
        }
        _thirdWaveEnd = true;
    }

    private void RecreatePoints()
    {
        if (enemyMarkers.Count != 0) enemyMarkers.Clear();
        if (_points.Length == 1)
        {
            _points[0] = new Vector2(transform.root.localPosition.x -328, transform.root.localPosition.y);
            enemyMarkers.Add(Instantiate(enemySpawnMarker, _points[0], Quaternion.identity));
            enemyMarkers[0].transform.localScale *= 2;
        }
        else
        {
            for (int i = 0; i < _points.GetLength(0); i++)
            {
                _points[i] = new Vector2(transform.root.localPosition.x + Random.Range(-1100, 550), transform.root.localPosition.y + Random.Range(361, -550));
                enemyMarkers.Add(Instantiate(enemySpawnMarker, _points[i], Quaternion.identity));
            }
        }
    }

    private void DeleteNull(List<GameObject> list)
    {
        if (list.Count != 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null)
                {
                    list.RemoveAt(i);
                }
            }
        }
    }

    private IEnumerator DelayBeforeTriggerPlayer()
    {
        yield return new WaitForSeconds(1f);
        _isOpen = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_isOpen)
            {
                Destroy(GetComponent<BoxCollider2D>());
                CloseDoors();
                if (boss)
                    StaticClass.mainScript.MusicPlay(StaticClass.mainScript.kindOfBossMusic[Random.Range(0, StaticClass.mainScript.kindOfBossMusic.Count)]);
                StartCoroutine(SpawnFirstWave());
                _playerTrigger = true;
            }
        }
    }
}

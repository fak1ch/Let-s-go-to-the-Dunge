using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _doors = new List<GameObject>();
    [SerializeField] private int _secondsBetweenWaves = 2;
    [SerializeField] private int _numberOfSpawnPoints = 5;
    [SerializeField] private int _numberOfWaves = 2;
    [SerializeField] private TypeOfWave _type1 = TypeOfWave.Single;
    [SerializeField] private TypeOfWave _type2 = TypeOfWave.Single;
    [SerializeField] private TypeOfWave _type3 = TypeOfWave.Single;
    [SerializeField] private GameObject _chest;
    [SerializeField] private bool _boss = false;
    [SerializeField] private GameObject _enemySpawnMarker;

    private List<GameObject> _enemy1 = new List<GameObject>();
    private List<GameObject> _enemy2 = new List<GameObject>();
    private List<GameObject> _enemy3 = new List<GameObject>();
    private List<GameObject> _wave1 = new List<GameObject>();
    private List<GameObject> _wave2 = new List<GameObject>();
    private List<GameObject> _wave3 = new List<GameObject>();
    private List<GameObject> enemyMarkers = new List<GameObject>();

    private Vector2[] _points;

    private bool _playerTrigger = false;
    private bool _isOpen = false;
    private bool _firstWaveEnd = false;
    private bool _secondWaveEnd = false;
    private bool _thirdWaveEnd = false;

    private MainScript _mainScript;

    public int FirstDoor => _doors[0].GetComponent<Door>().GetNumber;
    public int DoorsCount => _doors.Count;
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
        _mainScript = FindObjectOfType<MainScript>();
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
        if (!_boss)
        {
            for (int i = 0; i < _numberOfSpawnPoints * 3; i++) 
            {
                if (_numberOfWaves == 1)
                {
                    _enemy1.Add(_mainScript.GetRandomEnemyWave1());
                }
                else if(_numberOfWaves == 2)
                {
                    _enemy1.Add(_mainScript.GetRandomEnemyWave2());
                    _enemy2.Add(_mainScript.GetRandomEnemyWave2());
                }
                else if (_numberOfWaves == 3)
                {
                    _enemy1.Add(_mainScript.GetRandomEnemyWave3());
                    _enemy2.Add(_mainScript.GetRandomEnemyWave3());
                    _enemy3.Add(_mainScript.GetRandomEnemyWave3());
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
                Instantiate(_chest, transform.root.gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public void AddDoorsToList(GameObject gm)
    {
        _doors.Add(gm);
    }

    private void CloseDoors()
    {
        for (int i = 0; i < _doors.Count; i++)
        {
            _doors[i].SetActive(true);
        }
    }

    public void OpenDoors()
    {
        for (int i = 0; i < _doors.Count; i++)
        {
            _doors[i].gameObject.SetActive(false);
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
            }
            else
            if (_boss)
            {
                _wave1.Add(Instantiate(_mainScript.GetFirstBoss(), _points[i], Quaternion.identity));
            }
            Destroy(enemyMarkers[i]);
        }
        _firstWaveEnd = true;
    }

    IEnumerator SpawnSecondWave()
    {
        _firstWaveEnd = false;
        RecreatePoints();
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
        _secondWaveEnd = false;
        RecreatePoints();
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
            _points[0] = new Vector2(transform.root.localPosition.x -328, transform.root.localPosition.y - 200);
            enemyMarkers.Add(Instantiate(_enemySpawnMarker, _points[0], Quaternion.identity));
            enemyMarkers[0].transform.localScale *= 2;
        }
        else
        {
            for (int i = 0; i < _points.GetLength(0); i++)
            {
                _points[i] = new Vector2(transform.root.localPosition.x + Random.Range(-1100, 550), transform.root.localPosition.y + Random.Range(361, -550));
                enemyMarkers.Add(Instantiate(_enemySpawnMarker, _points[i], Quaternion.identity));
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
                _isOpen = false;
                Destroy(GetComponent<BoxCollider2D>());
                CloseDoors();
                if (_boss)
                    _mainScript.MusicPlay(_mainScript.GetRandomBossMusic());
                StartCoroutine(SpawnFirstWave());
                _playerTrigger = true;
            }
        }
    }
}

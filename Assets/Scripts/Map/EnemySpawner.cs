using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int secondsBetweenWaves = 2;
    public int numberOfSpawnPoints = 5;
    public int numberOfWaves = 2;
    public TypeOfWave type1 = TypeOfWave.Single;
    public TypeOfWave type2;
    public TypeOfWave type3;
    private Vector2[] points;
    private bool playerTrigger = false;
    private bool _isOpen = false;

    public List<GameObject> enemy1 = new List<GameObject>();
    private List<GameObject> enemy2 = new List<GameObject>();
    private List<GameObject> enemy3 = new List<GameObject>();

    private  List<GameObject> wave1 = new List<GameObject>();
    private List<GameObject> wave2 = new List<GameObject>();
    private List<GameObject> wave3 = new List<GameObject>();

    public List<GameObject> doors = new List<GameObject>();
    private List<GameObject> enemyMarkers = new List<GameObject>();

    public GameObject chest;
    public bool boss = false;
    public GameObject enemySpawnMarker;

    private bool FirstWaveEnd = false;
    private bool SecondWaveEnd = false;
    private bool ThirdWaveEnd = false;

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
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        points = new Vector2[numberOfSpawnPoints];
        StartCoroutine(DelayBeforeTriggerPlayer());
        RandomEnemys();
    }

    private void Update()
    {
        if (playerTrigger)
        {
            Menu();
        }
    }

    private void RandomEnemys()
    {
        if (!boss)
        {
            for (int i = 0; i < 15; i++) 
            {
                if (numberOfWaves == 1)
                {
                    enemy1.Add(mainScript.kindOfEnemies1Wave[Random.Range(0, mainScript.kindOfEnemies1Wave.Count)]);
                }
                else if(numberOfWaves == 2)
                {
                    enemy2.Add(mainScript.kindOfEnemies2Wave[Random.Range(0, mainScript.kindOfEnemies2Wave.Count)]);
                }
                else if (numberOfWaves == 3)
                {
                    enemy3.Add(mainScript.kindOfEnemies3Wave[Random.Range(0, mainScript.kindOfEnemies3Wave.Count)]);
                }
            }
        }
    }

    private void Menu()
    {
        if (FirstWaveEnd)
        {
            DeleteNull(wave1);
            if (wave1.Count == 0 && numberOfWaves !=1)
            {
                StartCoroutine(SpawnSecondWave());
            }
            else if (wave1.Count == 0 && numberOfWaves == 1)
            {
                OpenDoors();
                playerTrigger = true;
                Destroy(gameObject);  
            }
        }
        else if (SecondWaveEnd)
        {
            DeleteNull(wave2);
            if (wave2.Count == 0 && numberOfWaves != 2)
            {
                StartCoroutine(SpawnThirdWave());
            }
            else if (wave2.Count == 0 && numberOfWaves == 2)
            {
                OpenDoors();
                playerTrigger = true;
                Destroy(gameObject);
            }
        }
        else if (ThirdWaveEnd)
        {
            DeleteNull(wave3);
            if (wave3.Count == 0)
            {
                OpenDoors();
                playerTrigger = true;
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
        for (int i = 0; i < points.GetLength(0); i++)
        {
            if (enemy1.Count != 0)
            {
                wave1.Add(Instantiate(enemy1[0], points[i], Quaternion.identity));
                enemy1.RemoveAt(0);
                Destroy(enemyMarkers[i]);
            }
            if (boss && enemy1.Count != 0)
            {
                wave1.Add(Instantiate(enemy1[0], points[i], Quaternion.identity));
                enemy1.RemoveAt(0);
                Destroy(enemyMarkers[i]);
            }
        }
        FirstWaveEnd = true;
    }

    IEnumerator SpawnSecondWave()
    {
        RecreatePoints();
        FirstWaveEnd = false;
        yield return new WaitForSeconds(secondsBetweenWaves);
        for (int i = 0; i < points.GetLength(0); i++)
        {
            if (enemy2.Count != 0)
            {
                wave2.Add(Instantiate(enemy2[0], points[i], Quaternion.identity));
                enemy2.RemoveAt(0);
                Destroy(enemyMarkers[i]);
            }
        }
        if (type2 == TypeOfWave.Double)
        {
            RecreatePoints();
            yield return new WaitForSeconds(1);
            for (int i = 0; i < points.GetLength(0); i++)
            {
                if (enemy2.Count != 0)
                {
                    wave2.Add(Instantiate(enemy2[0], points[i], Quaternion.identity));
                    enemy2.RemoveAt(0);
                    Destroy(enemyMarkers[i]);
                }
            }
        }
        if (type2 == TypeOfWave.Triple)
        {
            RecreatePoints();
            yield return new WaitForSeconds(1);
            for (int i = 0; i < points.GetLength(0); i++)
            {
                if (enemy2.Count != 0)
                {
                    wave2.Add(Instantiate(enemy2[0], points[i], Quaternion.identity));
                    enemy2.RemoveAt(0);
                    Destroy(enemyMarkers[i]);
                }
            }
        }
        SecondWaveEnd = true;
    }

    IEnumerator SpawnThirdWave()
    {
        RecreatePoints();
        SecondWaveEnd = false;
        yield return new WaitForSeconds(secondsBetweenWaves);
        for (int i = 0; i < points.GetLength(0); i++)
        {
            if (enemy3.Count != 0)
            {
                wave3.Add(Instantiate(enemy3[0], points[i], Quaternion.identity));
                enemy3.RemoveAt(0);
                Destroy(enemyMarkers[i]);
            }
        }
        if (type2 == TypeOfWave.Double)
        {
            RecreatePoints();
            yield return new WaitForSeconds(1);
            for (int i = 0; i < points.GetLength(0); i++)
            {
                if (enemy3.Count != 0)
                {
                    wave3.Add(Instantiate(enemy3[0], points[i], Quaternion.identity));
                    enemy3.RemoveAt(0);
                    Destroy(enemyMarkers[i]);
                }
            }
        }
        if (type2 == TypeOfWave.Triple)
        {
            RecreatePoints();
            yield return new WaitForSeconds(1);
            for (int i = 0; i < points.GetLength(0); i++)
            {
                if (enemy3.Count != 0)
                {
                    wave3.Add(Instantiate(enemy3[0], points[i], Quaternion.identity));
                    enemy3.RemoveAt(0);
                    Destroy(enemyMarkers[i]);
                }
            }
        }
        ThirdWaveEnd = true;
    }

    private void RecreatePoints()
    {
        if (enemyMarkers.Count != 0) enemyMarkers.Clear();
        if (points.Length == 1)
        {
            points[0] = new Vector2(transform.root.localPosition.x -328, transform.root.localPosition.y);
            enemyMarkers.Add(Instantiate(enemySpawnMarker, points[0], Quaternion.identity));
            enemyMarkers[0].transform.localScale *= 2;
        }
        else
        {
            for (int i = 0; i < points.GetLength(0); i++)
            {
                points[i] = new Vector2(transform.root.localPosition.x + Random.Range(-1100, 550), transform.root.localPosition.y + Random.Range(361, -550));
                enemyMarkers.Add(Instantiate(enemySpawnMarker, points[i], Quaternion.identity));
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
                playerTrigger = true;
            }
        }
    }
}

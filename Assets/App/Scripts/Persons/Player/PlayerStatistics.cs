using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    private PlayerCharacteristic _playerCharacteristic;

    [SerializeField] private int _takenAmethyst = 0;
    [SerializeField] private int _takenDamage = 0;
    [SerializeField] private int _dealtDamage = 0;
    [SerializeField] private int _enemiesDestroyed = 0;
    [SerializeField] private float _timePlayerAlive = 0;
    [SerializeField] private int _numbefShots = 0;

    public int TakenAmethyst  => _takenAmethyst;
    public int TakenDamage => _takenDamage;
    public int DealtDamage => _dealtDamage;
    public int Kills => _enemiesDestroyed;
    public float TimeAlive => _timePlayerAlive;
    public int NumbefShots => _numbefShots;

    private void Awake()
    {
        _playerCharacteristic = FindObjectOfType<PlayerCharacteristic>();
    }

    private void OnEnable()
    {
        _playerCharacteristic.OnHealthMinus += SetTakenDamage;
        _playerCharacteristic.OnAmethystAdd += SetTakenAmethysts;
    }
    private void OnDisable()
    {
        _playerCharacteristic.OnHealthMinus -= SetTakenDamage;
        _playerCharacteristic.OnAmethystAdd -= SetTakenAmethysts;
    }

    private void FixedUpdate()
    {
        _timePlayerAlive += Time.fixedDeltaTime;
    }

    private void SetTakenDamage(int damage)
    {
        _takenDamage += damage;
    }

    private void SetTakenAmethysts(int value)
    {
        if (value > 0)
            _takenAmethyst += value;
    }

    public void AddDealtDamage(int damage)
    {
        _dealtDamage += damage;
    }

    public void AddCountEnemyDestroyed()
    {
        _enemiesDestroyed++;
    }

    public void AddShot()
    {
        _numbefShots++;
    }


}

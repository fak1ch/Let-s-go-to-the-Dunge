using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverStats : MonoBehaviour
{
    private PlayerStatistics _stats;

    [SerializeField] private Text _takeDamage;
    [SerializeField] private Text _dealtDamage;
    [SerializeField] private Text _timeAlive;
    [SerializeField] private Text _kills;

    private void Awake()
    {
        _stats = FindObjectOfType<PlayerStatistics>();
    }

    private void OnEnable()
    {
        ChangeStats();
    }

    private void ChangeStats()
    {
        _takeDamage.text = _stats.TakenDamage.ToString();
        _dealtDamage.text = _stats.DealtDamage.ToString();
        ChangeTime();
        _kills.text = _stats.Kills.ToString();
    }

    private void ChangeTime()
    {
        if (_stats.TimeAlive < 1)
        {
            _timeAlive.text = "<1 sec wtf ? ??SDIJS AIJSFIJSAJ WHAT DUDE ?!?! ??!?";
        }
        else if (_stats.TimeAlive < 60)
        {
            int sec = Mathf.FloorToInt(_stats.TimeAlive);
            _timeAlive.text = $"{sec}sec";
        }
        else if(_stats.TimeAlive < 3600)
        {
            int time = Mathf.FloorToInt(_stats.TimeAlive);
            int rest = Mathf.FloorToInt(time % 60);
            int min = Mathf.FloorToInt((time - rest) / 60);
            _timeAlive.text = $"{min}m {rest}s";
        }
    }
}

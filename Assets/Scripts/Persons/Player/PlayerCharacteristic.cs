using System;
using System.Collections;
using UnityEngine;

public class PlayerCharacteristic : MonoBehaviour, IEntity
{
    public event Action<int, int> OnHealthChange;
    public event Action<int> OnHealthMinus;
    public event Action<int, int> OnManaChange;
    public event Action<int> OnAmethystChange;
    public event Action<int> OnAmethystAdd;
    public event Action OnPlayerDie;

    [SerializeField] private int _mana;
    [SerializeField] private int _maxMana;
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    private bool _immortalityOn = false;

    public int Amethists { get; set; }
    public int Mana { get { return _mana; } }

    private void Start()
    {
        _mana = _maxMana;
        OnHealthChange?.Invoke(_health, _maxHealth);
    }

    public void TakeDamage(int damage, GameObject whoKill)
    {
        if (!_immortalityOn)
        {
            _health -= damage;
            OnHealthChange?.Invoke(_health, _maxHealth);
            OnHealthMinus?.Invoke(damage);

            if (_health <= 0)
            {
                _immortalityOn = true;
                _health = 0;
                OnPlayerDie?.Invoke();
                StartCoroutine(PlayerSetFalse(0.1f));
            }
        }
    }


    public void TakeAmethyst(int value)
    {
        Amethists += value;

        OnAmethystChange?.Invoke(Amethists);
        OnAmethystAdd?.Invoke(value);
    }

    public void ChangeManaBar(int manacoast)
    {
        if (_mana + manacoast > _maxMana)
            _mana = _maxMana;
        else
            _mana += manacoast;

        OnManaChange?.Invoke(_mana,_maxMana);
    }

    public void HealHp(int hpCount)
    {
        if (_health + hpCount <= _maxHealth)
            _health += hpCount;
        else
            _health = _maxHealth;

        OnHealthChange?.Invoke(_health, _maxHealth);
    }

    public void OnImmortality(float i)
    {
        StartCoroutine(ImmortalityCoroutine(i));
    }

    private IEnumerator ImmortalityCoroutine(float i)
    {
        _immortalityOn = true;
        yield return new WaitForSeconds(i);
        _immortalityOn = false;
    }

    private IEnumerator PlayerSetFalse(float i)
    {
        yield return new WaitForSeconds(i);
        gameObject.SetActive(false);
    }
}

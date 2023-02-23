using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHp : MonoBehaviour
{
    [SerializeField] private Image _image;

    private void Start()
    {
        StartCoroutine(HideBossHp());
    }

    public void TakeDamage(int health, int maxHealth)
    {
        _image.fillAmount = (float)health / (float)maxHealth;
    }

    public void BossSpawned(int health, int maxHealth)
    {
        TakeDamage(health, maxHealth);
        StartCoroutine(ShowBossHp());
    }

    public void BossDie()
    {
        StartCoroutine(HideBossHp());
    }

    private IEnumerator ShowBossHp()
    {
        Image imageStroke = GetComponent<Image>();

        Color color = imageStroke.color;
        color.a = 0f;
        imageStroke.color = color;

        color = _image.color;
        color.a = 0f;
        _image.color = color;

        for (float i = 0f; i < 1; i += 0.05f)
        {
            color.a += i;
            imageStroke.color = color;
            _image.color = color;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator HideBossHp()
    {
        Image imageStroke = GetComponent<Image>();

        Color color = imageStroke.color;

        for (float i = 0f; i < 1; i += 0.05f)
        {
            color.a -= i;
            imageStroke.color = color;
            _image.color = color;
            yield return new WaitForSeconds(0.01f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject _loadingStart;
    [SerializeField] private GameObject _loadingEnd;
    [SerializeField] private Animator _loadingStartAnimator;
    [SerializeField] private Animator _loadingEndAnimator;

    [SerializeField] private float _durationStart = 1f;
    [SerializeField] private float _durationEnd = 1f;

    [SerializeField] private bool _isOpen = false;
    [SerializeField] private bool _isOpen1 = false;

    private MainScript _mainScript;
    private void Awake()
    {
        _mainScript = FindObjectOfType<MainScript>();
    }

    private void OnEnable()
    {
        _mainScript.OnGameLoad += LoadingLevelComplete;
    }

    private void OnDisable()
    {
        _mainScript.OnGameLoad -= LoadingLevelComplete;
    }

    public void LoadingLevelComplete()
    {
        StartCoroutine(LoadingStart());
    }

    public void LevelEnd()
    {
        StartCoroutine(LoadingEnd());
    }

    private IEnumerator LoadingStart()
    {
        _loadingStart.SetActive(true);
        _loadingStartAnimator.SetBool("isPlay", true);
        yield return new WaitForSeconds(_durationStart);
        _loadingStart.SetActive(false);

    }

    private IEnumerator LoadingEnd()
    {
        _loadingEnd.SetActive(true);
        yield return new WaitForSeconds(_durationEnd);
        _loadingEndAnimator.SetBool("isPlay", false);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class AudioSoundAfterDestroy : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private bool _isLocked = true;

    private void Update()
    {
        if (!_isLocked)
        {
            if (!_audioSource.isPlaying)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    public void PlaySoundAfterDestroy()
    {
        _isLocked = false;
        _audioSource.Play();
    }
}

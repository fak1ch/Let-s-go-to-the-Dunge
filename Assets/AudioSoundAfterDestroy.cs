using UnityEngine;
using UnityEngine.UI;

public class AudioSoundAfterDestroy : MonoBehaviour
{
    public AudioSource audioSource;
    private bool isLocked = true;

    private void Update()
    {
        if (!isLocked)
        {
            if (!audioSource.isPlaying)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

    public void PlaySoundAfterDestroy()
    {
        isLocked = false;
        audioSource.Play();
    }
}

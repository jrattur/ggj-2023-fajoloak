using UnityEngine;

public class PlayRandomMoleSound : MonoBehaviour
{
    public AudioClip[] sounds;
    public float betweenx;
    public float betweeny;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("PlayRandomSound", 0f, Random.Range(betweenx, betweeny));
    }

    private void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, sounds.Length);
        audioSource.clip = sounds[randomIndex];
        audioSource.Play();
    }
}

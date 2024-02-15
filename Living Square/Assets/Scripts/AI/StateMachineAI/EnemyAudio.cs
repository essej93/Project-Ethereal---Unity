using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyAudio script which is used to control the walking/running audio of the agent
// it changes the audio clip using the clips stored in the config
// and by calling the EnemyAudio script from the agent, we can have it call Run(), walk(), stop() functions respectively
public class EnemyAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AiAgent agent;
 
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<AiAgent>();
        audioSource = this.GetComponent<AudioSource>();
        

        audioSource.clip = agent.config.walkSound;
        
    }


    // Update is called once per frame
    void Update()
    {

        // if audio is playing it will constantly change the volume and pitch of the sound
        // as we are only using this script for walking/running then it will mildy modify the sound
        // to make it sound a little more realistic
        if (audioSource.isPlaying)
        {
            audioSource.volume = Random.Range(0.8f, 1);
            audioSource.pitch = Random.Range(0.8f, 1);
        }
        
    }

    public void Run()
    {
        audioSource.Stop();
        audioSource.clip = agent.config.runSound;
        audioSource.Play();
    }

    public void Walk()
    {
        audioSource.Stop();
        audioSource.clip = agent.config.walkSound;
        audioSource.Play();

    }

    public void Stop()
    {
        audioSource.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screaming : MonoBehaviour
{
    public AudioClip soundToPlay1;
    public AudioClip soundToPlay2;
    AudioSource audio1;
    AudioSource audio2;
    private bool alreadyPlayed1;
    public bool alreadyPlayed2;
    private bool crossedBoundary;
    private float volume;
    

    // Start is called before the first frame update
    void Start()
    {
        alreadyPlayed1 = false;
        alreadyPlayed2 = false;
        crossedBoundary = false;
        volume = 15f;
        Application.targetFrameRate = 300;
        audio1 = GameObject.FindGameObjectWithTag("Light House").GetComponent<AudioSource>();
        audio2 = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    //For Audio
    void OnTriggerEnter(Collider other)
    {
        if (!alreadyPlayed1 && !alreadyPlayed2)
        {
            audio1.PlayOneShot(soundToPlay1, volume);
            audio2.PlayOneShot(soundToPlay2, volume);
            alreadyPlayed1 = true;
            alreadyPlayed2 = true;
        }
        if (other.gameObject.tag == "Player")
        {
            crossedBoundary = true;
        }
    }

    //For GUI Message
    void OnGUI()
    {
        if (crossedBoundary)
        {
            GUI.TextField(new Rect(900, 15, 400, 35), "Move towards the Light House on the hills, someone is screaming!!!");
            StartCoroutine(BoxDisplay());
        }
    }
    IEnumerator BoxDisplay()
    {
        yield return new WaitForSeconds(15f);
        crossedBoundary = false;
        Destroy(GameObject.FindGameObjectWithTag("Cube"));
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private AudioCollection audioCollection;
    void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
    }
    // Start is called before the first frame update
    void Start()
    {
        audioCollection.PlayBGM(audioCollection.gameOver);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

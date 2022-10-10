using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Updates ScoreKeeper
// Singleton for the ScoreKeeper

public class ScoreKeeper : MonoBehaviour
{
    int score;

    static ScoreKeeper instance;
    
    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton() // allows ScoreKeeper to persist to next scene
    {
        if (instance != null) // if this is NOT the first ScoreKeeper to be instantiated, then destroy the ScoreKeeper
        {
            // disables it before destroying it on awake, in case other objects use the audio player
            gameObject.SetActive(false); 
            Destroy(gameObject);
        }
        else // if this is the first ScoreKeeper to be instantiated, then persist it
        {
            instance = this; // then set the instance as this ScoreKeeper (to persist it)
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int value)
    {
        score += value;
        Mathf.Clamp(score, 0, int.MaxValue); // prevent score < 0
        Debug.Log(score);
    }

    public void ResetScore()
    {
        score = 0;
    }
}

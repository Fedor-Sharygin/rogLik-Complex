using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] static private int frameRate = 60;
    static private GameManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            Application.targetFrameRate = frameRate;
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
}

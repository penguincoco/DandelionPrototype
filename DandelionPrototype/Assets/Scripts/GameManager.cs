using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private bool hasShakenBell = false;
    [SerializeField] private DandelionManager dandelionController;
    private bool dandelionInteractActive = false;

    [SerializeField] private MoveCharacter characterMover;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    public void SetBellStatus()
    {
        if (hasShakenBell == false)
            dandelionController.StartDandelionFlow();

        hasShakenBell = true;
        //dandelionInteractActive = true;
    }

    public bool GetDandelionInteractStatus()
    {
        return dandelionInteractActive;
    }

    public bool GetBellStatus()
    {
        return hasShakenBell;
    }

    //this is for if the player is in the interact zone, not if the dandelions have been activated or not yet 
    public void SetDandelionInteractStatus(bool status)
    {
        dandelionInteractActive = status;
    }

    public void MoveCharacter()
    {
        characterMover.MoveWrapper();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private bool hasShakenBell = false;
    [SerializeField] private DandelionManager dandelionController;
    private bool dandelionInteractActive = false;

    [SerializeField] private MoveCharacter characterMover;
    [SerializeField] private GameObject player;
    [SerializeField] private SplineContainer spline;
    [SerializeField] BezierKnot[] controlPoints;
    [SerializeField] private float flyTime; 

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        controlPoints = spline.Spline.ToArray();
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

    public void DandelionAnimationWrapper()
    {
        StartCoroutine(DandelionAnimation());
    }

    private IEnumerator DandelionAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        dandelionInteractActive = false;        //don't let the player re-activte the dandelion interaction if they've already flown away

        //SplineAnimate splineAnimator = new SplineAnimate();

        SplineAnimate splineAnimator = player.AddComponent<SplineAnimate>();
        splineAnimator.PlayOnAwake = false;
        splineAnimator.Easing = SplineAnimate.EasingMode.EaseInOut;
        splineAnimator.Duration = flyTime;
        splineAnimator.Loop = SplineAnimate.LoopMode.Once;
        splineAnimator.Container = this.spline;

        yield return new WaitForSeconds(0.1f);
        splineAnimator.Restart(true);
        splineAnimator.Play();

        while (splineAnimator.IsPlaying == true)
        {
            yield return null;
        }

        Destroy(splineAnimator);
    }
}

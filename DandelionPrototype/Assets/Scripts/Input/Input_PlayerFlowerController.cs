using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_PlayerFlowerController : MonoBehaviour
{
    private static Input_PlayerFlowerController _instance;
    public static Input_PlayerFlowerController Instance { get { return _instance; } }


    [Header("Player Variables")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float raycastDist;
    [SerializeField] private LayerMask interactableLayer;
    private bool isFacingTarget = false;

    [Header("Flower and Animation Variables")]
    [SerializeField] private Transform flowerHoldPos;
    [SerializeField] private GameObject flowerObject;
    [SerializeField] private float animationTime;
    [SerializeField] private GameObject animationParent;
    private bool isCarryingFlower = false;
    private bool flowerControlActive = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    public void OnPickFlower(InputValue inputValue)
    {
        //if the player is facing the thing they want to collect, but not holding a flower already
        if (isFacingTarget == true && isCarryingFlower == false && flowerControlActive)
        {
            isCarryingFlower = true;
            if (animationParent.activeInHierarchy == false)
            {
                animationParent.SetActive(true);
            }

            flowerObject.transform.parent = animationParent.transform;
            StartCoroutine(MoveFlower());
        }
    }

    public void OnShakeFlower(InputValue inputValue)
    {
        if (isCarryingFlower == true)
        {
            if (GameManager.Instance.GetDandelionInteractStatus() == false)
            {
                playerAnimator.Play("Anim_Wisp_FlowerShake");
                GameManager.Instance.SetBellStatus();
            }
            else if (GameManager.Instance.GetDandelionInteractStatus() == true && GameManager.Instance.GetBellStatus() == true)
                GameManager.Instance.MoveCharacter();
        }
    }

    private void Update()
    {
        if (flowerControlActive == true)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * raycastDist, Color.red);
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDist, interactableLayer))
            //if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDist))
            {
                //promptText.text = prompts[0];
                isFacingTarget = true;
            }
            else
            {
                //promptText.text = "";
                isFacingTarget = false;
            }
        }
    }

    private IEnumerator MoveFlower()
    {
        Transform flowerStartPos = flowerObject.transform;

        // Record the start time
        float startTime = Time.time;

        // Loop until the interpolation is complete
        while (Time.time - startTime < animationTime)
        {
            // Calculate the interpolation factor
            float t = (Time.time - startTime) / animationTime;

            // Lerp position
            flowerObject.transform.position = Vector3.Lerp(flowerStartPos.position, flowerHoldPos.position, t);

            // Lerp rotation
            flowerObject.transform.rotation = Quaternion.Lerp(flowerStartPos.rotation, flowerHoldPos.rotation, t);

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final position and rotation match the end transform exactly
        flowerObject.transform.position = flowerHoldPos.position;
        flowerObject.transform.rotation = flowerHoldPos.rotation;
    }

    public void SetFlowerMapActive()
    {
        flowerControlActive = true;
    }

    public void SetFlowerObject(GameObject flowerObject)
    {
        this.flowerObject = flowerObject;
    }
}

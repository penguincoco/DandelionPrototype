using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raincloud : MonoBehaviour
{
    [SerializeField] private float repulsionForce = 10f;
    [SerializeField] private float repulsionRange = 5f;

    [SerializeField] private float rainRate;
    [SerializeField] private float rainCapacity;
    [SerializeField] private ParticleSystem rainParticles;
    [SerializeField] private Color targetColorValley;
    [SerializeField] private Color targetColorPeak;
    [SerializeField] private float animationTime;
    [SerializeField] private Renderer cloudRenderer;
    private GameObject flowerObject;
    private bool isWateringFlower;
    [SerializeField] private AudioSource audioSrc;

    [SerializeField] private GameObject shadow;
    private Transform shadowPos;

    private void Start()
    {
        shadowPos = shadow.transform;
    }

    private void Update()
    {
        if (isWateringFlower == true)
            Water();
    }

    private void FixedUpdate()
    {
        // Get the position of the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return; // If player not found, exit

        // Calculate the distance between the player and the object
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check if the player is within the repulsion range
        if (distanceToPlayer <= repulsionRange)
        {
            // Calculate direction from the object to the player
            Vector3 direction = transform.position - player.transform.position;

            // Apply force in the direction opposite to the player
            GetComponent<Rigidbody>().AddForce(direction.normalized * repulsionForce, ForceMode.Force);
        }
    }

    private void Water()
    {
        flowerObject.gameObject.GetComponent<WaterableObject>().SetWater(rainRate);
        rainCapacity -= rainRate;

        if (rainCapacity < 0f)
        {
            isWateringFlower = false;
            rainParticles.Stop();

            StartCoroutine(OutOfWaterAnimation());
            StartCoroutine(FloatAwayAnimation());
        }
    }

    private IEnumerator OutOfWaterAnimation()
    {
        float elapsedTime = 0f;
        Material cloudMat = cloudRenderer.GetComponent<Renderer>().material;

        Color startValleyColor = cloudMat.GetColor("_Color_Valley");
        Color startPeakColor = cloudMat.GetColor("_Color_Peak");

        while (elapsedTime < animationTime)
        {
            float t = elapsedTime / animationTime;

            Color lerpedValley = Color.Lerp(startValleyColor, targetColorValley, t);
            Color lerpedPeak = Color.Lerp(startPeakColor, targetColorPeak, t);

            cloudMat.SetColor("_Color_Valley", lerpedValley);
            cloudMat.SetColor("_Color_Peak", lerpedPeak);

            //decrease the rain volume too
            audioSrc.volume = Mathf.Lerp(1, 0, elapsedTime / animationTime);

            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        cloudMat.SetColor("_Color_Valley", targetColorValley);
        cloudMat.SetColor("_Color_Peak", targetColorPeak);
    }

    private IEnumerator FloatAwayAnimation()
    {
        float elapsedTime = 0f;
        SpriteRenderer shadowSprite = shadow.gameObject.GetComponent<SpriteRenderer>();
        Color shadowSpriteColor = shadowSprite.color;
            
        shadow.transform.parent = null; //remove the shadow as a child so it doesn't float up with the cloud

        while (elapsedTime < animationTime * 4)
        {
            this.gameObject.transform.position += Vector3.up * Time.deltaTime;

            shadowSpriteColor.a = 1 - (elapsedTime / (animationTime * 4));

            shadowSprite.color = shadowSpriteColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(shadow);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, repulsionRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<WaterableObject>() != null)
        {
            isWateringFlower = true;
            flowerObject = other.gameObject;
        }
    }
}

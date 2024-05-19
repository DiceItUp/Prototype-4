using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody playerRB;
    public float playerSpeed = 5.0f;
   private GameObject focalPoint;
    public bool hasPowerup = false;
    private float powerupBoost = 12.0f;
    public GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
       focalPoint = GameObject.Find("FocalPoint");
        playerRB= GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        playerRB.AddForce(focalPoint.transform.forward * playerSpeed * forwardInput);
        playerRB.AddForce(focalPoint.transform.right * playerSpeed * horizontalInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup= true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position- transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerupBoost, ForceMode.Impulse);

            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            hasPowerup = false;
            powerupIndicator.SetActive(false);

        }
    }
}

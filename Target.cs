using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;

    public ParticleSystem targetParticle;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float torque = 10;
    private float xPosition = 4;
    private float yPosition = -2;

    public int targetPoint;
    public int livesGone;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        transform.position = RandomSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xPosition, xPosition), yPosition);
    }

    float RandomTorque()
    {
        return Random.Range(-torque, torque);
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(targetParticle, transform.position, targetParticle.transform.rotation);
            gameManager.UpdateScore(targetPoint);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateLives(livesGone);
        }
    }
}

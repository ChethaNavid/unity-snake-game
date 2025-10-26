using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your Snake head has tag Player
        {
            // Increase score
            ScoreManager.AddScore(1);

            // Destroy the food
            Destroy(gameObject);
        }
    }
}

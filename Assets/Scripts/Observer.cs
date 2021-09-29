using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;

    bool m_IsPlayerInRange;

    private void OnTriggerEnter(Collider other)
    {
        m_IsPlayerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        m_IsPlayerInRange = false;
    }

    private void Update()
    {
        if (m_IsPlayerInRange)
        {
            // perform a ray cast
            Vector3 directionOfRayCast = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, directionOfRayCast);

            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    // player came in line of sight of the observer
                    // end the game for player
                    Debug.Log("Player was detected by " + gameObject.transform.position);
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}

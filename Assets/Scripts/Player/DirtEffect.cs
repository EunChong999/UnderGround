using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticle;

    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField] float dirtFormationPeriod;

    UndergroundMovement undergroundMovement;

    float counter;

    private void Start()
    {
        undergroundMovement = GetComponent<UndergroundMovement>();
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (!undergroundMovement.isReached)
        {
            if (counter > dirtFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }
}

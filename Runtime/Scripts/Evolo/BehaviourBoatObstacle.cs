using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Addapted from https://discussions.unity.com/t/make-a-character-walk-around-randomly/83805 Tomas Barkan


namespace Evolo
{
    public class NPCController : MonoBehaviour
    {
        public float timeToChangeDirection = 5f;
        public float maxYawRate = 20f; // Maximum yaw rate in degrees per second
        private float toNextDirection;
        private float currentYawRate;
        private Rigidbody rb;

        public float speed = 2f; // Forward movement speed

        public void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.isKinematic = true; // Ensure Rigidbody is kinematic
            ChangeYawRate();
        }

        private void FixedUpdate()
        {
            toNextDirection -= Time.fixedDeltaTime;

            if (toNextDirection <= 0)
            {
                ChangeYawRate();
            }

            // Calculate new rotation
            Quaternion deltaRotation = Quaternion.Euler(0, currentYawRate * Time.fixedDeltaTime, 0);
            rb.MoveRotation(rb.rotation * deltaRotation);

            // Move forward
            Vector3 forwardMovement = transform.forward * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMovement);
        }

        private void ChangeYawRate()
        {
            currentYawRate = (currentYawRate == 0) ? Random.Range(-maxYawRate, maxYawRate) : 0;
            toNextDirection = timeToChangeDirection;
        }
    }
}

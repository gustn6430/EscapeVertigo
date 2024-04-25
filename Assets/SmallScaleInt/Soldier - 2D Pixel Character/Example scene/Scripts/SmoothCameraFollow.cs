using UnityEngine;

namespace SmallScaleInteractive._2DCharacter
{
    public class SmoothCameraFollow : MonoBehaviour
    {
        public Transform[] targets; // Array of possible targets the camera can follow
        public float smoothSpeed = 0.125f; // Adjust this to make the camera follow faster or slower
        public Vector3 offset; // Offset from the target (Adjust based on your scene setup)

        private Transform currentTarget; // The current target the camera is following

        void Update()
        {
            UpdateCurrentTarget();
        }

        void LateUpdate()
        {
            if (currentTarget == null) return; // Exit if there's no target

            Vector3 desiredPosition = currentTarget.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Optional: Make the camera look at the target
            transform.LookAt(currentTarget);
        }

        void UpdateCurrentTarget()
        {
            foreach (var potentialTarget in targets)
            {
                if (potentialTarget.gameObject.activeInHierarchy)
                {
                    currentTarget = potentialTarget;
                    break; // Exit the loop once the active target is found
                }
            }
        }
    }
}

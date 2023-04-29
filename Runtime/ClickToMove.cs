using UnityEngine;
using UnityEngine.Events;

namespace FSS.Settings.RoomConfig
{
    public class ClickToMove : MonoBehaviour
    {
        private bool isMoving = false; // Flag to indicate if the cube is currently being moved
        private Vector3 mouseDownPosition; // The position where the mouse button was pressed down
        private Vector3 initialPosition; // The initial position of the cube
        [field: SerializeField]
        public Vector3 MovementLimits { get; set; } = new Vector3(1.5f, 0, 4f);
        public UnityEvent OnEndMovement;
        public bool IsMovingX { get; set; } = true;


        void Awake()
        {
            //SetPositionFromDistance(FindObjectOfType<RoomController>().set)
            initialPosition = transform.position;
        }

        void Update()
        {
            // Check if the mouse button is pressed down
            if (Input.GetMouseButtonDown(0))
            {
                // Cast a ray from the camera to the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // Check if the ray hits the cube
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    // Set the flag to indicate that the cube is being moved
                    isMoving = true;
                    // Save the position where the mouse button was pressed down
                    mouseDownPosition = Input.mousePosition;
                    Cursor.visible = false;
                }
            }

            // Check if the mouse button is released
            if (Input.GetMouseButtonUp(0))
            {
                // Reset the flag to indicate that the cube is no longer being moved
                isMoving = false;
                OnEndMovement.Invoke();
                Cursor.visible = true;
            }

            // Check if the cube is being moved
            if (isMoving)
            {
                // Calculate the amount of movement based on the difference between the current mouse position
                // and the position where the mouse button was pressed down
                Vector3 movement = Input.mousePosition - mouseDownPosition;
                // Scale the movement so it's not too fast or slow
                movement *= 0.0025f;
                // Limit the movement to the X and Z axes only
                if(!IsMovingX)
                {
                    movement.x = 0;
                }
                movement.z = movement.y;
                movement.y = 0;
                // Move the cube by the calculated amount
                transform.position = initialPosition + movement;
            }
            else
            {
                //Set initial Position when you stop moving
                initialPosition = transform.position;
            }
        }
    }
}

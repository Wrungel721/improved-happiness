using UnityEngine;

namespace LearnProject.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon*Mathf.Epsilon;

        [SerializeField]
        public float speed = 1.5f;
        [SerializeField]
        private float _maxRadiusDelta = 10f;
        
        public Vector3 MovementDirection { get; set; }
        public Vector3 VisionDirection { get; set; }

        private CharacterController _characterController;


        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        protected void Update()
        {
            Translate();

            if (_maxRadiusDelta > 0f && VisionDirection != Vector3.zero)
                Rotate();
        }

        private void Translate() {

            var delta = MovementDirection * speed * Time.deltaTime;
            _characterController.Move(delta);
        }

        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnitude = (currentLookDirection - VisionDirection).sqrMagnitude;

            if (sqrMagnitude > SqrEpsilon)
            {
                var newRotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(VisionDirection, Vector3.up),
                    _maxRadiusDelta * Time.deltaTime);
                transform.rotation = newRotation;
            }
        }
    }
}
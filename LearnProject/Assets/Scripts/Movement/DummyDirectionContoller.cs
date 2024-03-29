using UnityEngine;

namespace LearnProject.Movement
{
    public class DummyDirectionContoller : MonoBehaviour, IMovementDirectionSource
    {
        public Vector3 MovementDirection {  get; private set; }

        protected void Awake()
        {
            MovementDirection = Vector3.zero;
        }
    }
}
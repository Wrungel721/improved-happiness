﻿
using LearnProject.Movement;
using UnityEngine;

namespace LearnProject.Enemy
{
    public class EnemyDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        
        public Vector3 MovementDirection {  get; private set; }
        public void UpdateMovementDirection(Vector3 targetPosition)
        {
            var realDirection = targetPosition - transform.position;
            MovementDirection = new Vector3(realDirection.x, 0, realDirection.z).normalized;
        }
    }
}
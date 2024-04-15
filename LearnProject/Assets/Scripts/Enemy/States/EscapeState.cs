using LearnProject.FSM;
using UnityEngine;

namespace LearnProject.Enemy.States
{
    public class EscapeState : BaseState
    {
        private readonly EnemyTarget _target;
        private readonly EnemyDirectionController _enemyDirectionController;
        private readonly EnemyCharacter _enemyCharacter;
        private Vector3 _currentPoint;

        public EscapeState(EnemyTarget target, EnemyDirectionController enemyDirectionController, EnemyCharacter enemyCharacter)
        {
            _target = target;
            _enemyDirectionController = enemyDirectionController;
            _enemyCharacter = enemyCharacter;

        }
        public override void Execute()
        {
            Vector3 targetPosition = _target.Closest.transform.position;
            targetPosition.x *= -0.1f;
            targetPosition.z *= -0.1f;

            Debug.Log("ESCAPE");

            if (_currentPoint != targetPosition)
            {
                _enemyCharacter.characterMovementController.speed *= _enemyCharacter.boost + 1;
                _currentPoint = targetPosition;
                _enemyDirectionController.UpdateMovementDirection(targetPosition);

            }
        }
    }
}

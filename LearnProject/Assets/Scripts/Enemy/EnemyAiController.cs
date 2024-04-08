
using System.Runtime.CompilerServices;
using LearnProject.Enemy.States;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace LearnProject.Enemy
{
    public class EnemyAiController : MonoBehaviour
    {

        [SerializeField]
        private float _viewRadius = 20f;
        private EnemyTarget _target;
        private EnemyStateMachine _stateMachine;
        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacter>();
            var enemyDirectionController = GetComponent<EnemyDirectionController>();
            var enemyCharacter = GetComponent<EnemyCharacter>();
            var navMesher = new NavMesher(transform);
            _target = new EnemyTarget(transform, _viewRadius, player, enemyCharacter);
            _stateMachine = new EnemyStateMachine(enemyDirectionController, enemyCharacter, navMesher, _target);
        }

        protected void Update()
        {
            _target.FindClosest();
            _stateMachine.Update();
        }
    }
}
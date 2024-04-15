using UnityEngine;

namespace LearnProject.Enemy
{
    [RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAiController))]
    public class EnemyCharacter : BaseCharacter
    {
        [SerializeField]
        public float boost = 0.5f;
        public float boarderHealth = 2f;

        [SerializeField]
        public int _braveness = 70;

    }
}
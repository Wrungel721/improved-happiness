using UnityEngine;

namespace LearnProject.Enemy
{
    [RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAiController))]
    public class EnemyCharacter : BaseCharacter
    {
        [SerializeField]
        private float _toughness = 0.3f;
        [SerializeField]
        public float boost = 0.5f;
        public float toughnessGet => _toughness * health;

        [SerializeField]
        public int _braveness = 70;

    }
}
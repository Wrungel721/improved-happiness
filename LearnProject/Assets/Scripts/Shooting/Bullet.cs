using UnityEngine;


namespace LearnProject.Shooting
{

    public class Bullet : MonoBehaviour
    {
        public float Damage { get; private set; }

        private Vector3 _direction;
        private float _flySpeed;
        private float _maxFlyDistance;
        private float _currentflyDistance;
        


        public void Initialize(Vector3 direction, float maxFlyDistance, float flySpeed, float bulletDamage)
        {
            _direction = direction;
            _flySpeed = flySpeed;
            _maxFlyDistance = maxFlyDistance;

            Damage = bulletDamage;
        }
        protected void Update()
        {
            var delta = _flySpeed * Time.deltaTime;
            _currentflyDistance += delta;
            transform.Translate(_direction * delta);
            if (_currentflyDistance >= _maxFlyDistance)
                Destroy(gameObject);


        }
    }
}
using Unity.VisualScripting;
using UnityEngine;

namespace LearnProject.Shooting
{
    public class ShootingController : MonoBehaviour
    {
        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => _target.transform.position;


        public Weapon weapon { get; private set; }

        private Collider[] _colliders = new Collider[2];
        private float _nextShotTimerSec;
        private GameObject _target;

        protected void Update()

        {
            _target = GetTarget();

            _nextShotTimerSec -= Time.deltaTime;
            if (_nextShotTimerSec < 0 )
            {
                if (HasTarget)
                    weapon.Shoot(TargetPosition);

                _nextShotTimerSec = weapon.ShootFrequencySec;
            }
        }

        public void SetWeapon( Weapon weaponPrefab, Transform hand)
        {
            if (weapon != null)
                Destroy(weapon.gameObject);
            weapon = Instantiate(weaponPrefab, hand);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;


        
        }

        private GameObject GetTarget()
        {
            GameObject target = null;

            var position = weapon.transform.position;
            var radius = weapon.ShootRadius;
            var mask = LayerUtils.EnemyMask;

            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, mask);
            if (size > 1)
            {
                for (int  i = 0; i < size; i++)
                {
                    if (_colliders[i].gameObject != gameObject)
                    {
                        target = _colliders[i].gameObject;
                        break;
                    }
                }
            }

            return target;
        }
    }
}
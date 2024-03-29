 using LearnProject.Movement;
using LearnProject.PickUp;
using LearnProject.Items;
using LearnProject.Shooting;
using UnityEngine;

namespace LearnProject
{
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]

    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        private Weapon _baseWeaponPrefab;

        [SerializeField]
        private Transform _hand;


        [SerializeField]
        private float _health = 2f;

        public CharacterMovementController _characterMovementController;

        private IMovementDirectionSource _movementDirectionSource;
        private ShootingController _shootingController;
        private SpeedController _speedController;

        private void Awake()
        {
            _characterMovementController = GetComponent<CharacterMovementController>();
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _shootingController = GetComponent<ShootingController>();
            _speedController = GetComponent<SpeedController>();
        }

        protected private void Start()
        {
            SetWeapon(_baseWeaponPrefab);
        }

        void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            var visionDirection = direction;
            if (_shootingController.HasTarget)
                visionDirection = (_shootingController.TargetPosition - transform.position).normalized; 


            _characterMovementController.MovementDirection = direction;
            _characterMovementController.VisionDirection = visionDirection;


            if (_health<= 0f)
                Destroy(gameObject);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();

                _health -= bullet.Damage;

                Destroy(other.gameObject);
            }
            else if (LayerUtils.IsPickUp(other.gameObject))
            {
                var pickUp = other.gameObject.GetComponent<PickUpWeapon>();
                pickUp.PickUp(this);    

                Destroy(other.gameObject);
            }
            else if (LayerUtils.IsPickUpBonus(other.gameObject))
            {
                var pickUp = other.gameObject.GetComponent<PickUpBonus>();
                pickUp.PickUp(this);

                Destroy(other.gameObject);
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, _hand);
        }

        public void SetBonus(SpeedBonus bonus)
        {
            _speedController.SetBonus(bonus, _characterMovementController);
        }

    }
}
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
        public Weapon baseWeaponPrefab;

        [SerializeField]
        public Transform hand;

        [SerializeField]
        private Animator _animator;

        public bool weaponChanged = false;

        [SerializeField]
        public float health { get; private set; } = 5f;

        public CharacterMovementController characterMovementController { get; private set; }
        private IMovementDirectionSource _movementDirectionSource;
        public ShootingController shootingController { get; private set; }
        private SpeedController _speedController;

        private void Awake()
        {
            characterMovementController = GetComponent<CharacterMovementController>();
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            shootingController = GetComponent<ShootingController>();
            _speedController = GetComponent<SpeedController>();
        }

        protected private void Start()
        {
            SetWeapon(baseWeaponPrefab);
        }

        void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            var visionDirection = direction;
            if (shootingController.HasTarget)
                visionDirection = (shootingController.TargetPosition - transform.position).normalized; 


            characterMovementController.MovementDirection = direction;
            characterMovementController.VisionDirection = visionDirection;

            _animator.SetBool("IsMoving", direction != Vector3.zero);
            _animator.SetBool("IsShooting", shootingController.HasTarget);
            _animator.SetTrigger("Jump");

            if (health<= 0f)
            {
                _animator.SetTrigger("Dead");
                Destroy(gameObject, _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            }
            
            
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();

                health -= bullet.Damage;

                Destroy(other.gameObject);
            }
            else if (LayerUtils.IsPickUp(other.gameObject) || LayerUtils.IsPickUpBonus(other.gameObject)    )
            {
                var pickUp = other.gameObject.GetComponent<PickUpItem>();
                pickUp.PickUp(this);    

                Destroy(other.gameObject);
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            shootingController.SetWeapon(weapon, hand);
            if (weapon != baseWeaponPrefab)
                weaponChanged = true;
        }

        public void SetBonus(SpeedBonus bonus)
        {
            _speedController.SetBonus(bonus);
        }

    }
}
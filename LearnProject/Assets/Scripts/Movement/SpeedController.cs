using LearnProject.Items;
using UnityEngine;

namespace LearnProject.Movement
{
    public class SpeedController : MonoBehaviour
    {
        [SerializeField]
        private float _speedBoost = 1f;

        private bool BonusActive = false;

        private float originalSpeed;

        private float bonusDuration;

        private float bonusTimerSec;


        private CharacterMovementController characterMovement;

        void Start()
        {
            characterMovement = GetComponent<CharacterMovementController>();
            originalSpeed = characterMovement._speed;
        }


        void Update()
        {


            if (BonusActive)
            {
                bonusTimerSec += Time.deltaTime;
                if (bonusTimerSec >= bonusDuration)
                {
                    BonusActive = false;
                    Debug.Log("BONUS DEACTIVATED");
                    characterMovement._speed = originalSpeed;
                }

            }


        }


        public void SetBonus(SpeedBonus Bonus, CharacterMovementController characterMovementController)
        {
            BonusActive = true;
            Debug.Log("BONUSACTIVE");
            bonusTimerSec = 0;
            bonusDuration = Bonus.ModifierTimer;
            Debug.Log(bonusDuration);
            characterMovementController._speed *= Bonus.SpeedModifier;
            Debug.Log(characterMovementController._speed);

        }
    }
}
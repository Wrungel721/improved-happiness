using LearnProject.Items;
using UnityEngine;

namespace LearnProject.Movement
{
    public class SpeedController : MonoBehaviour
    {

        private bool BonusActive = false;

        private float originalSpeed;

        private float bonusDuration;

        private float bonusTimerSec;


        private CharacterMovementController characterMovement;

        void Start()
        {
            characterMovement = GetComponent<CharacterMovementController>();
            originalSpeed = characterMovement.speed;
        }


        void Update()
        {


            if (BonusActive)
            {
                bonusTimerSec += Time.deltaTime;
                if (bonusTimerSec >= bonusDuration)
                {
                    BonusActive = false;
                    characterMovement.speed = originalSpeed;
                }

            }


        }


        public void SetBonus(SpeedBonus Bonus)
        {
            BonusActive = true;
            bonusTimerSec = 0;
            bonusDuration = Bonus.ModifierTimer;
            characterMovement.speed *= Bonus.SpeedModifier;

        }
    }
}
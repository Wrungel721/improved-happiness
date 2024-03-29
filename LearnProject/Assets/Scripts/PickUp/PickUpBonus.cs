using LearnProject.Items;
using UnityEngine;

namespace LearnProject.PickUp
{
    public class PickUpBonus : PickUpItem
    {
        [SerializeField]
        private SpeedBonus _bonusPrefab;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetBonus(_bonusPrefab);
        }
    }
}
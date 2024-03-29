using UnityEditor;
using UnityEngine;

namespace LearnProject.PickUp
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField]
        private PickUpItem _pickUpPrefab;

        [SerializeField]
        private float _range = 2f;

        [SerializeField]
        private int _maxCount = 2;

        [SerializeField]
        private float _spawnMinIntervalSeconds = 5f;
        [SerializeField]
        private float _spawnMaxIntervalSeconds = 10f;

        private float _currentSpawnTimerSeconds;
        private int _currentCount;

        // Update is called once per frame
        void Update()
        {
            if (_currentCount < _maxCount)
            {
                _currentSpawnTimerSeconds += Time.deltaTime;

                var spawnIntervalSeconds = RandomInterval(_spawnMinIntervalSeconds, _spawnMaxIntervalSeconds);

                if (_currentSpawnTimerSeconds > spawnIntervalSeconds)
                {
                    _currentSpawnTimerSeconds = 0f;
                    _currentCount++;

                    var randomPointInsideRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;

                    var pickUp = Instantiate(_pickUpPrefab, randomPosition, Quaternion.identity, transform);
                    pickUp.OnPickedUp += OnItemPickedUp;
                }
            }
        }

        private static float RandomInterval(float min, float max)
        {
            float range = max - min;
            float sample = Random.value;
            double scaled = (sample * range) + min;
            return (float)scaled;
        }

        private void OnItemPickedUp(PickUpItem pickUpItem)
        {
            _currentCount--;
            pickUpItem.OnPickedUp -= OnItemPickedUp;
        }

        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cashedColor;
        }
    }
}
using LearnProject.Enemy;
using UnityEditor;
using UnityEngine;

namespace LearnProject.Spawner
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemyCharacter _enemyPrefab;
        [SerializeField]
        private PlayerCharacter _playerPrefab;

        [SerializeField]
        private float _range = 2f;

        [SerializeField]
        private float _spawnMinIntervalSeconds = 5f;
        [SerializeField]
        private float _spawnMaxIntervalSeconds = 10f;

        private float _currentSpawnTimerSeconds;

        // Update is called once per frame
        void Update()
        {
            var player = FindObjectOfType<PlayerCharacter>();
            _currentSpawnTimerSeconds += Time.deltaTime;
            if (player == null)
            {
                if (_currentSpawnTimerSeconds > RandomInterval(_spawnMinIntervalSeconds, _spawnMaxIntervalSeconds))
                {
                    _currentSpawnTimerSeconds = 0f;

                    var randomPointInsideRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;

                    float rnd = Random.value;
                    /* if (rnd < 0.3f)
                         Instantiate(_enemyPrefab, randomPosition, Quaternion.identity, transform);
                     else*/
                    Instantiate(_playerPrefab, randomPosition, Quaternion.identity, transform);
                }

            }
            else
            {

                if (_currentSpawnTimerSeconds > RandomInterval(_spawnMinIntervalSeconds, _spawnMaxIntervalSeconds))
                {
                    _currentSpawnTimerSeconds = 0f;

                    var randomPointInsideRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;

                    Instantiate(_enemyPrefab, randomPosition, Quaternion.identity, transform);
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

        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cashedColor;
        }
    }
}
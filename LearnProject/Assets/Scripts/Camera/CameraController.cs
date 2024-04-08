using System;
using UnityEngine;

namespace LearnProject.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _followCamerOffset = Vector3.zero;
        [SerializeField]
        private Vector3 _rotationOffset = Vector3.zero;
        private GameObject _player = null;

        protected void Update()
        {
            if (_player == null)
                _player = FindObjectOfType<PlayerCharacter>().gameObject;


        }

        protected void LateUpdate()
        {
            if (_player != null)
            {
                Vector3 targetRotation = _rotationOffset - _followCamerOffset;

                transform.position = _player.transform.position + _followCamerOffset;
                transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
            }

        }
    }
}
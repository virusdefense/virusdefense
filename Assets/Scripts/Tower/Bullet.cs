using System;
using UnityEngine;

namespace Tower
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _target;
        private float _speed = 30f;

        public void Seek(Vector3 target)
        {
            _target = target;
        }

        public void Update()
        {
            var step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target, step);
        }
    }
}
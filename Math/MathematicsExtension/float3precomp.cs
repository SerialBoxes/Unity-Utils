using System;
using Unity.Mathematics;

namespace UnityUtils.Mathematics {
    /// <summary>
    /// A float3 with precomputed direction and length for speed (with proper checking for zero length vectors)
    /// </summary>
    [Serializable]
    public struct float3precomp {
        private float3 _vector;
        private float _length;
        private float3 _direction;
        public float3 vector { get => _vector; set => Update(value); }
        public float length => _length;
        public float3 direction => _direction;

        public float3precomp(float3 vector) {
            _vector = float3.zero;
            _length = 0;
            _direction = float3.zero;
            Update(vector);
        }

        private void Update(float3 vector) {
            _vector = vector;
            _length = roxmath.clampDown(math.length(vector));
            _direction = _length > 0 ? vector / _length : float3.zero;
        }
    }
}
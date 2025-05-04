using System;
using UnityEngine;

namespace RunTime.Data.ValueObjects
{
    [Serializable]
    public struct PlayerJumpData
    {
        public LayerMask groundLayerMask;
        public float groundCheckSize;
        public float jumpForce;
    }
}
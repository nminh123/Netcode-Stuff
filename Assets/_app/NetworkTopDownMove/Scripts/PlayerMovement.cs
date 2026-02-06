using NS.Utils.Input;
using Unity.Netcode;
using UnityEngine;

using static NS.Utils.Configuration.NetworkTopDownMove;

namespace NS.NetworkTopDownMove
{
    public class PlayerMovement : NetworkBehaviour
    {
        [Header("References")]

        [SerializeField]
        private InputReader m_input;
        [SerializeField]
        private Rigidbody2D m_rb;

        public override void OnNetworkSpawn()
        {
            if(!IsOwner) return;
            m_input.MoveEvent += HandleMove;
        }

        public override void OnNetworkDespawn()
        {
            if(!IsOwner) return;
            m_input.MoveEvent -= HandleMove;
        }

        private void HandleMove(Vector2 move_input)
        {
            if(!IsOwner) return;
            HandleMoveRpc(direction: move_input);
        }

        [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Owner)]
        private void HandleMoveRpc(Vector2 direction)
        {
            m_rb.linearVelocity = direction * SPEED;
        }
    }
}
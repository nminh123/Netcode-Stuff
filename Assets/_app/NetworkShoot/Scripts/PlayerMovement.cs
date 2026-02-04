using NS.Utils.Input;
using Unity.Netcode;
using UnityEngine;

using static NS.Utils.Configuration.NetworkShoot;

namespace NS.NetworkShoot
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private Rigidbody2D m_rigid;
        [SerializeField] private InputReader m_input;

        public override void OnNetworkSpawn()
        {
            if(!IsOwner) return;
            m_input.MoveEvent += MoveServerRpc;
        }

        public override void OnNetworkDespawn()
        {
            if(!IsOwner) return;
            m_input.MoveEvent -= MoveServerRpc;
        }

        [Rpc(SendTo.Server)]
        private void MoveServerRpc(Vector2 input_direction)
        {
            if(!IsServer) return;
            m_rigid.linearVelocityX = input_direction.x * SPEED;
        }
    }
}
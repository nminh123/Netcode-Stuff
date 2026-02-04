using NS.Utils.Input;
using Unity.Netcode;
using UnityEngine;

using static NS.Utils.Configuration.NetworkShoot;

namespace NS.NetworkShoot
{
    public class BulletShoot : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject m_bulletPrefabs;
        [SerializeField] private Transform m_bulletSpawnPosition;
        [SerializeField] private InputReader m_input;

        public override void OnNetworkSpawn()
        {
            // ❗ CHỈ OWNER mới được gửi request
            if (!IsOwner) return;
            m_input.ShootEvent += HandleShoot;
        }

        public override void OnNetworkDespawn()
        {
            if (!IsOwner) return;
            m_input.ShootEvent -= HandleShoot;
        }

        // =========================
        // REQUEST (Client → Server)
        // =========================
        private void HandleShoot()
        {
            if (!IsOwner) return;
            ShootServerRpc();
        }

        // =========================
        // VALIDATE + EXECUTE (Server)
        // =========================
        [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Owner)]
        private void ShootServerRpc(RpcParams rpcParams = default)
        {
            // 1️⃣ Validate ownership
            if (rpcParams.Receive.SenderClientId != OwnerClientId)
                return;

            // 2️⃣ Validate game rule (ví dụ)
            if (!CanShoot())
                return;

            // 3️⃣ Server spawn bullet
            var bullet = Instantiate(
                m_bulletPrefabs,
                m_bulletSpawnPosition.position,
                m_bulletSpawnPosition.rotation
            );

            var netObj = bullet.GetComponent<NetworkObject>();
            netObj.Spawn(); // ❗ server-owned projectile

            // 4️⃣ Apply movement on SERVER
            if (bullet.TryGetComponent(out Rigidbody2D rb))
            {
                rb.linearVelocity = m_bulletSpawnPosition.right * SPEED;
            }
        }

        // =========================
        // VALIDATION LOGIC
        // =========================
        private bool CanShoot()
        {
            // ví dụ: cooldown, ammo, trạng thái
            return true;
        }
    }
}

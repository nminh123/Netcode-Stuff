using UnityEngine;

using static NS.Utils.Configuration.NetworkShoot;

namespace NS.NetworkShoot
{
    public class Lifetime : MonoBehaviour
    {
        private void Start() => Destroy(this.gameObject, BULLET_LIFE_TIME);
    }
}
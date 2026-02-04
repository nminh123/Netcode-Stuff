using System;
using UnityEngine;
using UnityEngine.InputSystem;

using static NS.Utils.Input.Controls;

namespace NS.Utils.Input
{
    [CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public event Action<Vector2> MoveEvent;
        public event Action ShootEvent;
        private Controls m_controls;

        private void OnEnable()
        {
            if(m_controls == null)
            {
                m_controls = new Controls();
                m_controls.Player.SetCallbacks(this);
            }
            m_controls.Player.Enable();
        }

        private void OnDisable()
        {
            m_controls.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context) => MoveEvent?.Invoke(context.ReadValue<Vector2>());

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                ShootEvent?.Invoke();
            }
        }
    }
}
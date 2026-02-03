using System;
using UnityEngine;
using UnityEngine.InputSystem;

using static NS.Utils.Input.Controls;

namespace NS.Utils.Input
{
    public class InputReader : MonoBehaviour, IPlayerActions
    {
        public event Action<Vector2> MoveEvent;
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
    }
}
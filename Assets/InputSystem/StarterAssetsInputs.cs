using System;
using UnityEngine;
using Random = UnityEngine.Random;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")] public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool buttonX;
        private bool _buttonY;
        private bool Q, E;

        public bool buttonY
        {
            get
            {
                if (_buttonY && Random.Range(0, 100) > 98)
                    jump = true;
                return _buttonY;
            }
            set { _buttonY = value; }
        }

        [Header("Movement Settings")] public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
        [Header("Mouse Cursor Settings")] public bool cursorLocked = true;
        public bool cursorInputForLook = true;
#endif
        private Gamepad _gamepad;

        private void Awake()
        {
            _gamepad = UnityEngine.InputSystem.Gamepad.current;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) && !Q)
                Q = true;
            if (Input.GetKeyUp(KeyCode.Q) && Q)
                Q = false;
            if (Input.GetKeyDown(KeyCode.E) && !E)
                E = true;
            if (Input.GetKeyUp(KeyCode.E) && E)
                E = false;
                
            buttonX = _gamepad.buttonWest.isPressed || Q;
            buttonY = _gamepad.buttonNorth.isPressed || E;
        }
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

#if !UNITY_IOS || !UNITY_ANDROID

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

#endif
    }
}
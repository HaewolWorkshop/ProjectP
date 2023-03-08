using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace HaewolWorkshop
{
    public partial class Player
    {
        public enum ButtonActions // bool 값으로 입력받는 액션
        {
            Henshin,
            Aim,
            Attack,
            Grab,
            Guard,
            Jump,
            Sprint,
            Evade,
            Stealth,
            Skill1,
            Skill2,
        }

        public enum ValueActions //float 값으로 입력받는 액션
        {
        }


        // 벡터넘겨주는 액션은 따로 처리
        public UnityAction<Vector2> onMove { get; set; }
        private InputVector2Damper moveInputDamper = new InputVector2Damper();

        public UnityAction<Vector2> onLook { get; set; }


        private Dictionary<ButtonActions, InputAction> buttonActions;
        private Dictionary<ButtonActions, UnityAction<bool>> buttonEvents;

        private Dictionary<ValueActions, InputAction> valueActions;
        private Dictionary<ValueActions, UnityAction<bool>> valueEvents;


        private PlayerInputActions inputActions;
        private InputAction moveInputAction;
        private InputAction lookInputAction;

        private void InitInputs()
        {
            buttonActions = new Dictionary<ButtonActions, InputAction>();
            buttonEvents = new Dictionary<ButtonActions, UnityAction<bool>>();

            valueActions = new Dictionary<ValueActions, InputAction>();
            valueEvents = new Dictionary<ValueActions, UnityAction<bool>>();

            inputActions = new global::PlayerInputActions();

            moveInputAction = inputActions.Player.Move;
            lookInputAction = inputActions.Player.Look;

            buttonActions.Add(ButtonActions.Attack, inputActions.Player.Attack);
            inputActions.Player.Attack.started += (x) => GetAction(ButtonActions.Attack)?.Invoke(true);
            inputActions.Player.Attack.canceled += (x) => GetAction(ButtonActions.Attack)?.Invoke(false);

            buttonActions.Add(ButtonActions.Jump, inputActions.Player.Jump);
            inputActions.Player.Jump.started += (x) => GetAction(ButtonActions.Jump)?.Invoke(true);
            inputActions.Player.Jump.canceled += (x) => GetAction(ButtonActions.Jump)?.Invoke(false);

            buttonActions.Add(ButtonActions.Stealth, inputActions.Player.Stealth);
            inputActions.Player.Stealth.started += (x) => GetAction(ButtonActions.Stealth)?.Invoke(true);
            inputActions.Player.Stealth.canceled += (x) => GetAction(ButtonActions.Stealth)?.Invoke(false);

            buttonActions.Add(ButtonActions.Sprint, inputActions.Player.Sprint);
            inputActions.Player.Sprint.started += (x) => GetAction(ButtonActions.Sprint)?.Invoke(true);
            inputActions.Player.Sprint.canceled += (x) => GetAction(ButtonActions.Sprint)?.Invoke(false);

            buttonActions.Add(ButtonActions.Henshin, inputActions.Player.Henshin);
            inputActions.Player.Henshin.started += (x) => GetAction(ButtonActions.Henshin)?.Invoke(true);
            inputActions.Player.Henshin.canceled += (x) => GetAction(ButtonActions.Henshin)?.Invoke(false);

            buttonActions.Add(ButtonActions.Evade, inputActions.Player.Evade);
            inputActions.Player.Evade.started += (x) => GetAction(ButtonActions.Evade)?.Invoke(true);
            inputActions.Player.Evade.canceled += (x) => GetAction(ButtonActions.Evade)?.Invoke(false);

            buttonActions.Add(ButtonActions.Aim, inputActions.Player.Aim);
            inputActions.Player.Aim.started += (x) => GetAction(ButtonActions.Aim)?.Invoke(true);
            inputActions.Player.Aim.canceled += (x) => GetAction(ButtonActions.Aim)?.Invoke(false);

            buttonActions.Add(ButtonActions.Grab, inputActions.Player.Grab);
            inputActions.Player.Grab.started += (x) => GetAction(ButtonActions.Grab)?.Invoke(true);
            inputActions.Player.Grab.canceled += (x) => GetAction(ButtonActions.Grab)?.Invoke(false);

            // 임시
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void UpdateInputs()
        {
            var moveInput = moveInputDamper.getDampedValue(moveInputAction.ReadValue<Vector2>(), Time.deltaTime);
            var lookInput = lookInputAction.ReadValue<Vector2>();

            onMove?.Invoke(moveInput);
            onLook?.Invoke(lookInput);
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }


        private UnityAction<bool> GetAction(ButtonActions type)
        {
            if (buttonEvents.TryGetValue(type, out var action))
            {
                return action;
            }
            return null;
        }

        private UnityAction<bool> GetAction(ValueActions type)
        {
            if (valueEvents.TryGetValue(type, out var action))
            {
                return action;
            }
            return null;
        }

        public bool GetActionValue(ButtonActions type)
        {
            if (buttonActions.TryGetValue(type, out var input))
            {
                return input.IsPressed();
            }
            return false;
        }

        public float GetActionValue(ValueActions type)
        {
            if (valueActions.TryGetValue(type, out var input))
            {
                return input.ReadValue<float>();
            }

            return 0;
        }

        public void SetAction(ButtonActions type, UnityAction<bool> action, bool update = false)
        {
            if (!buttonEvents.ContainsKey(type))
            {
                buttonEvents.Add(type, action);
            }
            else
            {
                buttonEvents[type] = action;
            }

            if (update && buttonActions.TryGetValue(type, out var input))
            {
                action?.Invoke(input.IsPressed());
            }
        }

        public void ClearAction(ButtonActions type)
        {
            if (buttonEvents.ContainsKey(type))
            {
                buttonEvents.Remove(type);
            }
        }
    }
}
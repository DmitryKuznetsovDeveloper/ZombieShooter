using Game.Data;
using UnityEngine;

namespace Game.Controllers
{
    public sealed class CharacterMovement
    {
        private readonly CharacterController _controller;
        private readonly CharacterMovementConfig _characterMovementConfig;
        private readonly Transform _characterTransform;
        private readonly Transform _characterBody;

        private float _verticalVelocity; // Скорость по вертикали
        private float _rotationY; // Угол поворота по оси Y

        public CharacterMovement(
            CharacterController controller,
            CharacterMovementConfig characterMovementConfig,
            Transform characterTransform,
            Transform bodyTransform)
        {
            _controller = controller;
            _characterMovementConfig = characterMovementConfig;
            _characterTransform = characterTransform;
            _characterBody = bodyTransform;
        }


        public void MoveCharacterOrJump(Vector2 direction, bool isRun, bool isJump)
        {
            // Направление движения
            Vector3 move = _characterBody.transform.right * direction.x + _characterBody.transform.forward * direction.y;

            // Применяем гравитацию
            _verticalVelocity += _characterMovementConfig.gravity * Time.deltaTime;

            // Обновляем вертикальную скорость
            move.y = _verticalVelocity;

            var speed = isRun ? _characterMovementConfig.runSpeed : _characterMovementConfig.moveSpeed;
            
            // Движение персонажа
            _controller.Move(move * (speed * Time.deltaTime));
            
            // Прыжок
            if (_controller.isGrounded && isJump)
                _verticalVelocity = _characterMovementConfig.jumpForce;
        }

        public void LookAround(Vector2 direction)
        {
            if (!direction.Equals(Vector2.zero))
            {
                // Получаем ввод мыши
                float mouseX = direction.x * (_characterMovementConfig.mouseSensitivity * Time.deltaTime);
                float mouseY = direction.y * (_characterMovementConfig.mouseSensitivity * Time.deltaTime);

                // Обновляем вертикальное вращение
                _rotationY -= mouseY;
                _rotationY = Mathf.Clamp(_rotationY, -90f, 90f); // Ограничиваем вертикальный поворот

                // Поворот тела игрока
                var eulerAngles = _characterTransform.eulerAngles;
                float targetYRotation = eulerAngles.y + mouseX;
                float smoothedYRotation = Mathf.LerpAngle(eulerAngles.y, targetYRotation, _characterMovementConfig.rotationSmoothTime);

                eulerAngles = new Vector3(0f, smoothedYRotation, 0f);
                _characterTransform.eulerAngles = eulerAngles;

                // Поворот камеры
                _characterBody.localRotation = Quaternion.Euler(_rotationY, 0f, 0f);
            }
        }
    }
}
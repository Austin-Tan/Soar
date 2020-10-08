using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class Player
    {
        public int id;
        public string username;

        public Vector3 position;
        public Quaternion rotation;

        private static float baseSpeed = 5f;
        private float moveSpeed = baseSpeed / Constants.TICKS_PER_SEC;
        private bool[] inputs;

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;

            inputs = new bool[5];
        }

        // called at a fix rate, not necessrily every time a packet is (like above inputs modifiers)
        public void Update()
        {
            Vector2 _inputDirection = Vector2.Zero;
            if (inputs[1])
            {
                _inputDirection.Y += 1;
            }
            if (inputs[2])
            {
                _inputDirection.Y -= 1;
            }
            if (inputs[3])
            {
                _inputDirection.X -= 1;
            }
            if (inputs[4])
            {
                _inputDirection.X += 1;
            }

            Move(_inputDirection);
        }

        // we are client-authorative on rotation, we let clients tell us
        // exactly what they are rotated.
        private void Move(Vector2 _inputDirection)
        {
            position.X += _inputDirection.X * moveSpeed;
            position.Y += _inputDirection.Y * moveSpeed;


            // Vector3 _forward = Vector3.Transform(new Vector3(0, 0, 1), rotation);
            // Vector3 _right = Vector3.Normalize(Vector3.Cross(_forward, new Vector3(0, 1, 0)));

            // Vector3 _moveDirection = _right * _inputDirection.X + _forward * _inputDirection.Y;
            // position += _moveDirection * moveSpeed;

            ServerSend.PlayerPosition(this);
            // ServerSend.PlayerRotation(this);
        }

        public void SetInput(bool[] _inputs, Quaternion _rotation)
        {
            inputs = _inputs;
            rotation = _rotation;
        }
    }
}

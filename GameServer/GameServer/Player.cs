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
        public Vector2 velocity;
        public Quaternion rotation;

        // It'd be better to handle this outside of player, otherwise each player is tracking position increments.
        // Should increase if collision behavior is funky.
        public const int PositionPacketIncrements = 8;
        private int packetCounter = 0;


        public float MaxSpeed = 15f;
        private static float baseSpeed = 1f;
        private float moveSpeed = baseSpeed / Constants.TICKS_PER_SEC;
        private bool[] inputs;

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            velocity = new Vector2(0, 0);
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

        private const float _drag = 0.98f;
        //private const float _minSpeed = 0.1f;
        private void ApplyDrag()
        {
            velocity.X *= _drag;
            velocity.Y *= _drag;
            //if (velocity.X < _minSpeed)
            //{
            //    velocity.X = 0;
            //}
            //if (velocity.Y < _minSpeed)
            //{
            //    velocity.Y = 0;
            //}
        }

        // we are client-authorative on rotation, we let clients tell us
        // exactly what they are rotated.
        private void Move(Vector2 _inputDirection)
        {
            velocity.X += _inputDirection.X * moveSpeed;
            velocity.Y += _inputDirection.Y * moveSpeed;

            if (velocity.X > MaxSpeed)
            {
                velocity.X = MaxSpeed;
            }
            if (velocity.Y > MaxSpeed)
            {
                velocity.Y = MaxSpeed;
            }

            position.X += velocity.X;
            position.Y += velocity.Y;
            ApplyDrag();
            ServerSend.PlayerVelocity(this);

            //packetCounter++;
            //if (packetCounter == PositionPacketIncrements)
            //{
            ServerSend.PlayerPosition(this);
            //    packetCounter = 0;
            //}
        }

        public void SetInput(bool[] _inputs, Quaternion _rotation)
        {
            inputs = _inputs;
            rotation = _rotation;
        }
    }
}

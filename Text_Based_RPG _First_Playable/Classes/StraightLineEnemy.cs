﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Based_RPG__First_Playable.Classes
{
    internal class StraightLineEnemy : Enemy // Base
    {
        public override string EnemyType => "StraightLine";

        private int direction;
        protected static Random Srandom = new Random(); // should fix enemies moving the same everytime

        public StraightLineEnemy(Map map, int initialHealth, int startX, int startY, int damage) : base(map, initialHealth, startX, startY, damage) // Initialize bouncing enemy
        {
            direction = Srandom.Next(4); // Random direction
        }

        public override void MoveRandomly(Player player, HUD hud) // Over ride base enemy movement
        {
            bool moved = TryMoveInDirection(player, hud, direction);

            if (!moved) // If not moved
            {
                for (int i = 0; i < 4; i++) // Try moving in next direction
                {
                    int newDirection = Srandom.Next(4);
                    if (TryMoveInDirection(player, hud, newDirection))
                    {
                        direction = newDirection; // Update direction
                        break;
                    }
                }
            }
        }

        private bool TryMoveInDirection(Player player, HUD hud, int dir) // Try next movement method
        {
            int x = 0, y = 0;
            switch (dir)
            {
                case 0: y = -1; break; // Up
                case 1: x = 1; break;  // Right
                case 2: y = 1; break;  // Down
                case 3: x = -1; break; // Left
            }

            int newX = Position.x + x;
            int newY = Position.y + y;

            if (map.WithinBounds(newX, newY) && CanMove(newX, newY)) // Check if within map
            {
                if (newX == player.Position.x && newY == player.Position.y)
                {
                    Attack(player, hud);
                    hud.SetActionMessage($"You took {damage} damage from straightline enemy!");
                }
                else
                {
                    Position = (newX, newY); // Move
                    return true;
                }
            }
            return false;
        }
    }
}

using System;
using Microsoft.Xna.Framework;

namespace TowerOffense.Extensions {
    public static class GameTimeExtensions {
        public static float DeltaTime(this GameTime gameTime) {
            return Convert.ToSingle(gameTime.ElapsedGameTime.TotalMilliseconds / 1000d);
        }
    }
}
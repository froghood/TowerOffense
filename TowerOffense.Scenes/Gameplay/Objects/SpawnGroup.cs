using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TowerOffense.Extensions;

namespace TowerOffense.Scenes.Gameplay.Objects {
    public class SpawnGroup {
        public string Enemy { get; init; }
        public double StartTime { get; init; }
        public int Amount { get; init; }
        public double Interval { get; init; }

        private double _time = 0d;
        private int _totalSpawned = 0;
        private Queue<string> _spawnQueue = new();

        public void Update(GameTime gameTime) {
            _time += gameTime.DeltaTime();
            while (_totalSpawned < Amount && _time > StartTime) {
                _totalSpawned++;
                _time -= Interval;
                _spawnQueue.Enqueue(Enemy);
            }
        }

        public IEnumerable<string> DequeueSpawns() {
            while (_spawnQueue.Count > 0) {
                yield return _spawnQueue.Dequeue();
            }
        }
    }
}
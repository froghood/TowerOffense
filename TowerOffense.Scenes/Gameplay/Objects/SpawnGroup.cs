using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TowerOffense.Extensions;

namespace TowerOffense.Scenes.Gameplay.Objects {
    public class SpawnGroup {
        public string Enemy { get; init; }
        public float StartTime { get; init; }
        public int Amount { get; init; }
        public float Interval { get; init; }

        public bool Finished { get => _finished; }

        private float _time;
        private int _totalSpawned = 0;
        private Queue<string> _spawnQueue = new();
        private bool _finished;

        public void Update(GameTime gameTime) {
            _time += gameTime.DeltaTime();
            while (_totalSpawned < Amount && _time > StartTime) {
                _totalSpawned++;
                _time -= Interval;
                _spawnQueue.Enqueue(Enemy);
            }

            _finished = _totalSpawned >= Amount;
        }

        public IEnumerable<string> DequeueSpawns() {
            while (_spawnQueue.Count > 0) {
                yield return _spawnQueue.Dequeue();
            }
        }
    }
}
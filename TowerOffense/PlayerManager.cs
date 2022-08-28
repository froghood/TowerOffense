using System;
using TowerOffense;
using TowerOffense.Scenes.GameOver;

namespace TowerOffense {
    public class PlayerManager {
        public int Health { get; private set; }
        public int Money { get; set; }

        private bool _dead;

        public event EventHandler OnDeath;

        public void Damage(int amount) {
            Health = Math.Max(0, Health - amount);
            System.Console.WriteLine(Health);
            if (Health == 0 && !_dead) {
                OnDeath?.Invoke(this, EventArgs.Empty);
                _dead = true;
            }
        }

        public void Restart(int startingHealth, int startingMoney) {
            Health = startingHealth;
            System.Console.WriteLine(Health);
            Money = startingMoney;
        }
    }
}

// singleton info taken from https://www.c-sharpcorner.com/UploadFile/8911c4/singleton-design-pattern-in-C-Sharp/
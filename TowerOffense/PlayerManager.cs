using System;
using TowerOffense;

namespace TowerOffense {
    public class PlayerManager {
        public int Health { get; private set; }
        public int Money { get; private set; }
        public bool RunFinished { get => _runFinished; }


        private bool _runFinished;

        public event EventHandler OnWin;
        public event EventHandler OnDeath;

        public void Damage(int amount) {
            Health = Math.Max(0, Health - amount);
            System.Console.WriteLine(Health);
            if (Health == 0 && !_runFinished) {
                _runFinished = true;
                OnDeath?.Invoke(this, EventArgs.Empty);

            }
        }

        public void Win() {
            _runFinished = true;
            OnWin?.Invoke(this, EventArgs.Empty);
        }

        public void Restart(int startingHealth, int startingMoney) {
            Health = startingHealth;
            System.Console.WriteLine(Health);
            Money = startingMoney;
            _runFinished = false;
        }

        public void Pay(int amount) {
            Money += amount;
        }

        public void Charge(int amount) {
            Money -= amount;
        }
    }
}

// singleton info taken from https://www.c-sharpcorner.com/UploadFile/8911c4/singleton-design-pattern-in-C-Sharp/
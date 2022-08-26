using TowerOffense;
using TowerOffense.Scenes.GameOver;
namespace TowerOffense{
    public class PlayerManager {
        private int _hp;

        public int Hp { get => _hp; }

        private PlayerManager() {
            _hp = 100; // Put this in a config file somewhere maybe
        }
        private static PlayerManager instance = null;
        public static PlayerManager Instance {
            get {
                if ( instance == null ){
                    instance = new PlayerManager();
                }
                return instance;
            }
        }

        public void SubtractHp(int change){
            _hp -= change;
            if (_hp <= 0){
                Die();
            }
        }
        public void Die(){
            TOGame.Scenes.PushScene<GameOverScene>();
        }
    }
}

// singleton info taken from https://www.c-sharpcorner.com/UploadFile/8911c4/singleton-design-pattern-in-C-Sharp/
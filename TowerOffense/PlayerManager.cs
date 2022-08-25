using TowerOffense;
using TowerOffense.Scenes.GameOver;
namespace PlayerManager{
    public class PlayerManager {
        private int _hp;

        public PlayerManager() {}
        private static PlayerManager instance = null;
        public static PlayerManager Instance {
            get {
                if ( instance == null ){
                    instance = new PlayerManager();
                }
                return instance;
            }
        }

        public void SubtractHP(int change){
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
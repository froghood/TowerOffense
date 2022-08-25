namespace PlayerManager{
    public class PlayerManager {
        private int _hp;
        // private List<Tower> _towers; // uncomment once towers exist

        // Q: How do we give the player manager access to the sceneManager? Constructor could be public and do the get check instead. Or, initialize might be a thing. Hmmmm.
        // Could SceneManager also be a singleton? Are we ever going to need more than one?
        private PlayerManager() {}
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
            // go to gameover
        }
    }
}

// singleton info taken from https://www.c-sharpcorner.com/UploadFile/8911c4/singleton-design-pattern-in-C-Sharp/
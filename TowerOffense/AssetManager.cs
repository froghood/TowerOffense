using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TowerOffense {
    public class AssetManager {

        public Dictionary<string, Texture2D> Textures { get => _textures; }
        public Dictionary<string, SoundEffect> Sounds { get => _sounds; }

        private ContentManager _content;
        private Dictionary<string, Texture2D> _textures;
        private Dictionary<string, SoundEffect> _sounds;

        public AssetManager(ContentManager content) {
            _content = content;
            _textures = new Dictionary<string, Texture2D>();
            _sounds = new Dictionary<string, SoundEffect>();
        }

        public void LoadTexture(string name) {
            _textures.TryAdd(name, _content.Load<Texture2D>(name));
        }

        public void LoadSound(string name) {
            _sounds.TryAdd(name, _content.Load<SoundEffect>(name));
        }
    }
}
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TowerOffense {
    public class AssetManager {

        public Dictionary<string, Texture2D> Textures { get => _textures; }

        private ContentManager _content;
        private Dictionary<string, Texture2D> _textures;

        public AssetManager(ContentManager content) {
            _content = content;
            _textures = new Dictionary<string, Texture2D>();
        }

        public void LoadTexture(string name) {
            _textures.TryAdd(name, _content.Load<Texture2D>(name));
        }
    }
}
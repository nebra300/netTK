using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netTK
{
    public class Texture2D
    {
        private int _id;
        private int _width, _height;
        private string _path;

        public int ID { get { return _id; } }
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public string Path { get { return _path; } }

        public Texture2D(int id, string path, int width, int height)
        {
            this._id = id;
            this._width = width;
            this._height = height;
            this._path = path;
        }
    }
}

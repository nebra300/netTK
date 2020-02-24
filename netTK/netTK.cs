using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Drawing.Imaging;

namespace netTK
{
    public static class netTK
    {
        #region WORLD SIZE VARIABLES
        private static int _maxPXcor = 0;
        public static int MaxPXcor { get { return _maxPXcor; } }
        private static int _maxPYcor = 0;
        public static int MaxPYcor { get { return _maxPYcor; } }
        private static int _minPXcor = 0;
        public static int MinPXcor { get { return _minPXcor; } }
        private static int _minPYcor = 0;
        public static int MinPYcor { get { return _minPYcor; } }
        private static int _gridSizeX = 0;
        public static int GridSizeX { get { return _gridSizeX; } }
        private static int _gridSizeY = 0;
        public static int GridSizeY { get { return _gridSizeY; } }
        private static int _patchWidth = 50;
        public static int PatchWidth { get { return _patchWidth; } }
        private static int _patchHeight = 50;
        public static int PatchHeight { get { return _patchHeight; } }
        private static int _offsetX = 0;
        public static int OffsetX { get { return _offsetX; } }
        private static int _offsetY = 0;
        public static int OffsetY { get { return _offsetY; } }
        #endregion

        public static View ViewCam = new View(); //CAMERA

        #region---------------------------------------GLAVNE netTK METODE------------------------------------------------

        public static void CreateWorld(int gridSizeX, int gridSizeY, int offsetX, int offsetY, int patchWidth, int patchHeight)
        {
            if (gridSizeX % 2 == 0) gridSizeX++;
            if (gridSizeY % 2 == 0) gridSizeY++;

            _gridSizeX = gridSizeX;
            _gridSizeY = gridSizeY;

            if(offsetX<0 || offsetX > GridSizeX-1 || offsetY<0 || offsetY > GridSizeY - 1)
            {
                throw new ArgumentException("invalid offset");
            }
            _offsetX = offsetX;
            _offsetY = offsetY;

            _maxPXcor = GridSizeX-1-OffsetX;
            _maxPYcor = GridSizeX-1-OffsetY;
            _minPXcor = 0-OffsetX;
            _minPYcor = 0-OffsetY;

            _patchWidth = patchWidth;
            _patchHeight = patchHeight;

            Clear();
        } //Creates World
        public static void Clear() //Clears patches and agents (creates new instances of both collections)
        {
            _patches = new Patch[_gridSizeX, _gridSizeY];
            _agents = new List<Agent>();

            for (int i = 0; i < _gridSizeX; i++)
            {
                for (int j = 0; j < _gridSizeY; j++)
                {
                    _patches[i, j] = new Patch(new Point(i - OffsetX, j - OffsetY));
                }
            }
        }
        public static void Clear(Color color)
        {
            _patches = new Patch[_gridSizeX, _gridSizeY];
            _agents = new List<Agent>();

            for (int i = 0; i < _gridSizeX; i++)
            {
                for (int j = 0; j < _gridSizeY; j++)
                {
                    _patches[i, j] = new Patch(new Point(i - OffsetX, j - OffsetY), color);
                }
            }
        }

        #endregion

        #region--------------------------------------METODE ZA CRTANJE---------------------------------------------------

        public static bool ShowAxes = true;
        public static bool ShowGrid = false;
        public static bool ShowPatchBorders = true;

        public static void Render(ref GLControl renderCanvas)
        {
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            ViewCam.ApplyTransform();

            for (int i = 0; i < _gridSizeX; i++)
            {
                for (int j = 0; j < _gridSizeY; j++)
                {
                    _patches[i, j].DrawPatch();
                }
            }

            DrawGrid();
            DrawPatchBorders();
            DrawAxes();
            DrawWorldBorders();

            foreach (Agent agent in _agents)
            {
                agent.DrawAgent();
            }

            renderCanvas.SwapBuffers();
        } //Draws everything (CALLED ON RENDER FORM PAINT EVENT)

        private static void DrawGrid()
        {
            if (ShowGrid == true)
            {
                GL.Color3(Color.Blue);
                for (int x = _minPXcor; x <= _maxPXcor; x++)
                {
                    GL.Begin(PrimitiveType.Lines);
                    GL.Vertex2(x * _patchWidth, (_minPYcor - 0.5) * _patchHeight);
                    GL.Vertex2(x * _patchWidth, (_maxPYcor + 0.5) * _patchHeight);
                    GL.End();
                }
                for (int y = _minPYcor; y <= _maxPYcor; y++)
                {
                    GL.Begin(PrimitiveType.Lines);
                    GL.Vertex2((_minPXcor - 0.5) * _patchWidth, y * _patchHeight);
                    GL.Vertex2((_maxPXcor + 0.5) * _patchWidth, y * _patchHeight);
                    GL.End();
                }
            }
        } 
        private static void DrawAxes()
        {
            if (ShowAxes == true)
            {
                GL.Color3(Color.White);
                GL.Begin(PrimitiveType.Lines);
                GL.Vertex2(0, (_maxPYcor + 0.5) * _patchHeight);
                GL.Vertex2(0, (_minPYcor - 0.5) * _patchHeight);
                GL.End();
                GL.Begin(PrimitiveType.Lines);
                GL.Vertex2((_maxPXcor + 0.5) * _patchWidth,0);
                GL.Vertex2((_minPXcor - 0.5) * _patchWidth,0);
                GL.End();
            }
        }
        private static void DrawPatchBorders()
        {
            if (ShowPatchBorders == true)
            {
                GL.Color3(Color.Red);
                for (int x = _minPXcor; x < _maxPXcor; x++)
                {
                    GL.Begin(PrimitiveType.Lines);
                    GL.Vertex2(x * _patchWidth + _patchWidth / 2, _minPYcor * _patchHeight - _patchHeight / 2);
                    GL.Vertex2(x * _patchWidth + _patchWidth / 2, _maxPYcor * _patchHeight + _patchHeight / 2);
                    GL.End();
                }
                for (int y = _minPYcor; y < _maxPYcor; y++)
                {
                    GL.Begin(PrimitiveType.Lines);
                    GL.Vertex2(_minPXcor * _patchWidth - _patchWidth / 2, y * _patchHeight + _patchHeight / 2);
                    GL.Vertex2(_maxPXcor * _patchWidth + _patchWidth / 2, y * _patchHeight + _patchHeight / 2);
                    GL.End();
                }
            }
        }
        private static void DrawWorldBorders()
        {
            GL.Color3(Color.White);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2((_minPXcor - 0.5) * _patchWidth, (_maxPYcor + 0.5) * _patchHeight);
            GL.Vertex2((_maxPXcor + 0.5) * _patchWidth, (_maxPYcor + 0.5) * _patchHeight);
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2((_minPXcor - 0.5) * _patchWidth, (_minPYcor - 0.5) * _patchHeight);
            GL.Vertex2((_maxPXcor + 0.5) * _patchWidth, (_minPYcor - 0.5) * _patchHeight);
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2((_minPXcor - 0.5) * _patchWidth, (_maxPYcor + 0.5) * _patchHeight);
            GL.Vertex2((_minPXcor - 0.5) * _patchWidth, (_minPYcor - 0.5) * _patchHeight);
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2((_maxPXcor + 0.5) * _patchWidth, (_maxPYcor + 0.5) * _patchHeight);
            GL.Vertex2((_maxPXcor + 0.5) * _patchWidth, (_minPYcor - 0.5) * _patchHeight);
            GL.End();
        }

        #endregion

        #region-------------------------------------------PATCH METODE---------------------------------------------------

        private static Patch[,] _patches = new Patch[1, 1] { { new Patch(new Point(0, 0)) } }; //PATCHES MATRIX

        public static Patch PatchAt(Point position)
        {
            try
            {
                return _patches[position.X + OffsetX, position.Y + OffsetY];
            }
            catch
            {
                return null;
            }
        }
        public static Patch PatchAt(int x, int y)
        {
            return PatchAt(new Point(x, y));
        }

        public static List<Patch> GetPatchesAsList()
        {
            List<Patch> toReturn = new List<Patch>();
            for (int i = 0; i < _gridSizeX; i++)
            {
                for (int j = 0; j < _gridSizeY; j++)
                {
                    toReturn.Add(_patches[i, j]);
                }
            }
            return toReturn;
        }

        public static List<Patch> GetPatchArea(Point p1, Point p2)
        {
            List<Patch> toReturn = new List<Patch>();
            try
            {
                if (p1.X == p2.X)
                {
                    if (p1.Y == p2.Y)
                    {
                        toReturn.Add(PatchAt(p1));
                    }
                    if (p1.Y > p2.Y)
                    {
                        for (int y = p2.Y; y <= p1.Y; y++)
                        {
                            toReturn.Add(PatchAt(p1.X, y));
                        }
                    }
                    if (p1.Y < p2.Y)
                    {
                        for (int y = p1.Y; y <= p2.Y; y++)
                        {
                            toReturn.Add(PatchAt(p1.X, y));
                        }
                    }
                }
                if (p1.X > p2.X)
                {
                    if (p1.Y == p2.Y)
                    {
                        for (int x = p2.X; x <= p1.X; x++)
                        {
                            toReturn.Add(PatchAt(x, p1.Y));
                        }
                    }
                    if (p1.Y > p2.Y)
                    {
                        for (int x = p2.X; x <= p1.X; x++)
                        {
                            for (int y = p2.Y; y <= p1.Y; y++)
                            {
                                toReturn.Add(PatchAt(x, y));
                            }
                        }
                    }
                    if (p1.Y < p2.Y)
                    {
                        for (int x = p2.X; x <= p1.X; x++)
                        {
                            for (int y = p1.Y; y <= p2.Y; y++)
                            {
                                toReturn.Add(PatchAt(x, y));
                            }
                        }
                    }
                }
                if (p1.X < p2.X)
                {
                    if (p1.Y == p2.Y)
                    {
                        for (int x = p1.X; x <= p2.X; x++)
                        {
                            toReturn.Add(PatchAt(x, p1.Y));
                        }
                    }
                    if (p1.Y > p2.Y)
                    {
                        for (int x = p1.X; x <= p2.X; x++)
                        {
                            for (int y = p2.Y; y <= p1.Y; y++)
                            {
                                toReturn.Add(PatchAt(x, y));
                            }
                        }
                    }
                    if (p1.Y < p2.Y)
                    {
                        for (int x = p1.X; x <= p2.X; x++)
                        {
                            for (int y = p1.Y; y <= p2.Y; y++)
                            {
                                toReturn.Add(PatchAt(x, y));
                            }
                        }
                    }
                }
                foreach (var item in toReturn)
                {
                    if (item == null) toReturn.Remove(item);
                }
                return toReturn;
            }
            catch
            {
                foreach (var item in toReturn)
                {
                    if (item == null) toReturn.Remove(item);
                }
                return toReturn;
            }
        }
        public static List<Patch> GetPatchArea(int x1, int y1, int x2, int y2)
        {
            return GetPatchArea(new Point(x1, y1), new Point(x2, y2));
        }

        public static List<Patch> GetUnoccupiedPatchesAsList()
        {
            List<Patch> toReturn = new List<Patch>();
            for (int i = 0; i < _gridSizeX; i++)
            {
                for (int j = 0; j < _gridSizeY; j++)
                {
                    if (_patches[i, j].IsOccupied() == false)
                    {
                        toReturn.Add(_patches[i, j]);
                    }
                }
            }
            return toReturn;
        }
        public static List<Patch> GetOccupiedPatchesAsList()
        {
            List<Patch> toReturn = new List<Patch>();
            for (int i = 0; i < _gridSizeX; i++)
            {
                for (int j = 0; j < _gridSizeY; j++)
                {
                    if (_patches[i, j].IsOccupied() == true)
                    {
                        toReturn.Add(_patches[i, j]);
                    }
                }
            }
            return toReturn;
        }

        public static List<Patch> GetPatchesWithColor(Color color)
        {
            List<Patch> toReturn = new List<Patch>();
            for (int i = 0; i < _gridSizeX; i++)
            {
                for (int j = 0; j < _gridSizeY; j++)
                {
                    if (_patches[i, j].GetColor() == color)
                    {
                        toReturn.Add(_patches[i, j]);
                    }
                }
            }
            return toReturn;
        }

        public static void AddCustomPatchProperty(string key)
        {
            for (int i = 0; i < _gridSizeX; i++)
            {
                for (int j = 0; j < _gridSizeY; j++)
                {
                    _patches[i, j].CustomProperties.Add(key, null);
                }
            }
        }
        public static void RemoveAllCustomPatchProperties()
        {
            for (int i = 0; i < _gridSizeX; i++)
            {
                for (int j = 0; j < _gridSizeY; j++)
                {
                    _patches[i, j].CustomProperties = new Dictionary<string, object>();
                }
            }
        }

        #endregion

        #region-----------------------------------------AGENT METODE-----------------------------------------------------

        private static List<Agent> _agents = new List<Agent>(); //AGENTS LIST

        public static void InsertAgent(Agent agent)
        {
            _agents.Add(agent);
        }
        public static void InsertAgentAt(Point position, Agent agent)
        {
            agent.SetPosition(position);
            InsertAgent(agent);
        }
        public static void InsertAgentAt(int x, int y, Agent agent)
        {
            InsertAgentAt(new Point(x, y), agent);
        }
        public static void InsertAgentAt(Patch p, Agent agent)
        {
            InsertAgentAt(p.Position, agent);
        }

        public static List<Agent> GetAgents()
        {
            return _agents;
        }

        public static List<Agent> GetAgentsAt(Point position)
        {
            List<Agent> toReturn = new List<Agent>();
            foreach (Agent agent in _agents)
            {
                if (agent.Position == position)
                {
                    toReturn.Add(agent);
                }
            }
            return toReturn;
        }
        public static List<Agent> GetAgentsAt(int x, int y)
        {
            return GetAgentsAt(new Point(x, y));
        }
        public static List<Agent> GetAgentsAt(Patch p)
        {
            return GetAgentsAt(p.Position);
        }

        public static List<Agent> GetHiddenAgents()
        {
            List<Agent> toReturn = new List<Agent>();

            foreach (Agent agent in _agents)
            {
                if (agent.Hidden() == true)
                {
                    toReturn.Add(agent);
                }
            }

            return toReturn;
        }
        public static List<Agent> GetVisibleAgents()
        {
            List<Agent> toReturn = new List<Agent>();

            foreach (Agent agent in _agents)
            {
                if (agent.Hidden() == false)
                {
                    toReturn.Add(agent);
                }
            }

            return toReturn;
        }

        public static void RemoveAgent(Agent agent)
        {
            if (_agents.Contains(agent))
            {
                _agents.Remove(agent);
            }
        }

        #endregion

        #region-----------------------------------------CONTENT METODE---------------------------------------------------

        public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>() { { "default", LoadTexture("arrow.png") } }; //TEXTURE DICTIONARY
        public static Texture2D LoadTexture(string path)
        {
            if (!File.Exists("Assets/" + path)) { throw new FileNotFoundException("File not found at 'Assets/" + path + "'"); }

            try
            {
                int id = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, id);

                Bitmap bmp = new Bitmap("Assets/" + path);
                BitmapData data = bmp.LockBits(
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb
                    );
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.BindTexture(TextureTarget.Texture2D, -1);
                return new Texture2D(id, path, bmp.Width, bmp.Height);
            }
            catch
            {
                Console.WriteLine("Error loading texture");
                return null;
            }
        }

        /*PR.
        1.add texture to dictionary

            Textures.Add("key", LoadTexture("tekstura.png"));

        2.add texture to agent from dictionary

            Agent.SetTexture(Textures["key"].ID);

        */

        #endregion

        #region----------------------------------------------MISC-------------------------------------------------------

        public static double Euclid(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }
        public static double Euclid(Patch p1, Patch p2)
        {
            return Euclid(p1.Position, p2.Position);
        }
        public static double Euclid(int x1, int y1, int x2, int y2)
        {
            return Euclid(new Point(x1, y1), new Point(x2, y2));
        }

        public static double Manhatten(Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
        }
        public static double Manhatten(Patch p1, Patch p2)
        {
            return Manhatten(p1.Position, p2.Position);
        }
        public static double Manhatten(int x1, int y1, int x2, int y2)
        {
            return Manhatten(new Point(x1, y1), new Point(x2, y2));
        }

        #endregion
    }
}

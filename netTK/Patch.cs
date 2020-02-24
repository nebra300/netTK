using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace netTK
{
    public class Patch
    {
        private Point _position;
        private Color _color;
        public Dictionary<string, object> CustomProperties;

        public Point Position { get { return _position; } } //in engine position (patch grid)
        public int X { get { return _position.X; } } //in engine position (patch grid X)
        public int Y { get { return _position.Y; } } //in engine position (patch grid Y)

        public int GL_X { get { return X * netTK.PatchWidth; } } //real position (GL position of X)
        public int GL_Y { get { return Y * netTK.PatchHeight; } } //real position (GL position of Y)

        //KONSTRUKTOR
        public Patch(Point position)
        {
            _position = position;
            _color = Color.Black;
            CustomProperties = new Dictionary<string, object>();
        }
        public Patch(Point position, Color color)
        {
            _position = position;
            _color = color;
            CustomProperties = new Dictionary<string, object>();
        }

        //CRTANJE:
        public void DrawPatch()
        {
            GL.Color3(_color);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(GL_X - netTK.PatchWidth / 2, GL_Y - netTK.PatchHeight / 2);
            GL.Vertex2(GL_X - netTK.PatchWidth / 2, GL_Y + netTK.PatchHeight / 2);
            GL.Vertex2(GL_X + netTK.PatchWidth / 2, GL_Y + netTK.PatchHeight / 2);
            GL.Vertex2(GL_X + netTK.PatchWidth / 2, GL_Y - netTK.PatchHeight / 2);
            GL.End();
        }

        //METODE ZA ATRIBUTE:
        public Color GetColor()
        {
            return _color;
        }
        public void SetColor(Color color)
        {
            _color = color;
        }

        //SUSJEDI U netTK WORLDU:
        public List<Patch> Neighbours()
        {
            List<Patch> toReturn = new List<Patch>();
            toReturn.AddRange(Neighbours90());
            toReturn.AddRange(Neighbours45());
            return toReturn;
        }
        public List<Patch> Neighbours90()
        {
            List<Patch> toReturn = new List<Patch>();
            if (netTK.PatchAt(X - 1, Y) != null)
            {
                toReturn.Add(netTK.PatchAt(X - 1, Y));
            }
            if (netTK.PatchAt(X + 1, Y) != null)
            {
                toReturn.Add(netTK.PatchAt(X + 1, Y));
            }
            if (netTK.PatchAt(X, Y - 1) != null)
            {
                toReturn.Add(netTK.PatchAt(X, Y - 1));
            }
            if (netTK.PatchAt(X, Y + 1) != null)
            {
                toReturn.Add(netTK.PatchAt(X, Y + 1));
            }
            return toReturn;
        }
        public List<Patch> Neighbours45()
        {
            List<Patch> toReturn = new List<Patch>();
            if (netTK.PatchAt(X + 1, Y + 1) != null)
            {
                toReturn.Add(netTK.PatchAt(X + 1, Y + 1));
            }
            if (netTK.PatchAt(X + 1, Y - 1) != null)
            {
                toReturn.Add(netTK.PatchAt(X + 1, Y - 1));
            }
            if (netTK.PatchAt(X - 1, Y + 1) != null)
            {
                toReturn.Add(netTK.PatchAt(X - 1, Y + 1));
            }
            if (netTK.PatchAt(X - 1, Y - 1) != null)
            {
                toReturn.Add(netTK.PatchAt(X - 1, Y - 1));
            }
            return toReturn;
        }
        public List<Patch> NeighboursWithColor(Color color)
        {
            List<Patch> toReturn = new List<Patch>();
            foreach (Patch item in Neighbours())
            {
                if (item.GetColor() == color) toReturn.Add(item);
            }
            return toReturn;
        }
        public List<Patch> Neighbours90WithColor(Color color)
        {
            List<Patch> toReturn = new List<Patch>();
            foreach (Patch item in Neighbours90())
            {
                if (item.GetColor() == color) toReturn.Add(item);
            }
            return toReturn;
        }
        public List<Patch> Neighbours45WithColor(Color color)
        {
            List<Patch> toReturn = new List<Patch>();
            foreach (Patch item in Neighbours45())
            {
                if (item.GetColor() == color) toReturn.Add(item);
            }
            return toReturn;
        }

        //AGENT METODE:
        public List<Agent> AgentsHere()
        {
            List<Agent> toReturn = new List<Agent>();
            foreach (Agent agent in netTK.GetAgents())
            {
                if (agent.Position == Position)
                {
                    toReturn.Add(agent);
                }
            }
            return toReturn;
        }
        public void InsertAgentHere(Agent agent)
        {
            agent.SetPosition(Position);
            netTK.InsertAgent(agent);
        }
        public bool IsOccupied()
        {
            if (AgentsHere().Count == 0) return false; else return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace netTK
{
    public class Agent
    {
        private string _id;
        private Point _position;
        private bool _hidden;
        private double _heading;
        private int _textureID;
        private Color _color;
        private int _height;
        private int _width;

        public string ID { get { return _id; } }

        private double Heading
        {
            get
            {
                return _heading;
            }
            set //OGRANICAVA HEADING NA 0-360
            {
                double newValue = value;
                while (newValue < 0)
                {
                    newValue += 360;
                }
                _heading = newValue % 360;
            }
        } // VRACA HEADING U STUPNJEVIMA
        private double HeadingRad
        {
            get
            {
                return (Math.PI / 180) * Heading;
            }
        } //VRACA HEADING U RADIJANIMA

        public Point Position { get { return _position; } } //in engine position (patch grid)
        public int X { get { return _position.X; } } //in engine position (patch grid X)
        public int Y { get { return _position.Y; } } //in engine position (patch grid Y)

        public int GL_X { get { return X * netTK.PatchWidth; } } //real position (GL position of X)
        public int GL_Y { get { return Y * netTK.PatchHeight; } } //real position (GL position of Y)

        //KONSTRUKTORI:
        public Agent()
        {
            _id = Guid.NewGuid().ToString();
            SetPosition(0, 0);
            SetHeading(0);
            _height = netTK.PatchHeight;
            _width = netTK.PatchWidth;
            SetColor(Color.White);
            SetTexture(netTK.Textures["default"].ID);
            Show();
        }
        public Agent(Point position)
        {
            SetPosition(position);
            SetHeading(0);
            _height = netTK.PatchHeight;
            _width = netTK.PatchWidth;
            SetColor(Color.White);
            SetTexture(netTK.Textures["default"].ID);
            Show();
        }

        //CRTANJE:
        public void DrawAgent()
        {
            if (_hidden == false && X >= netTK.MinPXcor && X <= netTK.MaxPXcor && Y >= netTK.MinPYcor && Y <= netTK.MaxPYcor)
            {
                GL.Color3(_color);
                GL.BindTexture(TextureTarget.Texture2D, _textureID);

                GL.PushMatrix();

                GL.Translate(GL_X, GL_Y, 0);
                GL.Rotate(-Heading, 0, 0, 1);
                GL.Translate(-GL_X, -GL_Y, 0);

                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(1, 1);
                GL.Vertex2(GL_X - _width / 2, GL_Y - _height / 2);
                GL.TexCoord2(1, 0);
                GL.Vertex2(GL_X - _width / 2, GL_Y + _height / 2);
                GL.TexCoord2(0, 0);
                GL.Vertex2(GL_X + _width / 2, GL_Y + _height / 2);
                GL.TexCoord2(0, 1);
                GL.Vertex2(GL_X + _width / 2, GL_Y - _height / 2);
                GL.End();

                GL.PopMatrix();

                GL.BindTexture(TextureTarget.Texture2D, -1);
            }
        }

        #region Metode za atribute
        public void SetPosition(Point newPosition)
        {
            _position = new Point(newPosition.X, newPosition.Y);
        }
        public void SetPosition(int x, int y)
        {
            SetPosition(new Point(x, y));
        }
        public void SetPosition(Patch p)
        {
            SetPosition(p.Position);
        }

        public void MoveForward(int n)
        {
            int tempX = X;
            int tempY = Y;
            if (Heading > 315 && Heading <= 360 || Heading >= 0 && Heading <= 45)
            {
                tempY = tempY + n;
            }
            if (Heading > 45 && Heading <= 135)
            {
                tempX = tempX + n;
            }
            if (Heading > 135 && Heading <= 225)
            {
                tempY = tempY - n;
            }
            if (Heading > 225 && Heading <= 315)
            {
                tempX = tempX - n;
            }
            SetPosition(tempX, tempY);
        }
        public void MoveBackwards(int n)
        {
            MoveForward(-n);
        }
        public void MoveLeft(int n)
        {
            Rotate(-90);
            MoveForward(n);
            Rotate(90);
        }
        public void MoveRight(int n)
        {
            Rotate(90);
            MoveForward(n);
            Rotate(-90);
        }

        public void Hide()
        {
            _hidden = true;
        }
        public void Show()
        {
            _hidden = false;
        }
        public bool Hidden()
        {
            return _hidden;
        }

        public double GetHeading()
        {
            return Heading;
        }
        public double GetHeadingRad()
        {
            return HeadingRad;
        }
        public void SetHeading(double heading)
        {
            Heading = heading;
        }
        public void Rotate(double degrees)
        {
            Heading += degrees;
        }
        public void LookAt(Point p)
        {
            Point p1 = this.Position;
            Point p2 = p;

            SetHeading(-Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * 180 / Math.PI + 90);
        }
        public void LookAt(Patch p)
        {
            LookAt(p.Position);
        }

        public Color GetColor()
        {
            return _color;
        }
        public void SetColor(Color color)
        {
            _color = color;
        }

        public void SetTexture(int textureID)
        {
            _textureID = textureID;
        }
        public void RemoveTexture()
        {
            _textureID = -1;
        }
        #endregion

        //PATCH METODE:
        public Patch PatchHere()
        {
            return netTK.PatchAt(Position);
        }
        public Patch PatchAhead(int steps)
        {
            int tempX = X;
            int tempY = Y;
            if(Heading > 337.5 && Heading <= 360 || Heading >= 0 && Heading <= 22.5)
            {
                tempY += steps;
            }
            if(Heading > 22.5 && Heading <= 67.5)
            {
                tempY += steps;
                tempX += steps;
            }
            if(Heading > 67.5 && Heading <= 112.5)
            {
                tempX += steps;
            }
            if(Heading > 112.5 && Heading <= 157.5)
            {
                tempX += steps;
                tempY -= steps;
            }
            if(Heading > 157.5 && Heading <= 202.5)
            {
                tempY -= steps;
            }
            if(Heading > 202.5 && Heading <= 247.5)
            {
                tempY -= steps;
                tempX -= steps;
            }
            if(Heading > 247.5 && Heading <= 292.5)
            {
                tempX -= steps;
            }
            if(Heading > 292.5 && Heading <= 337.5)
            {
                tempX -= steps;
                tempY += steps;
            }

            return netTK.PatchAt(tempX, tempY);
        }
        public Patch PatchRightAndAhead(int degrees, int steps)
        {
            int tempX = X;
            int tempY = Y;
            double tempHeading = Heading + degrees;
            while (tempHeading < 0)
            {
                tempHeading += 360;
            }
            tempHeading = tempHeading % 360;
            if (tempHeading > 337.5 && tempHeading <= 360 || tempHeading >= 0 && tempHeading <= 22.5)
            {
                tempY += steps;
            }
            if (tempHeading > 22.5 && tempHeading <= 67.5)
            {
                tempY += steps;
                tempX += steps;
            }
            if (tempHeading > 67.5 && tempHeading <= 112.5)
            {
                tempX += steps;
            }
            if (tempHeading > 112.5 && tempHeading <= 157.5)
            {
                tempX += steps;
                tempY -= steps;
            }
            if (tempHeading > 157.5 && tempHeading <= 202.5)
            {
                tempY -= steps;
            }
            if (tempHeading > 202.5 && tempHeading <= 247.5)
            {
                tempY -= steps;
                tempX -= steps;
            }
            if (tempHeading > 247.5 && tempHeading <= 292.5)
            {
                tempX -= steps;
            }
            if (tempHeading > 292.5 && tempHeading <= 337.5)
            {
                tempX -= steps;
                tempY += steps;
            }
            return netTK.PatchAt(tempX, tempY);
        }
        public Patch PatchLeftAndAhead(int degrees, int steps)
        {
            return PatchRightAndAhead(-degrees, steps);
        }

        //MISC:
        public void Die()
        {
            if(netTK.GetAgents().Contains(this))
            {
                netTK.RemoveAgent(this);
            }
        }
        public Agent Hatch()
        {
            Agent novi = new Agent();
            novi._position = this._position;
            novi._heading = this._heading;
            novi._color = this._color;
            novi._hidden = this._hidden;
            novi._textureID = this._textureID;
            netTK.InsertAgent(novi);
            return novi;
        }
    }
}

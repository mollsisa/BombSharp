﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombSharp.Classes
{
    public class Bomb : Entity
    {
        public Bomb() : base(null)
        {
            BombSS = Properties.sprites.items;
            this.HitBox = BombHitBox.FromBomb(this);
            this.Width = Block.Width;
            this.Height = Block.Height;
        }

        private Image BombSS = null;

        public float CoordX, CoordY;
        public int Width, Height;
        private bool Deployed;
        private bool Colliding;
        public DateTime DeployTime { get; set; }

        public override void Draw(Graphics g)
        {
            if(Deployed)
                g.DrawImage(BombSS, new RectangleF(CoordX, CoordY, this.Width, this.Height), new Rectangle(0, 0, 14, 14), GraphicsUnit.Pixel);
            HitBox.Draw(g);
        }

        public void Deploy(Player player)
        {
            

            if (!Deployed)
            {
                this.Width = Block.Width - 20;
                this.Height = Block.Height - 20;

                switch (player.PlayerDirection)
                {
                    case FacingDirections.Left:
                        this.CoordX = player.CoordX - this.Width;
                        this.CoordY = player.CoordY;
                        break;
                    case FacingDirections.Right:
                        this.CoordX = player.CoordX + this.Width;
                        this.CoordY = player.CoordY;
                        break;
                    case FacingDirections.Up:
                        this.CoordX = player.CoordX;
                        this.CoordY = player.CoordY - this.Width;
                        break;
                    case FacingDirections.Down:
                        this.CoordX = player.CoordX;
                        this.CoordY = player.CoordY + player.Height;
                        break;
                }
                if(!Colliding)
                    Deployed = true;
                    DeployTime = DateTime.Now;   
            } 
        }

        public void Explode()
        {
                this.CoordX -= 2.5f;
                this.CoordY -= 2.5f;
                this.Width += 5;
                this.Height += 5;
                Deployed = false;          
        }

        public override void OnCollision(CollisionInfo info)
        {
            
        }
    }
}

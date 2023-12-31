using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using _321_Lab05_3;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;

namespace Lanna_s_Defense
{
    internal class MouseController
    {
        public Vector2 mousePos = new Vector2();
        public MouseState mouseState = Mouse.GetState();
        private MouseState oldState;
        public List<Turret> _turretList = new List<Turret>();
        public List<Swordman> _turretList1 = new List<Swordman>();
        public List<Cannon> _turretList2 = new List<Cannon>();
        float turretWidth {get; set;}
        float turretHeight {get; set;}
        int hoveringTurretIndex = 0;
        bool hovering = false;
        bool hasBeenPressed = false;
        public double gt {get; set;}
        public double timeSinceLast = 0;
        string click = "turret";
        
        public MouseController(List<Turret> _turretList, float turretWidth, float turretHeight, double gt)
        {
            this._turretList = _turretList;
            this.turretWidth = turretWidth;
            this.turretHeight = turretHeight;
            this.gt = gt;

        }
        public MouseController(List<Swordman> _turretList1, float turretWidth, float turretHeight, double gt)
        {
            this._turretList1 = _turretList1;
            this.turretWidth = turretWidth;
            this.turretHeight = turretHeight;
            this.gt = gt;

        }
        public MouseController(List<Cannon> _turretList2, float turretWidth, float turretHeight, double gt)
        {
            this._turretList2 = _turretList2;
            this.turretWidth = turretWidth;
            this.turretHeight = turretHeight;
            this.gt = gt;

        }

        public void MouseUpdate(){
            var mousePosition = Mouse.GetState().Position;
            mousePos = new Vector2(mousePosition.X, mousePosition.Y);

            CheckTurretCollision(mousePos);

            
            
            if (gt > timeSinceLast + 1000)
            {
                hasBeenPressed = false;
                timeSinceLast = gt;
            }
            //Console.WriteLine(_turretList[hoveringTurretIndex].rangeUppgrade.hovering);
            MouseState newState = Mouse.GetState(); 
            
            
                if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {

                    if (_turretList.Count > 0)
                    {
                        CheckUpgradeCollision(mousePos, _turretList[hoveringTurretIndex].shootUppgrade);
                        CheckUpgradeCollision(mousePos, _turretList[hoveringTurretIndex].rangeUppgrade);
                    }
                    if (mousePosition.Y < 720)
                    {
                        if (_turretList.Count > 0)
                        {
                            if (_turretList[hoveringTurretIndex].shootUppgrade.hovering == true)
                            {
                                //if(_turretList[hoveringTurretIndex].damage < 2){
                                if (Game1.score >= 400)
                                {
                                    _turretList[hoveringTurretIndex].damage++;
                                    Game1.score -= 400;
                                    Console.WriteLine("+1 damage");
                                }
                                //}
                            }
                            else if (_turretList[hoveringTurretIndex].rangeUppgrade.hovering == true)
                            {
                                if (Game1.score >= 400 && _turretList[hoveringTurretIndex].rangeTextureScale < 1.6f)
                                {
                                    _turretList[hoveringTurretIndex].rangeTextureScale *= 1.25f;

                                    _turretList[hoveringTurretIndex].turretRange *= 1.25f;
                                    Game1.score -= 400;
                                    Console.WriteLine(_turretList[hoveringTurretIndex].rangeTextureScale);
                                }
                            }
                            else if (_turretList[hoveringTurretIndex].mouseIsHovering && _turretList[hoveringTurretIndex].showUpgrades == false)
                            {
                                Console.WriteLine("Selecting Unit");

                                _turretList[hoveringTurretIndex].showUpgrades = true;
                            }
                            else if (_turretList[hoveringTurretIndex].showUpgrades == true)
                            {
                                _turretList[hoveringTurretIndex].showUpgrades = false;
                            }
                            else
                            {
                                foreach (Turret turret in _turretList)
                                {
                                    if (turret.mouseIsHovering)
                                    {
                                        break;
                                    }
                                }
                                Game1.AddTurret(mousePos);
                                _turretList[hoveringTurretIndex].showUpgrades = false;
                            }
                        }
                        else
                        {
                            Game1.AddTurret(mousePos);
                        }
                    }

                }
                if (newState.RightButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    if (!hasBeenPressed)
                    {
                        hasBeenPressed = true;
                        if (_turretList[hoveringTurretIndex].mouseIsHovering == true)
                        {
                            _turretList[hoveringTurretIndex].beenPressed = true;
                            Game1.DeleteTurret(hoveringTurretIndex);
                        }
                    }

                }
                oldState = newState;

            
        }
            
        void CheckUpgradeCollision(Vector2 mousePos, UpgradeCard upgrade){            
            
            if(_turretList.Count > 0){
                if(mousePos.X > upgrade.position.X - upgrade.texture.Width/4 && mousePos.X < upgrade.position.X + upgrade.texture.Width){
                    if(mousePos.Y > upgrade.position.Y - upgrade.texture.Height/2 && mousePos.Y < upgrade.position.Y + upgrade.texture.Height/2){
                        upgrade.hovering = true;
                    }else{
                        upgrade.hovering = false;
                    }
                }else{
                    upgrade.hovering = false;
                }
            }
                
                
            
        }
        void CheckTurretCollision(Vector2 mousePos){
            // Console.WriteLine(hovering);

            for (int i = 0; i < _turretList.Count; i++)
            {
                if(mousePos.X > _turretList[i].position.X - turretWidth/2 && mousePos.X < _turretList[i].position.X + turretWidth/2){
                    if(mousePos.Y > _turretList[i].position.Y - turretHeight/2 && mousePos.Y < _turretList[i].position.Y + turretHeight/2){
                        //hasBeenPressed = false;
                        _turretList[i].mouseIsHovering = true;
                        
                        

                        hoveringTurretIndex = i;
                    }else{
                        _turretList[i].mouseIsHovering = false;
                    }
                    
                }else{
                    _turretList[i].mouseIsHovering = false;
                }
                
            }
           
        }

    }
}
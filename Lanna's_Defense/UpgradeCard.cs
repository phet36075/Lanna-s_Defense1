using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
// using System.Formats.Asn1;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using System.Collections.Generic;
using _321_Lab05_3;


namespace Lanna_s_Defense
{
    internal class UpgradeCard
    {
        public Texture2D texture {get; set;}
        public Vector2 position{get; set;}
        public float rotation{get; set;}
        public Vector2 offset {get; set;}
        public bool hovering = false;
        public UpgradeCard(Texture2D texture, Vector2 position, float rotation, Vector2 offset){
            this.texture = texture;
            this.position = position;
            this.rotation = rotation;
            this.offset = offset;
        }
    }
}
using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class MasterNode : Node
    {
        [InputPropperty]
        public GameObject hair;

        [InputPropperty]
        public GameObject torso;

        [InputPropperty]
        public GameObject leggs;

        [InputPropperty]
        public GameObject feet;

        public override void Init(Vector2 position)
        {
            base.Init(position);
            name = "Charakter Node";
        }

        public override void Draw()
        {
            base.Draw();

            GUI.EndGroup();
        }

        public override void CalculateChange()
        {
            
        }
    }
}
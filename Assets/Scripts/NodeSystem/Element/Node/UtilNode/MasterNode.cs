﻿using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class MasterNode : Node
    {
        [InputPropperty]
        public Color hair;

        [InputPropperty]
        public GameObject torso;

        [InputPropperty]
        public GameObject leggs;

        [InputPropperty]
        public GameObject feet;

        private Character character;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            base.Init(position, eventHandeler);
            name = "Personage Node";

            nodeAreas.Add(new Rect(0, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height, 200, 10));

            rect.size = new Vector2(200, nodeAreas[nodeAreas.Count-1].y + nodeAreas[nodeAreas.Count-1].height);
        }

        public void SetCharakterScript(Character character)
        {
            this.character = character;
        }

        public override void Draw()
        {
            base.Draw();

            GUI.Box(nodeAreas[2], "", styleBottomArea);

            GUI.EndGroup();
        }

        public override void CalculateChange()
        {
            
        }
    }
}
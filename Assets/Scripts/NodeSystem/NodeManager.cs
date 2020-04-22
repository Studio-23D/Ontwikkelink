﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NodeSystem
{
    public class NodeManager : MonoBehaviour
    {
        protected Rect rect;

        protected ElementDrawer elementDrawer;
        protected SystemEventHandeler eventHandeler;
        //protected Menu menu;

        protected GameObject charackterEdit;
        protected MasterNode masterNode;
        protected PatternNode patternNode;

        protected List<Element> elements = new List<Element>();
        protected List<Element> garbage = new List<Element>();

        public void Init()
        {
            rect = new Rect(0, 0, Screen.width - 200, Screen.height);
            eventHandeler = new SystemEventHandeler(rect);
            elementDrawer = new ElementDrawer();

            masterNode = new MasterNode();
            masterNode.Init(new Vector2(rect.width/2, rect.height/2));
            elements.Add(masterNode);

            patternNode = new PatternNode();
            patternNode.Init(new Vector2(rect.width / 2, rect.height / 2));
            elements.Add(patternNode);
            /*menu = new Menu();
            menu.CreateMenuEntry("ColorNode", () =>
            {
                InstantiateNode(new ColorNode());
            });
            eventHandeler.SubscribeTo(EventType.MouseDown, ()=>
            {
                menu.Init(eventHandeler.MousePosition);
            });*/

            //elements.Add(menu);
            SystemEventHandeler.OnElementRemove += RemoveElement;
        }

        public void InstantiateNode(Node node)
        {
            node.Init(eventHandeler.MousePosition);
            elements.Add(node);
        }

        public void RemoveElement(Element element)
        {
            garbage.Add(element);
        }

        private void DestroyGarbage()
        {
            garbage.ForEach(element =>
            {
                if (elements.Contains(element))
                {
                    elements.Remove(element);
                }
            });
            garbage = new List<Element>();
        }

        private void OnGUI()
        {
//            eventHandeler.CheckInput();
            elementDrawer.Draw(elements);
            DestroyGarbage();

            foreach(Node node in elements)
            {
                node.ProcessEvents(Event.current);
            }

            if (GUILayout.Button("Compile"))
            {
                foreach(Node node in elements)
                {
                    node.CalculateChange();
                }
            }

            if (GUILayout.Button("ColorNode"))
            {
                InstantiateNode(new ColorNode());
            }
            if (GUILayout.Button("PaternNode red to blue"))
            {
                patternNode.colorR = Color.blue;
            }
            if (GUILayout.Button("PaternNode red to green"))
            {
                patternNode.colorR = Color.green;
            }
            if (GUILayout.Button("PaternNode red to red"))
            {
                patternNode.colorR = Color.red;
            }

            if (GUILayout.Button("PaternNode"))
            {
                InstantiateNode(new PatternNode());
            }
        }
    }
}
                                                                         
using System.Collections.Generic;
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
        protected List<Element> elements = new List<Element>();
        protected List<Element> garbage = new List<Element>();

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            rect = new Rect(0, 0, Screen.width - 200, Screen.height);
            eventHandeler = new SystemEventHandeler(rect);
            elementDrawer = new ElementDrawer();
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
            eventHandeler.CheckInput();
            elementDrawer.Draw(elements);
            DestroyGarbage();

            foreach(Node node in elements)
            {
                node.ProcessEvents(Event.current);
            }

            if(GUILayout.Button("ColorNode"))
            {
                InstantiateNode(new ColorNode());
            }

            MyGUI.DrawLine(new Vector2(100, 100), new Vector2(300, 300), Color.black, 3);
        }
    }
}
                                                                         
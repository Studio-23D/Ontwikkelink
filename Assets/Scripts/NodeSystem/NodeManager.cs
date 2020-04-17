using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NodeSystem
{
    public class NodeManager : MonoBehaviour
    {
        protected ElementDrawer elementDrawer;
        protected SystemEventHandeler eventHandeler;
        protected Menu menu;
        protected List<Element> elements = new List<Element>();
        protected List<Element> garbage = new List<Element>();

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            eventHandeler = new SystemEventHandeler();
            elementDrawer = new ElementDrawer();
            menu = new Menu();
            menu.CreateMenuEntry("Hello", () =>
            {
                Debug.Log("Hello");
            });
            eventHandeler.SubscribeTo(EventType.MouseDown, ()=>
            {
                menu.Init(eventHandeler.MousPosition);
            });

            elements.Add(menu);
            SystemEventHandeler.OnElementRemove += RemoveElement;
        }

        public void InstantiateNode()
        {

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
        }
    }
}
                                                                         
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NodeSystem
{
    public class NodeManager : MonoBehaviour
    {
        protected SystemEventHandeler eventHandeler;
        protected Menu menu;
        protected List<Element> elements;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            eventHandeler = new SystemEventHandeler();
            eventHandeler.SubscribeTo(EventType.MouseDown, ()=>
            {
                menu = new Menu(Input.mousePosition);
            });
        }

        public void InstantiateNode()
        {

        }

        public void RemoveElement(Element element)
        {
            element.Destroy();
        }

        private void OnGUI()
        {
            eventHandeler.CheckInput();
        }
    }
}
                                                                         
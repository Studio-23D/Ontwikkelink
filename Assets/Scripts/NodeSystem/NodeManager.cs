using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NodeSystem
{
    public class NodeManager : MonoBehaviour
    {
        [SerializeField]
        private CharacterAppearance characterAppearance;

        protected Rect rect;

        protected ElementDrawer elementDrawer;
        protected SystemEventHandeler eventHandeler;
        //protected Menu menu;

        protected PersonageNode masterNode;

        protected List<Element> elements = new List<Element>();
        protected List<Element> garbage = new List<Element>();

		public void Init()
        {
            rect = new Rect(0, 0, Screen.width - 200, Screen.height);
            eventHandeler = new SystemEventHandeler(rect);
            elementDrawer = new ElementDrawer();

            masterNode = new PersonageNode();
            masterNode.Init(new Vector2(rect.width/2, rect.height/2), eventHandeler);
            masterNode.characterAppearance = characterAppearance;
            elements.Add(masterNode);

            SystemEventHandeler.OnElementRemove += RemoveElement;
            SystemEventHandeler.OnElementCreate += (Element element) =>
            {
                if (element is Connection) elements.Add(element);
            };
        }

        public void InstantiateNode(Node node)
        {
            node.Init(eventHandeler.MousePosition, eventHandeler);
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
			if (elementDrawer == null) return;

            elementDrawer.Draw(elements);
            DestroyGarbage();
            eventHandeler.CheckInput();

            foreach(Element element in elements)
            {
                if (element is Node)
                {
                    Node node = element as Node;
                    node.ProcessEvents(Event.current);
                }
            }

            if (GUILayout.Button("ColorNode"))
            {
                InstantiateNode(new ColorNode());
            }

            if (GUILayout.Button("PatternNode"))
            {
                InstantiateNode(new PatternNode());
            }

            if (GUILayout.Button("HairNode"))
            {
                InstantiateNode(new HairNode());
            }

            if (GUILayout.Button("TorsoClothingNode"))
            {
                InstantiateNode(new TorsoClothingNode());
            }

            if (GUILayout.Button("textileNode"))
            {
                InstantiateNode(new TextileNode());
            }
        }
    }
}
                                                                         
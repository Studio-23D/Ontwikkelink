using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace NodeSystem
{
    public class NodeManager : MonoBehaviour
    {
		public List<Element> GetElements => elements;

        [SerializeField] private CharacterAppearance characterAppearance;
        [SerializeField] private Image nodeFieldBackground;
        [SerializeField] private bool drawGUI;

		protected Rect rect;

        protected ElementDrawer elementDrawer;
        protected SystemEventHandeler eventHandeler;
        //protected Menu menu;

        protected CharacterNode characterNode;

        protected List<Element> elements = new List<Element>();
        protected List<Element> garbage = new List<Element>();

		public void Init()
        {
            rect = new Rect(0, 0, 1100, Screen.height);//GetComponent<RectTransform>().rect;//new Rect(0, 0, Screen.width - 200, Screen.height);
            eventHandeler = new SystemEventHandeler(rect);
            elementDrawer = new ElementDrawer();

            elementDrawer.Rect = rect;

            characterNode = new CharacterNode();
            characterNode.Init(new Vector2(rect.width/2, rect.height/2), eventHandeler);
            characterNode.characterAppearance = characterAppearance;
            elements.Add(characterNode);

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
            elements = elements.OrderBy(e => e.drawOrder).ToList();
        }

		public void InstantiateNode(string nodeName)
		{
			Node node;

			switch (nodeName)
			{
				case "Color":
					node = new ColorNode();
					break;

				case "Pattern":
					node = new PatternNode();
					break;

				case "Textile":
					node = new TextileNode();
					break;

				case "Hair":
					node = new HairNode();
					break;

				case "Torso":
					node = new TorsoClothingNode();
					break;

				case "Legs":
					node = new LegsClothingNode();
					break;

				case "Feet":
					node = new FeetClothingNode();
					break;

				default:
					Debug.LogError(nodeName + " IS NOT A VALID NODE NAME");
					return;
			}

			node.Init(nodeFieldBackground.transform.position, eventHandeler);
            //node.Init(eventHandeler.MousePosition, eventHandeler);
			elements.Add(node);
			elements = elements.OrderBy(e => e.drawOrder).ToList();
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

			if (drawGUI)
			{
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

				if (GUILayout.Button("LegsClothingNode"))
				{
					InstantiateNode(new LegsClothingNode());
				}

				if (GUILayout.Button("FeetClothingNode"))
				{
					InstantiateNode(new FeetClothingNode());
				}

				if (GUILayout.Button("textileNode"))
				{
					InstantiateNode(new TextileNode());
				}
			}
        }
    }
}
                                                                         
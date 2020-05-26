using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NodeSystem
{
	public class NodeManager : MonoBehaviour
	{
		public List<Element> GetElements => elements;

		[SerializeField] private CharacterAppearance characterAppearance;
		[SerializeField] private RectTransform trashCan;
		[SerializeField] private RectTransform characterStage;
		[SerializeField] private RectTransform nodeStage;
		[SerializeField] private RectTransform nodeButtonBar;
		[SerializeField] private bool drawGUI  = false;

		private bool canDraw = false;

		protected Rect rect;

        protected ElementDrawer elementDrawer;
        protected SystemEventHandeler eventHandeler;
        protected CharacterNode characterNode;

        protected List<Element> elements = new List<Element>();
        protected List<Element> garbage = new List<Element>();

		public void Init()
        {
            rect = new Rect();
            rect.width = Screen.width - (characterStage.rect.width / 1920 * Screen.width);
            rect.height = Screen.height;

            eventHandeler = new SystemEventHandeler(rect);
            elementDrawer = new ElementDrawer();

            characterNode = new CharacterNode();
            characterNode.Init(new Vector2(rect.width/2, rect.height/2), eventHandeler);
            characterAppearance.Character = FindObjectOfType<Character>();
            characterNode.characterAppearance = characterAppearance;
            elements.Add(characterNode);

            canDraw = true;

			SystemEventHandeler.OnElementRemove += AddToGarbage;
			SystemEventHandeler.OnElementCreate += (Element element) =>
            {
                if (element is Connection) elements.Add(element);
            };
        }

        public void InstantiateNode(Node node)
        {
            node.Init(new Vector2(nodeStage.rect.width / 4, nodeStage.rect.height / 4), eventHandeler);
            elements.Add(node);
            elements = elements.OrderBy(e => e.drawOrder).ToList();
        }

		public void InstantiateNode(string nodeName)
		{
			switch (nodeName)
			{
				case "Color":
					InstantiateNode(new ColorNode());
					break;

				case "Pattern":
					InstantiateNode(new PatternNode());
					break;

				case "Textile":
					InstantiateNode(new TextileNode());
					break;

				case "Hair":
					InstantiateNode(new HairNode());
					break;

				case "Torso":
					InstantiateNode(new TorsoClothingNode());
					break;

				case "Legs":
					InstantiateNode(new LegsClothingNode());
					break;

				case "Feet":
					InstantiateNode(new FeetClothingNode());
					break;
			}
		}

		public void AddToGarbage(Element element)
        {
            garbage.Add(element);
        }

        public void ToggleDraw(bool canDraw)
        {
            this.canDraw = canDraw;
        }

		public void CheckForGarbage()
		{
			foreach (Element element in elements)
			{
				if (element is Node)
				{
					Node node = element as Node;

					if (!trashCan.rect.Overlaps(node.Rect) || node.GetType() == typeof(CharacterNode)) continue;

					AddToGarbage(node);
					DestroyGarbage();
				}
			}
		}

		private void OpenGarbage()
		{
			trashCan.gameObject.SetActive(true);
		}

		private void CloseGarbage()
		{
			trashCan.gameObject.SetActive(false);
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
			CloseGarbage();
		}

		private bool IsNodeDragged()
		{
			foreach (Element element in elements)
			{
				if (element is Node)
				{
					Node node = element as Node;

					if (node.isDragged)
					{
						return true;
					}
				}
			}

			return false;
		}

		private void OnGUI()
        {
			if (elementDrawer == null || !canDraw) return;

            rect.width = Screen.width - characterStage.rect.width / 1920 * Screen.width;
            rect.height = Screen.height - nodeButtonBar.rect.height / nodeStage.rect.height * Screen.height;

			elementDrawer.Draw(elements, rect);
            eventHandeler.CheckInput();

			foreach (Element element in elements)
            {
                if (element is Node)
                {
                    Node node = element as Node;
                    node.ProcessEvents(Event.current);

					if (node.isDragged && node.GetType() != typeof(CharacterNode))
					{
						OpenGarbage();
						continue;
					}
                }
            }

			if (!IsNodeDragged())
			{
				CheckForGarbage();
				CloseGarbage();
			}

			if (drawGUI) {
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
                                                                         
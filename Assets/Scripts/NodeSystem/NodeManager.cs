﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NodeSystem
{
	public class NodeManager : MonoBehaviour
	{
		public bool DraggingAllNodes
		{
			set
			{
				draggingAllNodes = value;
			}
		}
		public bool AreNodesDragged
		{
			get
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
		}
		public List<Element> GetElements => elements;

		[SerializeField] private CharacterAppearance characterAppearance;
		[SerializeField] private RectTransform trashCan;
		[SerializeField] private RectTransform characterStage;
		[SerializeField] private RectTransform nodeStage;
		[SerializeField] private RectTransform nodeButtonBar;
		[SerializeField] private bool drawGUI  = false;

		private bool canDraw = false;
		private bool draggingAllNodes = false;

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
			for (int i = 0; i < elements.Count; i++)
			{

				if (elements[i] is Node)
				{
					Node node = elements[i] as Node;

					if (!trashCan.rect.Overlaps(node.Rect) || node.GetType() == typeof(CharacterNode) || !trashCan.gameObject.activeSelf) continue;

					AddToGarbage(node);
					DestroyGarbage();
				}
			}
		}

		public bool IsNodeDragged(Node node)
		{
			foreach (Element element in elements)
			{
				if (element is Node)
				{
					Node nodeToCheck = element as Node;

					if (node.GetType() == nodeToCheck.GetType() && node.isDragged)
					{
						return true;
					}
				}
			}

			return false;
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
			for (int i = 0; i < garbage.Count; i++)
			{
				if (elements.Contains(garbage[i]))
				{
					if (garbage[i] is Node)
					{
						Node node = garbage[i] as Node;

						node.Destroy();
					}
					elements.Remove(garbage[i]);
				}
			}
            garbage = new List<Element>();
			CloseGarbage();
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

					if (node.isDragged && !IsNodeDragged(characterNode) && !draggingAllNodes)
					{
						OpenGarbage();
						continue;
					}
                }
            }

			if (!AreNodesDragged)
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
                                                                         
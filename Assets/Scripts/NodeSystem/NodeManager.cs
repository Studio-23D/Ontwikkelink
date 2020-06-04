using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NodeSystem
{
	public class NodeManager : MonoBehaviour
	{
		public List<Element> Elements => elements;

		[SerializeField] private CharacterAppearance characterAppearance;
		[SerializeField] private NodeField nodeField;
		[SerializeField] private GarbageBin garbageBin;
		[SerializeField] private RectTransform characterStage;
		[SerializeField] private RectTransform nodeStage;
		[SerializeField] private RectTransform nodeButtonBar;
		[SerializeField] private bool drawGUI  = false;

		private bool canDraw = false;

		public Rect rect;

        protected ElementDrawer elementDrawer;
        protected SystemEventHandeler eventHandeler;
        protected CharacterNode characterNode;

        protected List<Element> elements = new List<Element>();
        protected List<Element> garbage = new List<Element>();

		public SystemEventHandeler EventHandeler => eventHandeler;

		public void Init()
        {
			rect = new Rect
			{
				width = Screen.width - (characterStage.rect.width / 1920 * Screen.width),
				height = Screen.height
			};

			eventHandeler = new SystemEventHandeler();
            elementDrawer = new ElementDrawer();

			characterNode = InstantiateNode(new CharacterNode());
            characterAppearance.Character = characterAppearance.Character ?? FindObjectOfType<Character>();
            characterNode.characterAppearance = characterAppearance;

			eventHandeler.OnElementHold += garbageBin.OnElementDrag;
			eventHandeler.OnElementRelease += garbageBin.OnElementRelease;

			canDraw = true;

			eventHandeler.OnElementRemove += AddToGarbage;
			eventHandeler.OnElementCreate += (Element element) =>
            {
                if (element is Connection) elements.Add(element);
            };
        }

        public T InstantiateNode<T>(T node) where T : Node
        {
            node.Init(new Vector2(nodeStage.rect.width / 4, nodeStage.rect.height / 4), eventHandeler);
            elements.Add(node);
            elements = elements.OrderBy(e => e.DrawOrder).ToList();
			nodeField.OnSave += node.SaveStartPosition;
			nodeField.OnReset += node.ResetPosition;

			nodeField.OnDrag += node.EnableFieldDrag;
			nodeField.OnRelease += node.DisableFieldDrag;

			return node;
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

/*				case "Textile":
					InstantiateNode(new TextileNode());
					break;*/

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

		private void DestroyGarbage()
        {
			for (int i = 0; i < garbage.Count; i++)
			{
				Element elementToRemove = garbage[i];

				if (elementToRemove is Node) {
					Node elementAsNode = elementToRemove as Node;
					nodeField.OnReset -= elementAsNode.ResetPosition;
					nodeField.OnSave -= elementAsNode.SaveStartPosition;
					elementAsNode = null;
				}
				elements.Remove(elementToRemove);

				elementToRemove = null;
			}
		}

		private void OnGUI()
        {
			if (elementDrawer == null || !canDraw) return;

            rect.width = Screen.width - characterStage.rect.width / 1920 * Screen.width;
            rect.height = Screen.height - nodeButtonBar.rect.height / nodeStage.rect.height * Screen.height;

			if (Event.current.type == EventType.MouseDrag)
			{
				nodeField.Drag();
				GUI.Box(new Rect(100, 100, 100, 100), "");
			}
			else if (nodeField.isDragging && Event.current.type == EventType.MouseUp)
			{
				nodeField.Release();
			}

			elementDrawer.Draw(elements, rect);
			eventHandeler.OnGui?.Invoke(Event.current);

			if (garbage.Count > 0)
				DestroyGarbage();
        }
    }
}
                                                                         
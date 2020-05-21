using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NodeSystem
{
    public class NodeManager : MonoBehaviour
    {
		public List<Element> GetElements => elements;

        [SerializeField]
        private CharacterAppearance characterAppearance;
        [SerializeField]
        private RectTransform characterStage;
        [SerializeField]
        private RectTransform buttonMenu;

        protected Rect rect;

        protected ElementDrawer elementDrawer;
        protected SystemEventHandeler eventHandeler;
        
        private bool canDraw = false;

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

        public void RemoveElement(Element element)
        {
            garbage.Add(element);
        }

        public void ToggleDraw(bool canDraw)
        {
            this.canDraw = canDraw;
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
			if (elementDrawer == null || !canDraw) return;

            rect.width = Screen.width - (characterStage.rect.width / 1920 * Screen.width);
            rect.height = Screen.height - (buttonMenu.rect.height / 1080 * Screen.height);

            elementDrawer.Draw(elements, rect);
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
                                                                         
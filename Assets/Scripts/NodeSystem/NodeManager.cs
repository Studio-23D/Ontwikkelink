using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NodeSystem
{
    public class NodeManager : MonoBehaviour
    {
        private Rect rect;

        [SerializeField]
        private List<GameObject> elements = new List<GameObject>();
        [SerializeField]
        private Transform nodeField;

        private SystemEventHandeler eventHandeler;

        private void Start() { Init(); }

        public void Init()
        {
            rect = GetComponent<RectTransform>().rect;

            eventHandeler = new SystemEventHandeler(rect);
 
            GameObject personageNode = Instantiate(elements[0], nodeField);
            PersonageNode node = personageNode.GetComponent<PersonageNode>();
            node.Init(new Vector2(rect.width/2, rect.height/2), eventHandeler);
        }
            /*public List<Element> GetElements => elements;

            [SerializeField]
            private CharacterAppearance characterAppearance;

            protected Rect rect;

            protected ElementDrawer elementDrawer;
            protected SystemEventHandeler eventHandeler;

            protected PersonageNode masterNode;

            protected List<Element> elements = new List<Element>();
            protected List<Element> garbage = new List<Element>();

            public void Init()
            {
                eventHandeler = new SystemEventHandeler(rect);

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
            }*/
        }
}
                                                                         
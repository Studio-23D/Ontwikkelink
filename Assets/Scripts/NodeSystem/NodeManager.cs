using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NodeSystem
{
    public class NodeManager : MonoBehaviour
    {
        protected List<Element> elements;
        public void Init()
        {

        }

        public void InstantiateNode()
        {

        }

        public void RemoveElement(Element element)
        {
            element.Destroy();
        }
    }
}
                                                                         
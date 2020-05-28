using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeFactory : MonoBehaviour
{
    private Dictionary<NodeType, NodeContent> nodeContent = new Dictionary<NodeType, NodeContent>();
    private List<Node> nodes = new List<Node>();

    public void CreateNode(Node node)
    {
        Node nodeGameobject = Instantiate(node);
        transform.parent = nodeGameobject.transform;
        if (nodeContent.ContainsKey(nodeGameobject.Type))
            nodeGameobject.Content = nodeContent[nodeGameobject.Type];
        nodes.Add(nodeGameobject);
    }
}

using SimpleJSON;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grapht.Config {
    /// <summary>
    /// Represents data for a node on the graph
    /// </summary>
    public class Node {
        /// <summary>
        /// Either a Moveable or Immoveable node
        /// </summary>
        public enum Type {IMMOVEABLE, MOVEABLE};

        /// <summary>
        /// The position in worldspace for the node
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// The numeric value to display
        /// </summary>
        public int Value;

        /// <summary>
        /// The type of node
        /// </summary>
        public Type NodeType;

        /// <summary>
        /// The children this node has
        /// </summary>
        public IList<Node> Children;

        /// <summary>
        /// Create a Node instance from a JSONNode
        /// </summary>
        /// <param name="nodeObj">The json to parse</param>
        /// <returns>A Node object represented by the provided json</returns>
        public static Node Parse(JSONNode nodeObj) {
            Node node = new Node();
            if (nodeObj["pos"] != null) {
                node.Position = new Vector2(nodeObj["pos"]["x"].AsFloat, nodeObj["pos"]["y"].AsFloat);
            }
            node.Value = nodeObj["value"].AsInt;
            node.NodeType = (Type) nodeObj["type"].AsInt;
            node.Children = nodeObj["children"].AsArray.Childs.Select(child => Parse(child)).ToList();
            return node;
        }
    }
}

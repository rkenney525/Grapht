using Grapht.Entity;
using Grapht.Exception;
using SimpleJSON;
using System.Collections.Generic;
using System.Linq;

namespace Grapht.Config {
    /// <summary>
    /// Data relating to an individual level
    /// </summary>
    public class Level {
        /// <summary>
        /// The level identifier
        /// </summary>
        public int Id;

        /// <summary>
        /// All individually placed nodes
        /// </summary>
        public IList<Node> Nodes;

        /// <summary>
        /// Victory conditions for the root of the graph
        /// </summary>
        public IList<VictoryCondition> Rules;

        /// <summary>
        /// Create a Level instance from a JSONNode
        /// </summary>
        /// <param name="levelNode">The json to parse</param>
        /// <returns>A Level object represented by the provided json</returns>
        public static Level Parse(JSONNode levelNode) {
            Level obj = new Level();
            obj.Id = levelNode["id"].AsInt;
            obj.Nodes = levelNode["nodes"].AsArray.Childs.Select(node => Node.Parse(node)).ToList();
            obj.Rules = levelNode["rules"].AsArray.Childs.Select(rule => VictoryConditions.ParseVictoryCondition(rule)).ToList();
            return obj;
        }
    }
}

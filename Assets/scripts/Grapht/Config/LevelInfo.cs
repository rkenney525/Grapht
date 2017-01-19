using Grapht.Component.Victory;
using Grapht.Component.Hint;
using SimpleJSON;
using System.Collections.Generic;
using System.Linq;

namespace Grapht.Config {
    /// <summary>
    /// Data relating to an individual level
    /// </summary>
    public class LevelInfo {
        /// <summary>
        /// The level identifier
        /// </summary>
        public int Id;

        /// <summary>
        /// All individually placed nodes
        /// </summary>
        public IList<NodeInfo> Nodes;

        /// <summary>
        /// Victory conditions for the root of the graph
        /// </summary>
        public IList<VictoryCondition> Rules;

        /// <summary>
        /// Any hints to the solution
        /// </summary>
        public IList<string> Hints;

        /// <summary>
        /// Create a Level instance from a JSONNode
        /// </summary>
        /// <param name="levelNode">The json to parse</param>
        /// <returns>A Level object represented by the provided json</returns>
        public static LevelInfo Parse(JSONNode levelNode) {
            LevelInfo obj = new LevelInfo();
            obj.Id = levelNode["id"].AsInt;
            obj.Nodes = levelNode["nodes"].AsArray.Childs.Select(node => NodeInfo.Parse(node)).ToList();
            obj.Rules = levelNode["rules"].AsArray.Childs.Select(rule => VictoryConditions.ParseVictoryCondition(rule)).ToList();
            obj.Hints = levelNode["hints"].AsArray.Childs.Select(hint => HintFactory.ParseHint(hint)).ToList();
            return obj;
        }
    }
}

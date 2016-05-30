using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Grapht.Entity {
    /// <summary>
    /// A rule for victory, and all associated data
    /// </summary>
    public class VictoryCondition {
        /// <summary>
        /// Logic for determining if a Rule is met or not
        /// </summary>
        /// <param name="nodes">All filtered nodes</param>
        /// <returns>True if the condition is met, false otherwise</returns>
        public delegate bool Rule(IList<TreeNodeScript> nodes);

        /// <summary>
        /// Filters a list of all nodes to a desired group
        /// </summary>
        /// <param name="nodes">All nodes in the game</param>
        /// <returns>A list of desired nodes</returns>
        public delegate IList<TreeNodeScript> Filter(IList<TreeNodeScript> nodes);

        /// <summary>
        /// The Rule for this victory condition
        /// </summary>
        private Rule rule;

        /// <summary>
        /// The filter for this victory condition
        /// </summary>
        private Filter filter;

        /// <summary>
        /// The display text for this rule, to be displayed in the game overlay
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Create a Victory condition with the specified properties
        /// </summary>
        /// <param name="Title">The title text</param>
        /// <param name="rule">The rule logic</param>
        /// <param name="filter">The filter logic</param>
        public VictoryCondition(string Title, Rule rule, Filter filter) {
            this.Title = Title;
            this.rule = rule;
            this.filter = filter;
        }

        /// <summary>
        /// Apply this victory condition's rule to the filtered set of nodes
        /// </summary>
        /// <param name="nodes">All nodes in the game</param>
        /// <returns>True if the rule is met, false otherwise</returns>
        public bool Apply(IList<TreeNodeScript> nodes) {
            return rule(filter(nodes));
        }
    }
}

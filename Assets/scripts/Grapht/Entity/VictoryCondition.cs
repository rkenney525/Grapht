using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Grapht.Entity {
    public class VictoryCondition {
        public delegate bool Rule(IList<TreeNodeScript> nodes);
        public delegate IList<TreeNodeScript> Filter(IList<TreeNodeScript> nodes);
        private Rule rule;
        private Filter filter;
        public VictoryCondition(Rule rule, Filter filter) {
            this.rule = rule;
            this.filter = filter;
        }
        public bool Apply(IList<TreeNodeScript> nodes) {
            return rule(filter(nodes));
        }
    }
}

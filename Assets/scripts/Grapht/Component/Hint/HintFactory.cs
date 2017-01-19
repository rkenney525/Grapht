using Grapht.Exception;
using SimpleJSON;

namespace Grapht.Component.Hint {
    /// <summary>
    /// Houses all logic for generating the hint content from json.
    /// </summary>
    class HintFactory {
        private static string BranchSize(int size) {
            return string.Format("Try a branch sum of {0}", size);
        }

        /// <summary>
        /// Parses a given json object into a solution hint for display to the player
        /// </summary>
        /// <param name="json">The json node to parse</param>
        /// <returns>A solution hint</returns>
        public static string ParseHint(JSONNode json) {
            switch (json["name"]) {
                case "BranchSize":
                    return BranchSize(json["arg"].AsInt);
                default:
                    throw new GraphtParsingException();
            }
        }
    }
}

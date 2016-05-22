using SimpleJSON;

namespace Grapht.Config {
    public class Level {
        public int Id;
        public static Level Parse(JSONNode levelNode) {
            Level obj = new Level();
            obj.Id = levelNode["id"].AsInt;
            return obj;
        }
    }
}
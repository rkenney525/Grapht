using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Grapht.Arch;

namespace Grapht.Component {

    /// <summary>
    /// Script responsible for drawing grid squares on the screen
    /// </summary>
    public class GridOverlayScript : UnityComponent {

        /// <summary>
        /// The size of each line
        /// </summary>
        private const float LINE_SIZE = 0.03f;

        /// <summary>
        /// The space between each line
        /// </summary>
        private const float LINE_SPACING = 2.0f;

        /// <summary>
        /// The lines drawn on the screen
        /// </summary>
        private IList<LineRenderer> lines = new List<LineRenderer>();

        /// <summary>
        /// Reference to the Line prefab
        /// </summary>
        public GameObject LineRef;

        /// <summary>
        /// Draw a set of horizontal and vertical lines
        /// </summary>
        public void DrawGrid() {
            // TODO figure out why SELECT doesnt work here
            HorizontalCoordinates().Concat(VerticalCoordinates())
            .All(lineCoord => {
                lines.Add(BuildLine(lineCoord));
                return true;
            });
        }

        /// <summary>
        /// Configure and attach a line to this GameObject
        /// </summary>
        /// <param name="coords">A Tuple containing the start and end points of the line</param>
        /// <returns>The LineRenderer added to the GameObject</returns>
        private LineRenderer BuildLine(Tuple<Vector2, Vector2> coords) {
            LineRenderer line = Instantiate(LineRef).GetComponent<LineRenderer>();
            line.transform.parent = this.transform;
            line.SetWidth(LINE_SIZE, LINE_SIZE);
            line.SetPosition(0, coords._1);
            line.SetPosition(1, coords._2);
            return line;
        }

        /// <summary>
        /// Get a list of coordinates for all horizontal lines to be displayed
        /// </summary>
        /// <returns>A list of horizontal line coordinates</returns>
        private IList<Tuple<Vector2, Vector2>> HorizontalCoordinates() {
            IList<Tuple<Vector2, Vector2>> coords = new List<Tuple<Vector2, Vector2>>();
            ViewProperties.WORLD_LIMIT_BOTTOM.To(ViewProperties.WORLD_LIMIT_TOP).Step(LINE_SPACING).All(y => {
                coords.Add(Tuple.New(
                    new Vector2(ViewProperties.WORLD_LIMIT_LEFT, y),
                    new Vector2(ViewProperties.WORLD_LIMIT_RIGHT, y)));
                return true;
            });
            return coords;
        }

        /// <summary>
        /// Get a list of coordinates for all vertical lines to be displayed
        /// </summary>
        /// <returns>A list of vertical line coordinates</returns>
        private IList<Tuple<Vector2, Vector2>> VerticalCoordinates() {
            IList<Tuple<Vector2, Vector2>> coords = new List<Tuple<Vector2, Vector2>>();
            ViewProperties.WORLD_LIMIT_LEFT.To(ViewProperties.WORLD_LIMIT_RIGHT).Step(LINE_SPACING).All(x => {
                coords.Add(Tuple.New(
                    new Vector2(x, ViewProperties.WORLD_LIMIT_BOTTOM),
                    new Vector2(x, ViewProperties.WORLD_LIMIT_TOP)));
                return true;
            });
            return coords;
        }

        /// <summary>
        /// Remove all managed lines from the screen
        /// </summary>
        public void EraseGrid() {
            lines.All(line => {
                GameObject.Destroy(line);
                return true;
            });
            lines.Clear();
        }
    }
}

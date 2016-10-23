using UnityEditor;

class Pipeline {
     public static void PerformBuild () {
         string[] scenes = { "Assets/scenes/Splash.unity", "Assets/scenes/MainGameScene.unity" };
         BuildPipeline.BuildPlayer(scenes, "WebBuild", BuildTarget.WebGL, BuildOptions.None);
     }
}
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using Sirenix.OdinInspector.Editor.Examples;
using UnityEditor.PackageManager.UI;

public class PackageImportWindow  : OdinEditorWindow
{
    public const string PackagePath = "Assets/Editor";


    [TitleGroup("Multiple Stacked Boxes")]
    [HorizontalGroup("Multiple Stacked Boxes/Split",525)]
    [VerticalGroup("Multiple Stacked Boxes/Split/Left")]
    [BoxGroup("Multiple Stacked Boxes/Split/Left/框架")]
    public PackageImpData ZMUI=new PackageImpData("UI框架 ZMUIFrameWork", "   ZMUI是一款高性能、自动化的UI框架。从底层解决性能问题" +
        "代码一键生成、组件一键绑定、\n事件自动监听、彻底解放双手，提升50%开发效率.", "ZMUI");

    [VerticalGroup("Multiple Stacked Boxes/Split/Right")]
    [BoxGroup("Multiple Stacked Boxes/Split/Right/框架")]
    public PackageImpData ZMAsset=new PackageImpData("资源热更框架 ZMAsset", "    ZMAsset是一款多模块、多线程、资源热更管理框架 无需编写代码，" +
        "即可轻松实现资源热更\n及资源加载,包含：可视化多模块打包器、多模块热更、多线程下载、多版本热更、多版本回退\n" +
        "加密、解密、内嵌、解压、内存引用计数、大型对象池、AssetBundle加载、Editor加载.", "ZMAsset");

    [BoxGroup("Multiple Stacked Boxes/Split/Left/ 框架")]
    public PackageImpData DMVC= new PackageImpData("游戏框架 DMVC", "   DMVC是一款基于MVC思想设计的智能化、多世界、多模块的解耦游戏框架 \n " +
        "大道至简，数据层、逻辑层、网络层，可配合ZMUI使用", "DMVC");

    [BoxGroup("Multiple Stacked Boxes/Split/Right/插件")]
    public PackageImpData Debuger = new PackageImpData("日志插件 DebugerTookit", "   该插件是一款超级完善的日志系统，包含子线程本地日志文件写入、自定义颜色日志" +
        "移动设备\n日志查看工具、FPS实时显示、DLL日志编译时剔除、ProtoBuff转Json字符串打印日志、日志\n重定向.", "Debuger");

    [BoxGroup("Multiple Stacked Boxes/Split/Left/插件")]
    public PackageImpData ScroView = new PackageImpData("ZMUGUIPro ", " 该插件是一款UGUI拓展插件、包含：超高性能描边、高性能裁剪、文本多语言、图片多语言、" +
        "\n按钮长按事件、按钮双击事件、点击缩放、点击音效等", "ZMUGUIPro");

    [MenuItem("Tools/PackageImportWindow    ")]
    public static void ShowWindow()
    {
        GetWindow<PackageImportWindow>();
    }

    [System.Serializable]
    [HideLabel]
    public class PackageImpData
    {
        [HideInInspector]
        private Texture2D RegularPreviewField;

        [HideLabel]
        private string logoContent = "ZMUI";

        [HideLabel]
        private string titleContent = "";

        [Title("$titleContent", null, TitleAlignments.Centered)]
        [HideLabel]
        [DisplayAsString]
        public string title = "";

        [HideInInspector]
        private string des;

        public PackageImpData(string titleContent,string des,string logoName) {

            this.titleContent = titleContent;
            this.des = des;
            this.logoContent= logoName;
        }
        [OnInspectorInit]
        public void CreateData()
        {
            RegularPreviewField = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PackagePath}/{logoContent}.jpg");
            if (RegularPreviewField==null)
            {
                RegularPreviewField = AssetDatabase.LoadAssetAtPath<Texture2D>($"{PackagePath}/{logoContent}.png");
            }
         }


        [OnInspectorGUI]
        public void GUI()
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button(RegularPreviewField,GUILayout.Width(512),GUILayout.Height(300)))
            {
                Application.OpenURL("www.baidu.com");
            }
            
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(des);
            GUILayout.EndHorizontal();
        }
   


        [AssetsOnly]
        [Button(ButtonHeight = 40, Name = "Import")]
        public  void ButtonClick()
        {
            if (titleContent.Contains("Debuger"))
            {
                // UnityEditor.PackageManager.Requests.AddRequest request= UnityEditor.PackageManager.Client.Add("https://github.com/ZMteacher/ZMUIFrameWork.git");
                // while (!request.IsCompleted)
                // {
                //       request.Yield();
                //  }
                AssetDatabase.ImportPackage($"{PackagePath}/ZM UnityDebuger.unitypackage", true);
            }
            else if (titleContent.Contains("UGUIPro"))
            {
                AssetDatabase.ImportPackage($"{PackagePath}/ZM UGUIPro.unitypackage", true);
            }
            else if (titleContent.Contains("ZMUI"))
            {
                // UnityEditor.PackageManager.Requests.AddRequest request= UnityEditor.PackageManager.Client.Add("git@github.com:ZMteacher/ZMUIFrameWork.git");
                // while (!request.IsCompleted)
                // {
                //       request.Yield();
                //  }
                // Debug.Log(request.Result);
                AssetDatabase.ImportPackage($"{PackagePath}/ZMUI.unitypackage", true);
            }

        }
 
    }  

}
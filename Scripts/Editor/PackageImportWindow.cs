/*----------------------------------------------------------------------------
* Title: 帧同步定点数学库
*
* Author: 铸梦
*
* Date: 2025.02.20
*
* Description:基于定点数实现的一套AABB定点数学物理碰撞库，可用于客户端和服务端。
*
* Remarks: QQ:975659933 邮箱：zhumengxyedu@163.com
*
* 案例地址：www.yxtown.com/user/38633b977fadc0db8e56483c8ee365a2cafbe96b
----------------------------------------------------------------------------*/
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UIElements;

public class PackageImportWindow : EditorWindow
{
    private string m_TexturePath = "Packages/com.zm.import/Editor Resources/";
    private string m_PackagePath = "Packages/com.zm.import/Package Resources/";
    private Vector2 scrollPosition;
    private List<PackageItem> packageItems = new List<PackageItem>();
    private Texture2D defaultButtonTexture;
    private GUIStyle centeredButtonStyle;
    
    [MenuItem("Window/Package Import Window")]
    public static void ShowWindow()
    {
        GetWindowWithRect<PackageImportWindow>(new Rect(0, 0, 1000, 900));
    }

    private void OnEnable()
    {
        // 创建默认按钮纹理（如果没有找到资源）
        defaultButtonTexture = new Texture2D(1, 1);
        defaultButtonTexture.SetPixel(0, 0, new Color(0.3f, 0.3f, 0.3f));
        defaultButtonTexture.Apply();
        // 初始化示例数据
        InitializePackageItems();
    }

    private void InitializePackageItems()
    {
        packageItems.Clear();

        // 示例数据 - 实际应用中可以从配置文件或API加载
        packageItems.Add(new PackageItem(
            "UI框架 ZMUIFrameWork",
            LoadTexture($"{m_TexturePath}/ZMUI.jpg"),
            "ZMUI经过百万DAU验证过的一款Mono分离式，高性能、自动化、商业级UI框架UI框架。从底层解决性能问题代码一键生成、组件一键绑定、事件自动监听、彻底解放双手，提升50%开发效率."
        ));

        packageItems.Add(new PackageItem(
            "资源热更框架 ZMAsset",
            LoadTexture($"{m_TexturePath}/ZMAsset.jpg"),
            "ZMAsset是一款多模块、多线程、资源热更管理框架。无需编写代码，即可轻松实现资源热更及资源加载,包含：可视化多模块打包器、多模块热更、多线程下载、多版本热更、多版本回退加密、解密、内嵌、解压、内存引用计数、大型对象池、AssetBundle加载、Editor加载"
        ));

        packageItems.Add(new PackageItem(
            "游戏框架 DMVC",
            LoadTexture($"{m_TexturePath}/DMVC.jpg"),
            "DMVC是一款基于MVC思想设计的智能化、多世界、多模块的解耦游戏框架,与HyBridCLR多程序集设计超级适配的游戏框架，大道至简.数据层、逻辑层、网络层，最好配合ZMUI使用."
        ));
        
        packageItems.Add(new PackageItem(
            "日志插件 DebugerTookit",
            LoadTexture($"{m_TexturePath}/Debuger.jpg"),
            "该插件是一款超级完善的日志系统，包含子线程本地日志文件写入、自定义颜色日志，移动设备日志查看工具、FPS实时显示、DLL日志编译时剔除、ProtoBuff转Json字符串打印日志、日志重定向.."
        ));
        
        packageItems.Add(new PackageItem(
            "ZMUGUIPro",
            LoadTexture($"{m_TexturePath}/ZMUGUIPro.jpg"),
            "该插件是一款UGUI拓展插件、包含：超高性能描边、高性能裁剪、文本多语言、图片多语言、按钮长按事件、按钮双击事件、点击缩放、点击音效等.."
        ));
        
        packageItems.Add(new PackageItem(
            "更多功能",
            LoadTexture($"{m_TexturePath}/ComingSoon.jpg"),
            "更多功能和实战案例正在制作中,点击上方图片查看详情信息..."
        ));
    }

    private Texture2D LoadTexture(string path)
    {
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        return texture != null ? texture : defaultButtonTexture;
    }

    private void OnGUI()
    {
        if (centeredButtonStyle == null)
        {
            centeredButtonStyle = new GUIStyle(GUI.skin.button)
            {
                padding = new RectOffset(0, 0, 0, 0),
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove
            };
        }
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Available Packages", EditorStyles.boldLabel);
        GUILayout.Space(10);

        // 开始滚动视图
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // 2列布局
        int columnCount = 2;
        int itemCount = 0;

        while (itemCount < packageItems.Count)
        {
            EditorGUILayout.BeginHorizontal();

            for (int i = 0; i < columnCount; i++)
            {
                if (itemCount + i < packageItems.Count)
                {
                    DrawPackageItem(packageItems[itemCount + i]);
                }
                else
                {
                    // 空项目填充布局
                    GUILayout.FlexibleSpace();
                }
            }

            EditorGUILayout.EndHorizontal();
            itemCount += columnCount;
        }

        EditorGUILayout.EndScrollView();
    }

    private void DrawPackageItem(PackageItem item)
    {
        EditorGUILayout.BeginVertical("box", GUILayout.Width(position.width / 2 - 15), GUILayout.Height(300));

        // 标题
        EditorGUILayout.LabelField(item.Title,new GUIStyle(EditorStyles.boldLabel){ alignment = TextAnchor.MiddleCenter });
        GUI.DrawTexture(GUILayoutUtility.GetRect(position.width / 2 - 30,1),defaultButtonTexture,ScaleMode.StretchToFill);
        
        GUILayout.Space(10);

        // 大按钮
        if (GUILayout.Button(new GUIContent(item.ButtonTexture),centeredButtonStyle, GUILayout.Height(300), GUILayout.Width(position.width / 2 - 30)))
        {
            Debug.Log($"Clicked on {item.Title}");
            // 这里可以添加按钮点击逻辑
            OnTextureButtonClick(item);
        }
  
        GUILayout.Space(10);

        // 描述文本
        EditorStyles.wordWrappedLabel.wordWrap = true;
        EditorGUILayout.LabelField(item.Description, EditorStyles.wordWrappedLabel);
        GUILayout.Space(10);

        // 导入按钮
        if (GUILayout.Button("Import", GUILayout.Height(30)))
        {
            if (EditorUtility.DisplayDialog("Import Package", $"Are you sure you want to import {item.Title}?", "Import", "Cancel"))
            {
                Debug.Log($"Importing {item.Title}");
                // 这里添加实际的导入逻辑
                OnImportButtonClick(item);
            }
        }

        EditorGUILayout.EndVertical();
    }

    private void OnImportButtonClick(PackageItem item)
    {
        if (item.Title == "UI框架 ZMUIFrameWork")
        {
            AssetDatabase.ImportPackage($"{m_PackagePath}/ZMUI.unitypackage", true);
        }
        else if (item.Title == "资源热更框架 ZMAsset")
        {
            AssetDatabase.ImportPackage($"{m_PackagePath}/ZMAsset.unitypackage", true);
        }
        else if (item.Title == "游戏框架 DMVC")
        {
            AssetDatabase.ImportPackage($"{m_PackagePath}/ZMGC.unitypackage", true);
        }
        else if (item.Title == "日志插件 DebugerTookit")
        {
            AssetDatabase.ImportPackage($"{m_PackagePath}/ZM UnityDebuger.unitypackage", true);
        }
        else if (item.Title == "ZMUGUIPro")
        {
            AssetDatabase.ImportPackage($"{m_PackagePath}/ZMUGUIPro.unitypackage", true);
        }
        else if (item.Title == "更多功能")
        {
            Application.OpenURL("https://www.yxtown.com/user/38633b977fadc0db8e56483c8ee365a2cafbe96b");
        }
    }
    private void OnTextureButtonClick(PackageItem item)
    {
        if (item.Title == "UI框架 ZMUIFrameWork")
        {
            Application.OpenURL("https://github.com/ZMteacher/ZMUIFrameWork");
        }
        else if (item.Title == "资源热更框架 ZMAsset")
        {
            Application.OpenURL("https://github.com/ZMteacher/ZMAsset");
        }
        else if (item.Title == "游戏框架 DMVC")
        {
            Application.OpenURL("https://github.com/ZMteacher/ZMGC");
        }
        else if (item.Title == "日志插件 DebugerTookit")
        {
            Application.OpenURL("https://github.com/ZMteacher/DebugerToolkit");
        }
        else if (item.Title == "ZMUGUIPro")
        {
            Application.OpenURL("https://github.com/ZMteacher/ZMUGUIPro");
        }
        else if (item.Title == "更多功能")
        {
            Application.OpenURL("https://www.yxtown.com/user/38633b977fadc0db8e56483c8ee365a2cafbe96b");
        }
    }
    // 包项目数据类
    private class PackageItem
    {
        public string Title { get; }
        public Texture2D ButtonTexture { get; }
        public string Description { get; }

        public PackageItem(string title, Texture2D buttonTexture, string description)
        {
            Title = title;
            ButtonTexture = buttonTexture;
            Description = description;
        }
    }
}
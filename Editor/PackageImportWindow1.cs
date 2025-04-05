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

public class PackageImportWindow1 : EditorWindow
{
    private Vector2 scrollPosition;
    private List<PackageItem> packageItems = new List<PackageItem>();
    private Texture2D defaultButtonTexture;
    private GUIStyle centeredButtonStyle;
    
    [MenuItem("Window/Package Import Window")]
    public static void ShowWindow()
    {
        GetWindow<PackageImportWindow1>("Package Importer");
    }

    private void OnEnable()
    {
        // 创建默认按钮纹理（如果没有找到资源）
        defaultButtonTexture = new Texture2D(1, 1);
        defaultButtonTexture.SetPixel(0, 0, new Color(0.2f, 0.2f, 0.2f));
        defaultButtonTexture.Apply();
        
        centeredButtonStyle = new GUIStyle(GUI.skin.button)
        {
            padding = new RectOffset(0, 0, 0, 0),
            alignment = TextAnchor.MiddleCenter,
            imagePosition = ImagePosition.ImageAbove
        };
        
        // 初始化示例数据
        InitializePackageItems();
    }

    private void InitializePackageItems()
    {
        packageItems.Clear();

        // 示例数据 - 实际应用中可以从配置文件或API加载
        packageItems.Add(new PackageItem(
            "UI Toolkit Package",
            LoadTexture("Assets/Editor/ZMAsset.jpg"),
            "包含各种UI Toolkit组件和模板，简化UI开发流程。"
        ));

        packageItems.Add(new PackageItem(
            "Shader Effects",
            LoadTexture("Assets/Editor/Shader Icon.png"),
            "一系列高级着色器效果，包括水、雾和特殊材质。"
        ));

        packageItems.Add(new PackageItem(
            "AI Utilities",
            LoadTexture("Assets/Editor/AI Icon.png"),
            "人工智能工具集，包含路径查找和行为树实现。"
        ));
    }

    private Texture2D LoadTexture(string path)
    {
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        return texture != null ? texture : defaultButtonTexture;
    }

    private void OnGUI()
    {
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
        EditorGUILayout.LabelField(item.Title, EditorStyles.boldLabel);
        GUILayout.Space(10);

        // 大按钮
        GUIContent buttonContent = new GUIContent();
        buttonContent.image = item.ButtonTexture;
        if (GUILayout.Button(buttonContent,centeredButtonStyle, GUILayout.Height(300), GUILayout.Width(position.width / 2 - 30)))
        {
            Debug.Log($"Clicked on {item.Title}");
            // 这里可以添加按钮点击逻辑
        }
        // if (GUILayout.Button(item.ButtonTexture, GUILayout.Height(300), GUILayout.Width(position.width / 2 - 30)))
        // {
        //     Debug.Log($"Clicked on {item.Title}");
        //     // 这里可以添加按钮点击逻辑
        // }
        GUILayout.Space(10);

        // 描述文本
        EditorStyles.wordWrappedLabel.wordWrap = true;
        EditorGUILayout.LabelField(item.Description, EditorStyles.wordWrappedLabel);
        GUILayout.Space(10);

        // 导入按钮
        if (GUILayout.Button("Import", GUILayout.Height(30)))
        {
            if (EditorUtility.DisplayDialog("Import Package", 
                $"Are you sure you want to import {item.Title}?", 
                "Import", "Cancel"))
            {
                Debug.Log($"Importing {item.Title}");
                // 这里添加实际的导入逻辑
            }
        }

        EditorGUILayout.EndVertical();
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
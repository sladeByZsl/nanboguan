using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace TEngine.Editor
{
    [CustomEditor(typeof(ExButton), true)]
    [CanEditMultipleObjects]
    public class ExButtonEditor : ButtonEditor
    {
        SerializedProperty generalSoundNameProperty;
        SerializedProperty btnTextProperty;
        SerializedProperty buttonCdProperty;
        SerializedProperty msgKeyProperty;
        SerializedProperty cdMaskProperty;
        SerializedProperty cdTextProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            // 获取序列化属性
            generalSoundNameProperty = serializedObject.FindProperty("generalSoundName");
            btnTextProperty = serializedObject.FindProperty("btnText");
            buttonCdProperty = serializedObject.FindProperty("button_cd");
            msgKeyProperty = serializedObject.FindProperty("msg_key");
            cdMaskProperty = serializedObject.FindProperty("m_cd_mask");
            cdTextProperty = serializedObject.FindProperty("m_cd_text");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // 绘制按钮基础属性（继承自Button的属性）
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            
            // 保存修改前的值
            string previousText = btnTextProperty.stringValue;
            EditorGUILayout.PropertyField(btnTextProperty);
            
            // 如果文本被修改且不为空，自动设置子节点中的Text组件
            if (btnTextProperty.stringValue != previousText && !string.IsNullOrEmpty(btnTextProperty.stringValue))
            {
                ExButton exButton = (ExButton)target;
                UnityEngine.UI.Text textComponent = exButton.GetComponentInChildren<UnityEngine.UI.Text>();
                if (textComponent != null)
                {
                    Undo.RecordObject(textComponent, "Update Button Text");
                    textComponent.text = btnTextProperty.stringValue;
                    EditorUtility.SetDirty(textComponent);
                }
            }
            
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(buttonCdProperty);

            // 绘制ExButton特有属性
            EditorGUILayout.PropertyField(generalSoundNameProperty);
            
            EditorGUILayout.Space();
            // 只有当CD大于0时才显示CD相关属性
            if (buttonCdProperty.floatValue > 0)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(msgKeyProperty);
                EditorGUILayout.PropertyField(cdMaskProperty);
                EditorGUILayout.PropertyField(cdTextProperty);
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
} 
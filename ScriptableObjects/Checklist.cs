using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif

namespace UnityUtils {

[CreateAssetMenu(fileName = "Checklist", menuName = "Unity Utils/Checklist")]
public class Checklist : ScriptableObject {
    [System.Serializable]
    public class ChecklistItem {
        public bool complete;
        public string text;
    }

    [System.Serializable]
    public class ChecklistItemRoot {
        public ChecklistItem item;
        public List<ChecklistItem> subtaskList;
    }

    public List<ChecklistItemRoot> taskList = new List<ChecklistItemRoot>();
    
    public StyleSheet styleSheet;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Checklist.ChecklistItem))]
public class ChecklistItemDrawer : PropertyDrawer { //inherit from property drawer to change how a property is drawn

    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        VisualElement container = new VisualElement();
        
        //holy serialization batman!
        //StyleSheet sheet = typeof(Checklist).GetField("styleSheet").GetValue(property.serializedObject.targetObject) as StyleSheet;
        //if (sheet != null) container.styleSheets.Add(sheet);
        container.style.flexDirection = FlexDirection.Row;
        container.style.justifyContent = Justify.Center;
        container.style.alignContent = Align.Center;
        container.style.alignItems = Align.Center;
        container.style.marginTop = 1;
        
        var tex = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/UnityUtils/Icons/handle.png");
        // if (tex == null) {
        //     tex = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/UnityUtils/Icons/handle.png");
        // }

        VisualElement handle = new VisualElement();
        handle.style.width = 18;
        handle.style.height = 18;
        handle.style.flexShrink = 0;
        handle.style.marginRight = 4;
        handle.style.justifyContent = Justify.Center;
        handle.style.alignContent = Align.Center;
        handle.style.alignItems = Align.Center;
        handle.style.backgroundImage = tex;//Background.FromTexture2D(EditorGUIUtility.IconContent("btn_AlignMiddle").image as Texture2D);//d_Remove@2x, check-dash@2x
        handle.style.unityBackgroundImageTintColor = UnityUtils.GetEditorUIPrimaryColor();
        container.Add(handle);
        
        Toggle tickBox = new Toggle();
        tickBox.BindProperty(property.FindPropertyRelative("complete"));
        tickBox.label = "";
        tickBox.style.flexShrink = 0;
        tickBox.style.marginRight = 4;
        container.Add(tickBox);
        
        TextField textBox = new TextField();
        textBox.BindProperty(property.FindPropertyRelative("text"));
        textBox.label = "";
        textBox.style.flexGrow = 1;
        textBox.style.flexShrink = 1;
        textBox.style.marginRight = 0;
        textBox.multiline = true;
        textBox.style.whiteSpace = WhiteSpace.Normal;
        container.Add(textBox);

        return container;
    }
}

[CustomPropertyDrawer(typeof(Checklist.ChecklistItemRoot))]
public class ChecklistItemRootDrawer : PropertyDrawer { //inherit from property drawer to change how a property is drawn
    
    [SerializeField]
    Dictionary<VisualElement,ContextualMenuManipulator> manipulatorDictionary = new();
    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        VisualElement container = new VisualElement();
        container.style.flexDirection = FlexDirection.Column;
        
        VisualElement header = new VisualElement();
        header.style.flexDirection = FlexDirection.Row;
        header.style.alignItems = Align.Center;
        
        PropertyField item = new PropertyField(property.FindPropertyRelative("item"));
        item.style.flexGrow = 1;
        header.Add(item);
        
        Button addButton = new Button();
        addButton.text = "+";
        addButton.clickable = new Clickable(() => {
            // property.FindPropertyRelative("children").InsertArrayElementAtIndex(property.FindPropertyRelative("children").arraySize);
            // property.FindPropertyRelative("children").serializedObject.ApplyModifiedProperties();
            
            SerializedProperty array = property.FindPropertyRelative("subtaskList");
            array.InsertArrayElementAtIndex(array.arraySize);
            array.GetArrayElementAtIndex(array.arraySize - 1).boxedValue = new Checklist.ChecklistItem();
            array.serializedObject.ApplyModifiedProperties();
        });
        header.Add(addButton);
        
        container.Add(header);
        

        ListView list = new ListView();
        list.BindProperty(property.FindPropertyRelative("subtaskList"));
        list.showBoundCollectionSize = false;
        list.style.minHeight = 0;
        list.selectionType = SelectionType.None;
        list.reorderable = true;
        list.reorderMode = ListViewReorderMode.Animated;
        list.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
        list.makeNoneElement = () => { return new VisualElement();};
        list.makeItem = () => {
            VisualElement itemContainer = new VisualElement();
            itemContainer.style.flexDirection = FlexDirection.Row;
            PropertyField itemField = new PropertyField();
            itemField.style.flexGrow = 1;
            itemContainer.Add(itemField);
            return itemContainer;
        };
        list.bindItem = (container, index) => {
            PropertyField itemField = container.Q<PropertyField>();
            itemField.BindProperty(list.itemsSource[index] as SerializedProperty);
            manipulatorDictionary.Add(container, new ContextualMenuManipulator((ContextualMenuPopulateEvent evt) => {
                evt.menu.InsertAction(0, "Delete SubTask",
                    a => {
                        property.FindPropertyRelative("subtaskList").DeleteArrayElementAtIndex(index);
                        property.FindPropertyRelative("subtaskList").serializedObject.ApplyModifiedProperties();
                    },
                    action => {
                        return DropdownMenuAction.Status.Normal;
                    });
                evt.menu.InsertSeparator("", 1);
                evt.StopPropagation();
            }));
            container.AddManipulator(manipulatorDictionary[container]);
        };
        list.unbindItem = (container, index) => {
            PropertyField itemField = container.Q<PropertyField>();
            itemField.Unbind();
            if (manipulatorDictionary.ContainsKey(container)) {
                container.RemoveManipulator(manipulatorDictionary[container]);
                manipulatorDictionary.Remove(container);
            }
        };
        container.Add(list);

        return container;
    }
}

[CustomEditor(typeof(Checklist))]
public class ChecklistDrawer : Editor { //inherit from property drawer to change how a property is drawn

    [SerializeField]
    Dictionary<VisualElement,ContextualMenuManipulator> manipulatorDictionary = new();
    public override VisualElement CreateInspectorGUI() {
        Checklist script = (Checklist)target;
        VisualElement container = new VisualElement();
        container.styleSheets.Add(script.styleSheet);
        container.style.flexDirection = FlexDirection.Column;
        
        Label title = new Label();
        title.text = UnityUtils.FormatCamelCase(script.name);
        title.style.fontSize = 24;
        title.style.marginTop = title.style.marginBottom = 12;
        container.Add(title);

        ListView list = new ListView();
        list.BindProperty(serializedObject.FindProperty("taskList"));
        list.showBoundCollectionSize = false;
        list.style.minHeight = 0;
        list.selectionType = SelectionType.None;
        list.reorderable = true;
        list.reorderMode = ListViewReorderMode.Animated;
        list.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
        list.makeItem = () => {
            VisualElement itemContainer = new VisualElement();
            itemContainer.style.flexDirection = FlexDirection.Row;
            PropertyField itemField = new PropertyField();
            itemField.style.flexGrow = 1;
            itemContainer.Add(itemField);
            return itemContainer;
        };
        list.makeNoneElement = () => { return new VisualElement();};
        list.bindItem = (container, index) => {
            PropertyField itemField = container.Q<PropertyField>();
            itemField.BindProperty(list.itemsSource[index] as SerializedProperty);
            manipulatorDictionary.Add(itemField, new ContextualMenuManipulator((ContextualMenuPopulateEvent evt) => {
                evt.menu.InsertAction(0, "Delete Task",
                    a => {
                        serializedObject.FindProperty("taskList").DeleteArrayElementAtIndex(index);
                        serializedObject.FindProperty("taskList").serializedObject.ApplyModifiedProperties();
                    },
                    action => {
                        return DropdownMenuAction.Status.Normal;
                    });
                evt.menu.InsertSeparator("", 1);
            }));
            itemField.AddManipulator(manipulatorDictionary[itemField]);
        };
        list.unbindItem = (container, index) => {
            PropertyField itemField = container.Q<PropertyField>();
            itemField.Unbind();
            if (manipulatorDictionary.ContainsKey(itemField)) {
                itemField.RemoveManipulator(manipulatorDictionary[itemField]);
                manipulatorDictionary.Remove(itemField);
            }
        };
        container.Add(list);

        VisualElement footer = new VisualElement();
        footer.style.flexDirection = FlexDirection.Row;
        footer.style.justifyContent = Justify.FlexEnd;
        footer.style.marginTop = 8;
        
        Button addButton = new Button();
        addButton.text = "+ New Task";
        addButton.clickable = new Clickable(() => {
            SerializedProperty array = serializedObject.FindProperty("taskList");
            array.InsertArrayElementAtIndex(array.arraySize);
            array.GetArrayElementAtIndex(array.arraySize-1).boxedValue = new Checklist.ChecklistItemRoot();
            array.serializedObject.ApplyModifiedProperties();
        });
        footer.Add(addButton);
        
        container.Add(footer);
        
        return container;
    }
}
#endif

}
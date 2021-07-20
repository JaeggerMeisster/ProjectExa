using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Types;
using Exa.Utils;
using UnityEditor;

namespace Exa.CustomEditors {
    [CustomEditor(typeof(Block), true)]
    public class BlockEditor : Editor {
        private DefaultDict<Type, BlockDataInfo> typeCache;
        
    #if ENABLE_BLOCK_LOGS
        private bool foldoutLogs = true;
    #endif
        
        private void OnEnable() {
            typeCache = new DefaultDict<Type, BlockDataInfo>(type => new BlockDataInfo(type));
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            base.OnInspectorGUI();

            var block = (Block) target;

            foreach (var behaviour in block.GetBehaviours()) {
                var data = behaviour.BlockComponentData;
                data = typeCache[data.GetType()].Draw(data);

                try {
                    behaviour.BlockComponentData = data;
                } catch { }
            }
            
            EditorGUILayout.Space(8);
            
            block.DebugFocused = EditorGUILayout.Toggle("Focus on block", block.DebugFocused);
            
        #if ENABLE_BLOCK_LOGS
            EditorGUILayout.Space(8);

            foldoutLogs = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutLogs, "Show block logs");

            if (foldoutLogs) {
                EditorGUI.indentLevel++;
                
                foreach (var log in block.Logs) {
                    EditorGUILayout.LabelField(log);
                }

                EditorGUI.indentLevel--;
            }
        #endif
        }

        private class BlockDataInfo {
            private bool foldout = true;
            private readonly string name;
            private readonly IEnumerable<FieldInfo> properties;

            public BlockDataInfo(Type dataType) {
                name = dataType.Name;
                properties = dataType.GetFields().ToList();
            }

            public IBlockComponentValues Draw(IBlockComponentValues values) {
                EditorGUILayout.Space();

                foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, name);
                
                if (foldout) {
                    EditorGUI.indentLevel += 1;
                    
                    foreach (var prop in properties) {
                        var currentValue = prop.GetValue(values);
                        var value = DrawField(prop.Name.ToProperCase(), currentValue, prop.FieldType);

                        if (value != null && !currentValue.Equals(value)) {
                            prop.SetValue(values, value);
                        }
                    }

                    EditorGUI.indentLevel -= 1;
                }

                EditorGUILayout.EndFoldoutHeaderGroup();

                return values;
            }

            private object DrawField(string name, object c, Type cType) {
                if (cType == typeof(bool)) {
                    return EditorGUILayout.Toggle(name, (bool) c);
                }

                if (cType == typeof(float)) {
                    return EditorGUILayout.FloatField(name, (float) c);
                }

                return null;
            }
        }
    }
}
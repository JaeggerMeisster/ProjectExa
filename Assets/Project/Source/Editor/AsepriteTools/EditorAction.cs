using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Exa.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Exa.CustomEditors {
    public abstract class EditorAction {
        protected readonly AsepriteTools context;

        protected EditorAction(AsepriteTools context) {
            this.context = context;
        }

        public abstract void Render();

        public abstract void Execute();

        protected Process GetProcess(string parameters) {
            return new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = context.AsepritePath,
                    Arguments = parameters,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
        }
    }
}
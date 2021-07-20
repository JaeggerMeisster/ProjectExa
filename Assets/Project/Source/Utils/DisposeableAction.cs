using System;
using UnityEngine;

namespace Exa.Utils {
    /// <summary>    
    /// The disposable action.    
    /// </summary>    
    public class DisposableAction : IDisposable {
        private Action dispose;

        /// <summary>    
        /// Initializes a new instance of the <see cref="DisposableAction"/> class.    
        /// </summary>    
        /// <param name="dispose">    
        /// The dispose.    
        /// </param>    
        public DisposableAction(Action dispose) {
            this.dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
        }

        /// <summary>    
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.    
        /// </summary>    
        /// <filterpriority>2</filterpriority>    
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>    
        /// The dispose.    
        /// </summary>    
        /// <param name="disposing">    
        /// The disposing.    
        /// </param>    
        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (dispose == null) {
                    return;
                }

                try {
                    dispose();
                } catch (Exception ex) {
                    Debug.LogException(ex);
                }

                dispose = null;
            }
        }
    }
}
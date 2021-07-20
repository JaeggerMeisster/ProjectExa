using System;
using Exa.Data;
using Exa.UI.Components;

namespace Exa.UI.Settings {
    /// <summary>
    ///     Base class for the manager of a settings object
    /// </summary>
    /// <typeparam name="TSettings">Type of the settings object</typeparam>
    /// <typeparam name="TValues">Type of the settings values for the settings object</typeparam>
    public abstract class SettingsTab<TSettings, TValues> : SettingsTabBase
        where TSettings : SaveableSettings<TValues>
        where TValues : class, IEquatable<TValues> {
        /// <summary>
        ///     Current stored settings object
        /// </summary>
        private TSettings settingsBackingField;

        public TSettings Container {
            get => settingsBackingField ??= GetSettingsContainer();
        }

        public override bool IsDefault {
            get => Container.Values.Equals(Container.DefaultValues);
        }

        public override bool IsDirty {
            get => !Container.Values.Equals(GetSettingsValues());
        }

        /// <summary>
        ///     Reflects the values of the settings
        /// </summary>
        /// <param name="values"></param>
        public abstract void ReflectValues(TValues values);

        /// <summary>
        ///     Gets a settings object
        /// </summary>
        /// <returns></returns>
        public abstract TValues GetSettingsValues();

        public virtual void Init() {
            Container.Load();
            Container.Apply();
            Container.Save();
            ReflectValues(Container.Values);
        }

        public override void SetDefaultValues() {
            var values = Container.DefaultValues;
            ReflectValues(values);
            Apply(values);
        }

        public override void ApplyChanges() {
            Apply(GetSettingsValues());
        }

        /// <summary>
        ///     Applies the given values
        /// </summary>
        /// <param name="values"></param>
        protected void Apply(TValues values) {
            Container.Values = values;
            Container.Save();
            Container.Apply();
        }

        protected abstract TSettings GetSettingsContainer();
    }

    public abstract class SettingsTabBase : SettingsTab {
        /// <summary>
        ///     Denotes wether the current settings object is equal to the default values
        /// </summary>
        public abstract bool IsDefault { get; }

        public abstract bool IsDirty { get; }

        /// <summary>
        ///     Applies the default values
        /// </summary>
        public abstract void SetDefaultValues();

        /// <summary>
        ///     Applies the settings from the controls
        /// </summary>
        public abstract void ApplyChanges();
    }
}
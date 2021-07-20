namespace Exa.Audio {
    /// <summary>
    ///     Supports managing the events for a given sound handle
    /// </summary>
    public interface ITrackContext {
        /// <summary>
        ///     Registers a sound on the track context
        ///     <para>
        ///         This is used to setup the events for the handle
        ///     </para>
        /// </summary>
        /// <param name="soundHandle">Sound handle to register</param>
        void RegisterHandle(SoundHandle soundHandle);

        /// <summary>
        ///     Stops all the sounds in the audio track
        /// </summary>
        void StopAllSounds();
    }
}
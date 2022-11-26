namespace VTTiny.Editor
{
    internal class VTubeTinyEditor
    {
        private VTubeTiny VTubeTiny { get; set; }

        /// <summary>
        /// Instantiates a new VTubeTiny editor instance from a given VTubeTiny instance.
        /// </summary>
        /// <param name="instance">The VTubeTiny instance.</param>
        public VTubeTinyEditor(VTubeTiny instance)
        {
            VTubeTiny = instance;
        }

        /// <summary>
        /// Initializes the editor and all of the systems it needs.
        /// </summary>
        public void Initialize()
        {
            
        }

        /// <summary>
        /// Renders the editor.
        /// </summary>
        public void Render()
        {
            VTubeTiny.ActiveStage.RenderEditorGUI();
        }

        /// <summary>
        /// Destroys all of the systems and data used by the editor.
        /// </summary>
        public void Destroy()
        {

        }
    }
}

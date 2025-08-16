using Umbraco.Cms.Core.PropertyEditors;

namespace GingerSquirrel.SeoPreview.PropertyEditors
{
    // Legacy property editor for existing data types
    [DataEditor("GingerSquirrel.SEOPreview")]
    public class SeoPreviewPropertyEditor : DataEditor
    {
        public SeoPreviewPropertyEditor(IDataValueEditorFactory dataValueEditorFactory)
            : base(dataValueEditorFactory)
        {
        }
    }

    // New property editor UI for the frontend
    [DataEditor("GingerSquirrel.PropertyEditorUi.SEOPreview")]
    public class SeoPreviewPropertyEditorUi : DataEditor
    {
        public SeoPreviewPropertyEditorUi(IDataValueEditorFactory dataValueEditorFactory)
            : base(dataValueEditorFactory)
        {
        }
    }
}

using Umbraco.Cms.Core.PropertyEditors;

namespace GingerSquirrel.SeoPreview.PropertyEditors
{
    [DataEditor("GingerSquirrel.PropertyEditorUi.SEOPreview")]
    public class SeoPreviewPropertyEditorUi : DataEditor
    {
        public SeoPreviewPropertyEditorUi(IDataValueEditorFactory dataValueEditorFactory)
            : base(dataValueEditorFactory)
        {
        }
    }
}

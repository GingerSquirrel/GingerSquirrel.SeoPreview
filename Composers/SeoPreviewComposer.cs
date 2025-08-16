using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using GingerSquirrel.SeoPreview.PropertyValueConverters;

namespace GingerSquirrel.SeoPreview.Composers
{
    public class SeoPreviewComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            // Register the property value converter
            builder.PropertyValueConverters().Append<SeoPreviewPropertyValueConverter>();
        }
    }
}

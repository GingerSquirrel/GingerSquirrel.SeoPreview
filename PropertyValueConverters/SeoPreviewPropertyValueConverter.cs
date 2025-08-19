using System.Text.Json;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using GingerSquirrel.SeoPreview.Models;

namespace GingerSquirrel.SeoPreview.PropertyValueConverters
{
    public class SeoPreviewPropertyValueConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(IPublishedPropertyType propertyType)
        {
            return propertyType.EditorAlias.Equals("GingerSquirrel.PropertyEditorUi.SEOPreview", StringComparison.OrdinalIgnoreCase);
        }

        public override object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object? source, bool preview)
        {
            if (source == null) return null;

            var sourceString = source.ToString();
            if (string.IsNullOrWhiteSpace(sourceString)) return null;

            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<SeoMetaModel>(sourceString, options);
            }
            catch (JsonException)
            {
                return null;
            }
        }

        public override object? ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object? inter, bool preview)
        {
            return inter as SeoMetaModel;
        }

        public override Type GetPropertyValueType(IPublishedPropertyType propertyType)
        {
            return typeof(SeoMetaModel);
        }
    }
}
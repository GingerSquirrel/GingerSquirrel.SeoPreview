using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using GingerSquirrel.SeoPreview.Models;
using System.Text.Json;

namespace GingerSquirrel.SeoPreview.Extensions
{
    /// <summary>
    /// Extension methods for IPublishedContent to provide easy access to SEO properties
    /// </summary>
    public static class PublishedContentExtensions
    {

    private const string SeoEditorAlias = "GingerSquirrel.PropertyEditorUi.SEOPreview";
    private static readonly Dictionary<string, string?> SeoPropertyAliasCache = new();

        /// <summary>
        /// Gets the strongly-typed SEO model from the first property using the SEO editor alias.
        /// </summary>
        public static SeoMetaModel GetSeoMetaModel(this IPublishedContent content)
        {
            if (content == null) return new SeoMetaModel();

            // Try to get cached property alias for this content type
            var contentTypeAlias = content.ContentType.Alias;

            if (!SeoPropertyAliasCache.TryGetValue(contentTypeAlias, out var seoAlias))
            {
                seoAlias = content.ContentType.PropertyTypes
                    .FirstOrDefault(pt => pt.EditorAlias == SeoEditorAlias)?.Alias;
                SeoPropertyAliasCache[contentTypeAlias] = seoAlias;
            }

            if (string.IsNullOrEmpty(seoAlias)) return new SeoMetaModel();

            var rawValue = content.Value(seoAlias)?.ToString();
            if (string.IsNullOrWhiteSpace(rawValue)) return new SeoMetaModel();

            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<SeoMetaModel>(rawValue, options) ?? new SeoMetaModel();
            }
            catch (JsonException)
            {
                return new SeoMetaModel();
            }
    }

        /// <summary>
        /// Gets the meta title from the SEO composition if available, otherwise returns the content name
        /// </summary>
        /// <param name="content">The published content</param>
        /// <param name="fallbackToName">Whether to fallback to the content name if no meta title is set</param>
        /// <returns>The meta title or fallback value</returns>
        public static string GetMetaTitle(this IPublishedContent content, bool fallbackToName = true)
            => !string.IsNullOrWhiteSpace(content?.GetSeoMetaModel().MetaTitle)
                ? content.GetSeoMetaModel().MetaTitle
                : (fallbackToName ? (content?.Name ?? "") : "");

        /// <summary>
        /// Gets the meta description from the SEO composition if available
        /// </summary>
        /// <param name="content">The published content</param>
        /// <returns>The meta description or empty string</returns>
        public static string GetMetaDescription(this IPublishedContent content)
            => content?.GetSeoMetaModel().MetaDescription ?? "";

        /// <summary>
        /// Gets the SEO validation status for the content
        /// </summary>
        /// <param name="content">The published content</param>
        /// <returns>True if SEO is considered good, false otherwise</returns>
        public static bool HasGoodSeo(this IPublishedContent content)
        {
            var seoData = content?.GetSeoMetaModel();
            if (seoData == null) return false;
            return GetSeoWarningsFromData(seoData).Count == 0;
        }

        /// <summary>
        /// Helper method to generate SEO warnings from SeoMetaModel
        /// </summary>
        private static List<string> GetSeoWarningsFromData(SeoMetaModel seoData)
        {
            var warnings = new List<string>();
            
            if (string.IsNullOrWhiteSpace(seoData.MetaTitle))
            {
                warnings.Add("Meta title is missing");
            }
            else if (seoData.MetaTitle.Length < 50)
            {
                warnings.Add("Meta title is too short (less than 50 characters)");
            }
            else if (seoData.MetaTitle.Length > 60)
            {
                warnings.Add("Meta title is too long (more than 60 characters)");
            }
            
            if (string.IsNullOrWhiteSpace(seoData.MetaDescription))
            {
                warnings.Add("Meta description is missing");
            }
            else if (seoData.MetaDescription.Length < 120)
            {
                warnings.Add("Meta description is too short (less than 120 characters)");
            }
            else if (seoData.MetaDescription.Length > 155)
            {
                warnings.Add("Meta description is too long (more than 155 characters)");
            }
            
            return warnings;
        }

        /// <summary>
        /// Gets the SEO warnings for the content
        /// </summary>
        /// <param name="content">The published content</param>
        /// <returns>List of SEO warnings or empty list</returns>
        public static List<string> GetSeoWarnings(this IPublishedContent content)
        {
            var seoData = content?.GetSeoMetaModel();
            return seoData != null ? GetSeoWarningsFromData(seoData) : new List<string>();
        }

        /// <summary>
        /// Gets the SEO status CSS class for styling purposes
        /// </summary>
        /// <param name="content">The published content</param>
        /// <returns>CSS class representing SEO status</returns>
        public static string GetSeoStatusClass(this IPublishedContent content)
        {
            var seoData = content?.GetSeoMetaModel();
            if (seoData != null)
            {
                var warnings = GetSeoWarningsFromData(seoData);
                if (warnings.Count == 0) return "seo-excellent";
                if (warnings.Count <= 2) return "seo-good";
                return "seo-poor";
            }
            return "seo-unknown";
        }
    }
}

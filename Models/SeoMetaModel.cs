namespace GingerSquirrel.SeoPreview.Models
{
    public class SeoMetaModel
    {
        public string MetaTitle { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;

        /// <summary>
        /// Checks if the meta title is within the recommended length (50-60 characters)
        /// </summary>
        public bool IsMetaTitleOptimal => MetaTitle.Length >= 50 && MetaTitle.Length <= 60;

        /// <summary>
        /// Checks if the meta description is within the recommended length (120-155 characters)
        /// </summary>
        public bool IsMetaDescriptionOptimal => MetaDescription.Length >= 120 && MetaDescription.Length <= 155;
        
    }
}

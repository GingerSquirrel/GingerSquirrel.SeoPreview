export const manifests: Array<UmbExtensionManifest> = [
  {
    type: "propertyEditorUi",
    alias: "GingerSquirrel.PropertyEditorUi.SEOPreview",
    name: "SEO Preview Property Editor UI",
    element: () => import("../seo-preview-property-editor-ui.element.js"),
    meta: {
      label: "SEO Preview",
      icon: "icon-search",
      group: "common",
      propertyEditorSchemaAlias: "Umbraco.Plain.JSON"
    }
  }
];

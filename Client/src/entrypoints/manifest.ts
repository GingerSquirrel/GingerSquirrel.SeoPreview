export const manifests: Array<UmbExtensionManifest> = [
  {
    name: "Ginger Squirrel Seo Preview Entrypoint",
    alias: "GingerSquirrel.SeoPreview.Entrypoint",
    type: "backofficeEntryPoint",
    js: () => import("./entrypoint.js"),
  },
];

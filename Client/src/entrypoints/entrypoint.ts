import type {
  UmbEntryPointOnInit,
  UmbEntryPointOnUnload,
} from "@umbraco-cms/backoffice/extension-api";

// Import our property editor element
import '../seo-preview-property-editor-ui.element.js';

// load up the manifests here
export const onInit: UmbEntryPointOnInit = (_host, _extensionRegistry) => {
  console.log("SEO Preview extension loaded 🎉");
};

export const onUnload: UmbEntryPointOnUnload = (_host, _extensionRegistry) => {
  console.log("SEO Preview extension unloaded 👋");
};

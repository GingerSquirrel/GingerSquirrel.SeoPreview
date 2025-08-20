import { LitElement, html, css, customElement, property } from '@umbraco-cms/backoffice/external/lit';

interface SEOData {
    MetaTitle: string;
    MetaDescription: string;
}

@customElement('ginger-squirrel-seo-preview-property-editor-ui')
export default class GingerSquirrelSEOPreviewPropertyEditorUIElement extends LitElement {
    @property({ type: Object })
    public value: SEOData = {
        MetaTitle: '',
        MetaDescription: ''
    };

    // Ensure value is always properly initialized
    private get seoData(): SEOData {
        if (!this.value || typeof this.value !== 'object') {
            return {
                MetaTitle: '',
                MetaDescription: ''
            };
        }
        return {
            MetaTitle: this.value.MetaTitle || '',
            MetaDescription: this.value.MetaDescription || ''
        };
    }

    private _updateValue(seoData: SEOData) {
        this.value = seoData;
        this.requestUpdate();
        this._dispatchChangeEvent();
    }

    private _dispatchChangeEvent() {
        this.dispatchEvent(new CustomEvent('change'));
    }

    private _onTitleInput(e: InputEvent) {
        const MetaTitle = (e.target as HTMLInputElement).value;
        const currentData = this.seoData; // Use the safe getter
        this._updateValue({ ...currentData, MetaTitle });
    }

    private _onDescriptionInput(e: InputEvent) {
        const MetaDescription = (e.target as HTMLInputElement).value;
        const currentData = this.seoData; // Use the safe getter
        this._updateValue({ ...currentData, MetaDescription });
    }

    private _truncateText(text: string, maxLength: number): string {
        if (text.length <= maxLength) return text;
        return text.substring(0, maxLength) + '...';
    }

    private _getCharacterCount(text: string, maxLength: number): string {
        return `${text.length}/${maxLength}`;
    }

    private _getCharacterCountClass(text: string, maxLength: number): string {
        const length = text.length;
        if (length > maxLength) return 'over-limit';
        if (length > maxLength * 0.9) return 'near-limit';
        return 'under-limit';
    }

    override render() {
        const seoData = this.seoData; // Use the safe getter
        const truncatedTitle = this._truncateText(seoData.MetaTitle, 60);
        const truncatedDescription = this._truncateText(seoData.MetaDescription, 155);

        return html`
            <div class="seo-editor">
                <div class="input-section">
                    <div class="field-group">
                        <label for="meta-title">Meta Title</label>
                        <uui-input
                            id="meta-title"
                            class="title-input"
                            placeholder="Enter your page title..."
                            .value=${seoData.MetaTitle}
                            @input=${this._onTitleInput}
                        ></uui-input>
                        <div class="character-count ${this._getCharacterCountClass(seoData.MetaTitle, 60)}">
                            ${this._getCharacterCount(seoData.MetaTitle, 60)} characters (recommended: 50-60)
                        </div>
                    </div>

                    <div class="field-group">
                        <label for="meta-description">Meta Description</label>
                        <uui-textarea
                            id="meta-description"
                            class="description-input"
                            placeholder="Enter your page description..."
                            .value=${seoData.MetaDescription}
                            @input=${this._onDescriptionInput}
                            rows="3"
                        ></uui-textarea>
                        <div class="character-count ${this._getCharacterCountClass(seoData.MetaDescription, 155)}">
                            ${this._getCharacterCount(seoData.MetaDescription, 155)} characters (recommended: 120-155)
                        </div>
                    </div>
                </div>

                <div class="preview-section">
                    <h3>Google Search Preview</h3>
                    <div class="google-preview">
                        <div class="search-result">
                            <div class="url">https://yoursite.com/page-url</div>
                            <div class="title">
                                ${truncatedTitle || 'Your Page Title Will Appear Here'}
                            </div>
                            <div class="description">
                                ${truncatedDescription || 'Your meta description will appear here. This is what users will see in Google search results, so make it compelling and informative.'}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }

    static override readonly styles = [
        css`
            .seo-editor {
                display: flex;
                flex-direction: column;
                gap: 24px;
                max-width: 800px;
            }

            .input-section {
                display: flex;
                flex-direction: column;
                gap: 20px;
            }

            .field-group {
                display: flex;
                flex-direction: column;
                gap: 8px;
            }

            label {
                font-weight: 600;
                color: #333;
                font-size: 14px;
            }

            .title-input,
            .description-input {
                width: 100%;
                font-size: 14px;
            }

            .character-count {
                font-size: 12px;
                text-align: right;
                margin-top: 4px;
            }

            .character-count.under-limit {
                color: #52c41a;
            }

            .character-count.near-limit {
                color: #faad14;
            }

            .character-count.over-limit {
                color: #ff4d4f;
                font-weight: 600;
            }

            .preview-section {
                border-top: 1px solid #e0e0e0;
                padding-top: 20px;
            }

            .preview-section h3 {
                margin: 0 0 16px 0;
                font-size: 16px;
                font-weight: 600;
                color: #333;
            }

            .google-preview {
                background: #fff;
                border: 1px solid #dadce0;
                border-radius: 8px;
                padding: 16px;
                font-family: arial, sans-serif;
            }

            .search-result {
                max-width: 600px;
            }

            .url {
                color: #1a0dab;
                font-size: 14px;
                line-height: 1.3;
                margin-bottom: 2px;
            }

            .title {
                color: #1a0dab;
                font-size: 20px;
                line-height: 1.3;
                margin-bottom: 4px;
                cursor: pointer;
                font-weight: normal;
                text-decoration: underline;
                text-decoration-color: transparent;
                transition: text-decoration-color 0.2s;
            }

            .title:hover {
                text-decoration-color: #1a0dab;
            }

            .description {
                color: #4d5156;
                font-size: 14px;
                line-height: 1.4;
                max-width: 600px;
            }

            @media (min-width: 1100px) {
                .seo-editor {
                    flex-direction: row;
                    gap: 32px;
                }

                .input-section {
                    flex: 1;
                }

                .preview-section {
                    flex: 1;
                    border-top: none;
                    border-left: 1px solid #e0e0e0;
                    padding-top: 0;
                    padding-left: 32px;
                }
            }
        `,
    ];
}

declare global {
    interface HTMLElementTagNameMap {
        'ginger-squirrel-seo-preview-property-editor-ui': GingerSquirrelSEOPreviewPropertyEditorUIElement;
    }
}

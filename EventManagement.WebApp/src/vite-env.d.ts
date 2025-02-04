/// <reference types="vite/client" />

interface ImportMetaEnv {
    readonly VITE_AZURE_AD_CLIENT_ID: string;
    readonly VITE_AZURE_AD_TENANT_ID: string;
    // Add more environment variables as needed
}

interface ImportMeta {
    readonly env: ImportMetaEnv;
}
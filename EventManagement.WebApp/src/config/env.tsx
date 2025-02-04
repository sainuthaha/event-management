export const ENVIRONMENT = {
  DEV: import.meta.env.DEV,
  CLIENT_ID: import.meta.env.VITE_AZURE_AD_CLIENT_ID,
  TENANT_ID: import.meta.env.VITE_AZURE_AD_TENANT_ID,
};

console.log('Environment Variables:', ENVIRONMENT);
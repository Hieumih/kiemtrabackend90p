import { defineAsyncComponent } from 'vue'

export const LoadingComponent = defineAsyncComponent(() => import('./LoadingComponent.vue'))
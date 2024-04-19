<template>
    <router-view v-slot="{ Component }">
        <template v-if="Component">
            <suspense>
                <component :is="Component" />
                <template #fallback>
                    <Loading_Page />
                </template>
            </suspense>
        </template>
    </router-view>
    <div v-if="dev" class="dev">
        <p>IN DEVELOPMENT NOT PRODUCTION</p>
        <button @click="logout" class="me-2">logout</button>
    </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { Loading_Page } from '@/components/elements/index'
import { login, check_login, logout } from '~common/ajaxServer.js';

const dev = ref(import.meta.env.DEV)



</script>

<style scoped>
* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}

.dev {
    position: fixed;
    bottom: 0;
    right: 0;
    opacity: 0.5;
    font-size: 1.5rem;
    z-index: 100000000000000;
    display: flex;
    flex-direction: row-reverse;
}

.div p {
    pointer-events: none;
}
</style>
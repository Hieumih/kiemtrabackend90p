<template>
    <LeftSideBar :isClosed="isClosed" :url_link_icon_child="url_paths" />
    <HomeSection :isClosed="isClosed" @closeSidebar="closeSidebar">
        <router-view v-slot="{ Component, route }">
            <Transition name="page-fade" mode="out-in" :enter-active-class="route.meta.enterActiveClass"
                :leave-active-class="route.meta.leaveActiveClass">
                <component :is="Component" />
            </Transition>
        </router-view>
    </HomeSection>
    <Toast_Container ref="toastContainerRef" />
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';

import { LeftSideBar, HomeSection, Toast_Container } from "@/components/elements/index.js";
import { url_paths } from "~common/clientUrl";
import { EventBus } from '~common/common'
import { devKeyCode } from '~common/config'

localStorage.getItem('isClosed') ? null : localStorage.setItem('isClosed', false);

let isClosed = ref(localStorage.getItem('isClosed') === 'true' ? true : false);

const isDev = ref(import.meta.env.DEV);

const toastContainerRef = ref(null);



const closeSidebar = () => {
    isClosed.value = !isClosed.value;
};

watch(isClosed, (value) => {
    localStorage.setItem('isClosed', value);
});

onMounted(() => {
    
});

window.addEventListener('keydown', (e) => {
    if ((e.key === devKeyCode) && isDev.value) {
        // wierd stuff
        EventBus.emit('addNotification', { message: 'Dev mode enabled enjoy!', toast_type: 'info' });
    }
});

</script>


<style scoped>
.page-fade-enter-active,
.page-fade-leave-active {
    /* transition: 600ms ease all; */
    transition: opacity 0.5s ease-out;
}

.page-fade-enter,
.page-fade-leave-to {
    opacity: 0;
}
</style>
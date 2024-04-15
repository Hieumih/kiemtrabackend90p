<template>
    <Toast_Container ref="toastContainerRef"/>
    <LeftSideBar :isClosed="isClosed" :url_link_icon_child="url_paths"/>
    <HomeSection :isClosed="isClosed" @closeSidebar="closeSidebar">
        <router-view />
    </HomeSection>
</template>

<script setup>
import { ref, onMounted, watch, nextTick } from 'vue';
import { LeftSideBar, HomeSection, Toast_Container } from "@/components/elements";
import { url_paths } from "@/clientUrl";
import { EventBus } from '@/common'
import { devKeyCode } from '@/config'

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
    console.log('Router_Home mounted');
});

window.addEventListener('keydown', (e) => {
    if ((e.key === devKeyCode) && isDev.value) {
        // wierd stuff
        EventBus.emit('addNotification', { message: 'Dev mode enabled enjoy!', toast_type: 'info' });
    }
});

</script>
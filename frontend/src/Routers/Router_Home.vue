<template>
    <div class="home">
        <LeftSideBar :isClosed="isClosed" :url_link_icon_child="url_paths">
            <!-- <template #logo_img>
            <img :src="`./img/sidebarLogo.png`" alt="">
        </template> -->
            <template #logo_name>
                <p style="margin: 0;">TEST APP</p>
            </template>
        </LeftSideBar>
        <HomeSection :isClosed="isClosed" @closeSidebar="closeSidebar">
            <router-view v-slot="{ Component, route }">
                <Transition name="page-fade" mode="out-in">
                    <component :is="Component" />
                </Transition>
            </router-view>
        </HomeSection>
        <Toast_Container ref="toastContainerRef" />
    </div>
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


<style scoped lang="scss">

.page-fade-enter-active,
.page-fade-leave-active {
    /* transition: 600ms ease all; */
    transition: opacity 0.5s ease-out;
}

.page-fade-enter,
.page-fade-leave-to {
    opacity: 0;
}

// * {

// }

// div.home {
//     .home-section {
//         --home-section-bg: rgb(77, 76, 94);
//         --home-section-text: white;
//         --home-section-main-bg: rgb(43, 41, 63);
//     }

//     --easy-table-border: none;
//     --easy-table-row-border: solid 1px #e0e0e0;
//     --easy-table-header-font-color: #fff;
//     --easy-table-header-background-color: #000;
//     --easy-table-body-row-font-color: #fff;
//     --easy-table-body-row-background-color: #000;
//     --easy-table-body-row-hover-font-color: #000;
//     --easy-table-body-row-hover-background-color: #fff;
//     --easy-table-body-even-row-font-color: #212121;
//     --easy-table-body-even-row-background-color: #fff;
//     --easy-table-footer-background-color: #000;
//     --easy-table-footer-font-color: #fff;
//     --easy-table-rows-per-page-selector-width: auto;
//     --easy-table-rows-per-page-selector-option-padding: 5px;
//     --easy-table-rows-per-page-selector-z-index: auto;
//     --easy-table-message-font-color: #212121;
//     --easy-table-loading-mask-background-color: #000;
//     --easy-table-scrollbar-track-color: #000;
//     --easy-table-scrollbar-color: #000;
//     --easy-table-scrollbar-thumb-color: #fff;
//     --easy-table-scrollbar-corner-color: #000;
//     --easy-table-buttons-pagination-border: 1px solid #e0e0e0;
// }

</style>
<template>
    <LeftSideBar :isClosed="isClosed" :url_link_icon_child="url_paths"/>
    <HomeSection :isClosed="isClosed" @closeSidebar="closeSidebar">
        <router-view />
    </HomeSection>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { LeftSideBar, HomeSection } from "@/components/elements";
import { url_paths } from "../clientUrl";

let isClosed = ref(false);
let leftSideBar = ref(null);

const closeSidebar = () => {
    isClosed.value = !isClosed.value;
    //leftSideBar.value.closeSidebar();
};

onMounted(() => {
    leftSideBar = ref(document.querySelector('LeftSideBar'));
});
</script>
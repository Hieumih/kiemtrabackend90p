<template>
    <section :class="{ 'home-section': true, 'close': isClosed }" ref="homesection">
        <div class="home-content">
            <i @click="closeSidebar" class="bi bi-list menu"></i>
            <span class="text" ref="path">
                <!-- modify this -->
                <template v-if="routerLinks.length !== 0">
                    <template v-for="(item, index) in routerLinks" :key="index">
                        <router-link :to="item.link" @click="goToPath(item)" class="link">{{ item.text }}</router-link>
                        <!-- Add a separator if it's not the last item -->
                        <span v-if="index < routerLinks.length - 1"> / </span>
                    </template>
                </template>
                <template v-else>
                    <router-link to="/dashboard" class="link">Dashboard</router-link>
                </template>
            </span>
        </div>
        <div class="container-custom">
            <main>
                <slot></slot>
            </main>
        </div>
    </section>
</template>

<script setup>
import { ref, defineEmits, withDefaults } from 'vue';

const props = defineProps({
    isClosed: {
        type: Boolean,
        default: false
    },
    isClosed: {
        type: Boolean,
        default: false
    }
});

const emit = defineEmits(['closeSidebar']);

const routerLinks = ref([]); // Initialize routerLinks array

const closeSidebar = () => {
    emit('closeSidebar');
};

const modifyPath = (items) => {
    console.log(items);
    routerLinks.value = items;
};

const goToPath = (item) => {
    routerLinks.value = routerLinks.value.slice(0, routerLinks.value.indexOf(item) + 1);
};
</script>

<style scoped>
.home-section {
    position: relative;
    background: #e4e9f7;
    height: 100vh;
    left: 260px;
    width: calc(100% - 260px);
    /* left: 78px;
        width: calc(100% - 78px); */
    transition: all 0.5s ease;
}

.home-section.close {
    left: 78px;
    width: calc(100% - 78px);
}

.home-section .home-content {
    height: 60px;
    display: flex;
    align-items: center;
}

.home-section .home-content .menu,
.home-section .home-content .text {
    color: #11101d;
    font-size: 35px;
    /* transition: all 0.5s ease; */
}

.home-section .home-content .menu {
    margin: 0 15px;
    cursor: pointer;
    /* width: 50px;
    height: 50px; */
    display: flex;
    align-items: center;
    justify-content: center;
    transition: all 0.5s ease;
}

.home-section .home-content .menu:hover {
    opacity: 0.4;
}

.home-section .home-content .text .link {
    transition: all 0.3s ease;
}

.home-section .home-content .text .link:hover {
    opacity: 0.4;
}

.home-section .home-content .text {
    font-size: 26px;
    font-weight: 600;
}

.home-section .home-content .text a {
    color: #11101d;
    text-decoration: none;
}

.container-custom {
    width: 100%;
    height: calc(100% - 60px);
    /* background: #000; */
    display: flex;
    justify-content: center;
    align-items: center;
}

.container-custom main {
    background: #fff;
    width: calc(100% - 10px);
    height: 100%;
    margin-bottom: 10px;
    border-radius: 6px;
    overflow-y: auto;
}
</style>
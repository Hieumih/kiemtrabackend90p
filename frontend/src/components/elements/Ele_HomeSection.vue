<template>
    <section :class="{ 'home-section': true, 'close': isClosed }" ref="homesection">
        <div class="home-content">
            <i @click="closeSidebar" class="bi bi-list menu"></i>
            <span class="text" ref="path">
                Dashboard
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
import { ref, defineEmits, withDefaults, watch } from 'vue';
import { useRoute } from 'vue-router';

const props = defineProps({
    isClosed: {
        type: Boolean,
        default: false
    },
});

const emit = defineEmits(['closeSidebar']);

const closeSidebar = () => {
    emit('closeSidebar');
};

const paths = ref([]);

const route = useRoute();

watch(() => route.path, (path) => {
    console.log(route);
});

</script>

<style scoped lang="scss">



.home-section {
    --home-section-bg: #e4e9f7;
    --home-section-text: #11101d;
    --home-section-main-bg: #fff;
    position: relative;
    background: var(--home-section-bg);
    height: 100vh;
    left: 260px;
    width: calc(100% - 260px);
    //left: 78px;
    //width: calc(100% - 78px);
    transition: all 0.5s ease;

    .home-content {
        height: 60px;
        display: flex;
        align-items: center;

        .menu,
        .text {
            color: var(--home-section-text);
            font-size: 35px;
            // transition: all 0.5s ease; 
        }

        .menu {
            margin: 0 15px;
            cursor: pointer;
            // width: 50px;
            // height: 50px;
            display: flex;
            align-items: center;
            justify-content: center;
            transition: all 0.5s ease;


        }

        .menu:hover {
            opacity: 0.4;
        }

        .text {
            font-size: 26px;
            font-weight: 600;

            .link {
                transition: all 0.3s ease;
            }

            .link:hover {
                opacity: 0.4;
            }

            a {
                color: var(--home-section-text);
                text-decoration: none;
            }
        }
    }

    &.close {
        left: 78px;
        width: calc(100% - 78px);

    }

    .container-custom {
        width: 100%;
        height: calc(100% - 60px);
        /* background: #000; */
        display: flex;
        justify-content: center;
        align-items: center;

        main {
            background: var(--home-section-main-bg);
            width: calc(100% - 10px);
            height: 100%;
            margin-bottom: 10px;
            border-radius: 6px;
            overflow-y: auto;
        }
    }

}
</style>
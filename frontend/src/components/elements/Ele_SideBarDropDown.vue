<template>
    <!-- <li :class="{ 'close': isClosed }" ref="menu">
        <div class="icon-link">
            <router-link :to="link" @click="modifyPath(name)">
                <i :class="icon"></i>
                <span class="link-name">{{ name }}</span>
            </router-link>
            <template v-if="items">
                <i class="bi bi-caret-down-fill arrow" ref="arrow"></i>
            </template>
        </div>
        <ul :class="'sub-menu' + (items ? ' ' : '')">
            <li>
                <router-link class="link-name" :to="link"  @click="modifyPath(name)">{{ name }}</router-link>
                <template v-for="(item, index) in items" :key="index">
                    <router-link :to="link + item.link" @click="modifyPath(item)">
                        {{ item.text }}
                    </router-link>
                </template>
            </li>
        </ul>
    </li> -->

    <li :class="{ 'close': isClosed }" ref="menu">
        <div class="icon-link">
            <router-link :to="url">
                <i :class="icon"></i>
                <span class="link-name">{{ title }}</span>
            </router-link>
            <template v-if="child">
                <i class="bi bi-caret-down-fill arrow" ref="arrow"></i>
            </template>
        </div>
        <ul :class="'sub-menu' + (child ? ' ' : '')">
            <li>
                <router-link class="link-name" :to="url" >{{ title }}</router-link>
                <template v-for="(item, index) in child" :key="index">
                    <router-link :to="url + item.url">
                        <!-- <i :class="item?.icon ?? ''"></i> -->
                        {{ item.title }}
                    </router-link>
                </template>
            </li>
        </ul>
    </li>
</template>

<script setup>
import { ref, onMounted } from 'vue';

const props = defineProps({
    child: {
        type: Array,
        default: null
    },
    title: {
        type: String,
        required: true
    },
    url: {
        type: String,
        required: true
    },
    icon: {
        type: String,
        required: true
    },
    isClosed: {
        type: Boolean,
        default: false
    }
});

const emit = defineEmits(['modifyPath']);

const menu = ref(null);
const arrow = ref(null);

const closeSidebar = () => {
    console.log('Child method called from parent');
    menu.value.classList.toggle('close');
};

onMounted(() => {
    console.log(props.child);
    if (arrow.value) {
        arrow.value.addEventListener('click', (e) => {
            arrow.value.parentElement.parentElement.classList.toggle('show-menu');
        });
    }
});
</script>

<style scoped>
li {
    position: relative;
    list-style: none;
    transition: all 0.4s ease;
    /* height: 50px;
    width: 100%;
    margin-bottom: 10px; */
}

li:hover {
    background: #1b1b31;
}

li .icon-link {
    display: flex;
    /* align-items: center; */
    align-items: baseline;
    justify-content: space-between;
    /* padding: 15px 20px;
    cursor: pointer; */
}

li.close .icon-link {
    display: block;
}

li i {
    height: 50px;
    min-width: 78px;
    text-align: center;
    line-height: 50px;
    color: #fff;
    font-size: 20px;
    cursor: pointer;
    transition: all 0.3s ease;
}

li i.arrow {
    height: 50px;
    width: 50px;
    /* Thêm width cho arrow */
    text-align: center;
    line-height: 50px;
    color: #fff;
    font-size: 20px;
    transition: all 0.3s ease;
    transform-origin: center center;
    /* Đặt trung tâm quay tại trung tâm của arrow */
}

li.show-menu i.arrow {
    transform: rotate(-180deg);
}

li.close i.arrow {
    display: none;
}

li a {
    display: flex;
    text-decoration: none;
    align-items: center;
}

li a .link-name {
    font-size: 18px;
    font-weight: 400;
    color: #fff;
}

li.close a .link-name {
    opacity: 0;
    pointer-events: none;
    white-space: nowrap;
}

li .sub-menu {
    padding: 6px 6px 14px 80px;
    margin-top: -10px;
    background: #1b1b31;
    display: none;
}

li.close .sub-menu {
    position: absolute;
    left: 100%;
    top: -10px;
    margin-top: 0;
    padding: 10px 20px;
    border-radius: 0 6px 6px 0;
    transition: all 0.4s ease;
    opacity: 0;
    display: block;
    pointer-events: none;
    transition: 0s;
}

li.show-menu .sub-menu {
    display: block;
}

li .sub-menu a {
    color: #fff;
    font-size: 15px;
    padding: 5px 0;
    white-space: nowrap;
    opacity: 0.6;
    transition: all 0.3s ease;
}

li .sub-menu a:hover {
    opacity: 1;
}


li.close:hover .sub-menu {
    top: 0px;
    opacity: 1;
    pointer-events: auto;
    transition: all 0.4s ease;
}

/* li.close:hover .icon-link a .link-name {
    font-size: 18px;
} */

li .sub-menu .link-name {
    display: none;
}

li.close .sub-menu .link-name {
    font-size: 18px;
    opacity: 1;
    display: block;
}

li .sub-menu.blank {
    opacity: 1;
    pointer-events: auto;
    padding: 3px 20px 6px 16px;
    opacity: 0;
    pointer-events: none;

}

li:hover .sub-menu.blank {
    top: 50%;
    transform: translateY(-50%);
}
</style>
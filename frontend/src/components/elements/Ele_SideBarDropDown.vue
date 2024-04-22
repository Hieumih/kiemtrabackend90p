<template>
    <li :class="{ 'close': isClosed }" ref="menu">
        <div class="icon-link">
            <router-link :to="url" :class="{ 'link': child.length != 0 }">
                <i :class="icon" class=""></i>
                <span class="link-name" ref="linkNameRef">
                    <div class="text" ref="textRef">{{ title }}</div>
                </span>
            </router-link>
            <template v-if="child.length != 0">
                <i class="bi bi-caret-down-fill arrow" ref="arrow"></i>
            </template>
        </div>
        <ul :class="'sub-menu'">
            <li>
                <router-link class="link-name" :to="url">{{ title }}</router-link>
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

const linkNameRef = ref(null);
const textRef = ref(null);

onMounted(() => {
    import.meta.env.DEV && console.log(props.child);
    if (arrow.value) {
        arrow.value.addEventListener('click', (e) => {
            arrow.value.parentElement.parentElement.classList.toggle('show-menu');
        });
    }
    // get size of linkName and ref
    const linkNameWidth = linkNameRef.value.offsetWidth;
    const textWidth = textRef.value.offsetWidth;
    // console.log(linkNameWidth, textWidth, props.title);
    if (textWidth > linkNameWidth) {
        linkNameRef.value.classList.add('long');
    }
});
</script>

<style scoped lang="scss">
$submenu-hoverli-color: #1b1b31;
$font-color: #fff;


li {
    position: relative;
    list-style: none;
    transition: all 0.4s ease;

    /* height: 50px;
    width: 100%;
    margin-bottom: 10px; */
    .icon-link {
        display: flex;
        /* align-items: center; */
        align-items: baseline;
        justify-content: space-between;

        /* padding: 15px 20px;
    cursor: pointer; */
        .link {
            width: 70.5%;
            flex: 0 0 auto;
        }
    }

    i {
        height: 50px;
        min-width: 78px;
        text-align: center;
        line-height: 50px;
        color: $font-color;
        font-size: 20px;
        cursor: pointer;
        transition: all 0.3s ease;
    }

    i.arrow {
        height: 50px;
        width: 50px;
        /* Thêm width cho arrow */
        text-align: center;
        line-height: 50px;
        color: $font-color;
        font-size: 20px;
        transition: all 0.3s ease;
        transform-origin: center center;
        /* Đặt trung tâm quay tại trung tâm của arrow */
    }

    a {
        display: flex;
        text-decoration: none;
        align-items: center;

        .link-name {
            font-size: 18px;
            font-weight: 400;
            color: $font-color;
            overflow: hidden;
            white-space: nowrap;

            .text {
                display: inline-block;
                white-space: nowrap;
                overflow-x: visible;
            }
        }

        .link-name.long:hover .text {
            animation: move_text 5s linear infinite;
        }
    }

    .sub-menu {
        padding: 6px 6px 14px 80px;
        margin-top: -10px;
        background: $submenu-hoverli-color;
        display: none;

        a {
            color: $font-color;
            font-size: 15px;
            padding: 5px 0;
            white-space: nowrap;
            opacity: 0.6;
            transition: all 0.3s ease;
        }

        a:hover {
            opacity: 1;
        }

        .link-name {
            display: none;
        }
    }

    .sub-menu.blank {
        opacity: 1;
        pointer-events: auto;
        padding: 3px 20px 6px 16px;
        opacity: 0;
        pointer-events: none;

    }

}

li:hover {
    background: $submenu-hoverli-color;

    .sub-menu.blank {
        top: 50%;
        transform: translateY(-50%);
    }
}

li.close {
    .icon-link {
        display: block;
    }

    i.arrow {
        display: none;
    }

    a {
        .link-name {
            /* opacity: 0; */
            pointer-events: none;
        }
    }

    .sub-menu {
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

        .link-name {
            font-size: 18px;
            opacity: 1;
            display: block;
        }
    }
}

li.show-menu {
    i.arrow {
        transform: rotate(-180deg);
    }

    .sub-menu {
        display: block;
    }
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


@keyframes move_text {
    0% {
        transform: translateX(0);
    }

    100% {
        transform: translateX(-100%);
    }
}

@keyframes container-down {
    0% {
        opacity: 0;
        transform: translateY(-10px);
    }

    100% {
        opacity: 1;
        transform: translateY(0);
    }

}
</style>
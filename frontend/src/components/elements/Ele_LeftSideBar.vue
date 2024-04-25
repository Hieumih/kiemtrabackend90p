<template>
    <nav :class="{ 'sidebar': true, 'close': isClosed }" ref="sidebar">
        <div class="logo-details">
            <slot name="logo"></slot>

            <template v-if="!$slots.logo">

                <div class="logo-image">
                    <slot name="logo_img"></slot>
                    <!-- <img src="https://cdn-icons-png.flaticon.com/512/25/25231.png" alt="logo" /> -->
                    <i class="fa-solid fa-users-gear" v-if="!$slots.logo_img"></i>
                </div>

                <span class="logo-name">
                    <slot name="logo_name"></slot>
                    <template v-if="!$slots.logo_name">CRM APP</template>
                </span>

            </template>
        </div>


        <ul class="nav-links">

            <template v-for="(item, index) in url_link_icon_child" :key="index">
                <DropDown id="dropdown" :url="item.url" :title="item.title" :icon="item.icon" :child="item.child"
                    :isClosed="isClosed" />
            </template>


            <!-- #region profile -->
            <li>
                <div class="profile-details">
                    <div class="img-name-role">
                        <div class="profile-content">
                            <slot name="userImage"></slot>
                            <template v-if="!$slots.userImage">
                                <img :src="imgsrc" alt="Missing_ProfileImg">
                            </template>
                        </div>
                        <div class="name-role">
                            <div class="profile-name"> {{ profile_name }}</div>
                            <div class="role">{{ role }}</div>
                        </div>
                    </div>
                    <i class="bi bi-box-arrow-right" @click="logout"></i>
                </div>
            </li>
            <!-- endregion profile -->

            <br><br><br><br><br><br>
        </ul>

    </nav>
</template>

<script setup>
import { ref, defineProps, defineEmits, onMounted, defineAsyncComponent } from 'vue';
import 'bootstrap-icons/font/bootstrap-icons.css';
// import DropDown from './Ele_SideBarDropDown.vue';
const DropDown = defineAsyncComponent(() => import('./Ele_SideBarDropDown.vue'));

const props = defineProps({
    imgsrc: {
        type: String,
        default: 'img/user.jpg'
    },
    profile_name: {
        type: String,
        default: 'Test'
    },
    role: {
        type: String,
        default: 'Test'
    },
    url_link_icon_child: {
        type: Array,
        required: true,
    },
    isClosed: {
        type: Boolean,
        default: false
    }
});

const emit = defineEmits(['modifyPath', 'logout']);

let user_photo = ref(null);

const logout = () => {
    emit('logout');
};

onMounted(() => {
    import.meta.env.DEV && console.log(props.url_link_icon_child);
});
</script>

<style scoped lang="scss">
$sidebar-color: #10375C;
$profile-details-color: #1d1b31;

.sidebar {
    position: fixed;
    top: 0;
    left: 0;
    height: 100%;
    width: 260px;
    background: $sidebar-color;
    z-index: 100;
    transition: all 0.5s ease;

    .logo-details {
        height: 60px;
        width: 100%;
        display: flex;
        align-items: center;

        /* padding-right: 30px; */
        .logo-image {
            border-radius: 50%;
            /* margin-left: 10px; */
            /* background: red; */
            /* margin: auto; */
            /* min-width: 78px; */
            height: 50px;
            min-width: 78px;
            line-height: 50px;
            display: flex;
            align-items: center;
            justify-content: center;

            i {
                font-size: 30px;
                color: #fff;
            }
        }

        .logo-image:deep() img {
            width: 30px;
            height: 30px;
        }

        .logo-name {
            font-size: 22px;
            color: #fff;
            font-weight: 600;
            transition: 0.3s ease;
            transition-delay: 0.1s;
            white-space: nowrap;
            opacity: 1;
        }
    }

    .nav-links {
        height: 100%;
        padding-top: 30px;
        padding-inline-start: 0;
        overflow: auto;

        ::-webkit-scrollbar {
            display: none;
        }

        li {
            position: relative;
            list-style: none;
            transition: all 0.4s ease;

            //height: 50px;
            //width: 100%;
            //margin-bottom: 10px;
            i {
                height: 50px;
                min-width: 78px;
                text-align: center;
                line-height: 50px;
                color: #fff;
                font-size: 20px;
                cursor: pointer;
                transition: all 0.3s ease;
            }

            a {
                display: flex;
                text-decoration: none;
                align-items: center;
            }
        }

        li:hover {
            background: #1b1b31;
        }
    }

    .profile-details {
        position: fixed;
        bottom: 0;
        display: flex;
        align-items: center;
        justify-content: space-between;
        background: $profile-details-color;
        width: 260px;
        padding: 6px 0;
        transition: all 0.5s ease;

        .img-name-role {
            display: flex;
        }

        img {
            height: 52px;
            width: 52px;
            object-fit: cover;
            border-radius: 16px;
            margin: 0 14px 0 12px;
            background: $profile-details-color;
            transition: all 0.5s ease;
        }

        .profile-name,
        .role {
            color: #fff;
            font-size: 18px;
            font-weight: 500;
            white-space: nowrap;
        }

        .role {
            font-size: 15px;
        }
    }
}

.sidebar.close {
    width: 78px;

    .logo-details {
        .logo-name {
            transition-delay: 0s;
            opacity: 0;
            pointer-events: none;
        }
    }

    .nav-links {
        overflow: visible;
    }

    .profile-details {
        width: 78px;
        background: none;

        img {
            padding: 10px;
        }

        i,
        .profile-name,
        .role {
            display: none;
        }
    }
}

</style>
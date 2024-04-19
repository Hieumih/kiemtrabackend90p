<template>
    <nav :class="{ 'sidebar': true, 'close': isClosed }" ref="sidebar">
        <div class="logo-details">
            <div class="logo-image">
                <!-- <img src="https://cdn-icons-png.flaticon.com/512/25/25231.png" alt="logo" /> -->
                <i class="fa-solid fa-users-gear"></i>
            </div>
            
            <span class="logo-name">CRM APP</span>
        </div>


        <ul class="nav-links">

            <template v-for="(item, index) in url_link_icon_child" :key="index">
                <DropDown id="dropdown" :url="item.url" :title="item.title" :icon="item.icon" :child="item.child" :isClosed="isClosed" />
            </template>


            <!-- #region profile -->
            <li>
                <div class="profile-details">
                    <div class="img-name-role">
                        <div class="profile-content">
                            <img :src="imgsrc" alt="Missing_ProfileImg">
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
        default: '/img/user.jpg'
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

<style scoped>
.sidebar {
    position: fixed;
    top: 0;
    left: 0;
    height: 100%;
    width: 260px;
    background: #11101d;
    z-index: 100;
    transition: all 0.5s ease;
}

.sidebar.close {
    width: 78px;
}

/*#region logo-details*/

.sidebar .logo-details {
    height: 60px;
    width: 100%;
    display: flex;
    align-items: center;
    /* padding-right: 30px; */
}

.sidebar .logo-details .logo-image {
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
    /*  */
}

.sidebar .logo-details .logo-image img {
    width: 30px;
    height: 30px;
}

.sidebar .logo-details .logo-image i {
    font-size: 30px;
    color: #fff;
}

.sidebar .logo-details .logo-name {
    font-size: 22px;
    color: #fff;
    font-weight: 600;
    transition: 0.3s ease;
    transition-delay: 0.1s;
    white-space: nowrap;
    opacity: 1;
}

.sidebar.close .logo-details .logo-name {
    transition-delay: 0s;
    opacity: 0;
    pointer-events: none;
}

/*#endregion logo-details*/

/*#region nav-links*/
.sidebar .nav-links {
    height: 100%;
    padding-top: 30px;
    padding-inline-start: 0;
    overflow: auto;
}

.sidebar.close .nav-links {
    overflow: visible;
}

.sidebar .nav-links::-webkit-scrollbar {
    display: none;
}

.sidebar .nav-links li {
    position: relative;
    list-style: none;
    transition: all 0.4s ease;
    /* height: 50px;
    width: 100%;
    margin-bottom: 10px; */
}

.sidebar .nav-links li:hover {
    background: #1b1b31;
}


.sidebar .nav-links li i {
    height: 50px;
    min-width: 78px;
    text-align: center;
    line-height: 50px;
    color: #fff;
    font-size: 20px;
    cursor: pointer;
    transition: all 0.3s ease;
}

.sidebar .nav-links li a {
    display: flex;
    text-decoration: none;
    align-items: center;
}


/*#endregion nav-links*/

/*#region profile-detail*/
.sidebar .profile-details {
    position: fixed;
    bottom: 0;
    display: flex;
    align-items: center;
    justify-content: space-between;
    background: #1d1b31;
    width: 260px;
    padding: 6px 0;
    transition: all 0.5s ease;
}

.sidebar.close .profile-details {
    width: 78px;
    background: none;
}

.sidebar .profile-details .img-name-role {
    display: flex;
}

.sidebar .profile-details img {
    height: 52px;
    width: 52px;
    object-fit: cover;
    border-radius: 16px;
    margin: 0 14px 0 12px;
    background: #1d1b31;
    transition: all 0.5s ease;
}

.sidebar.close .profile-details img {
    padding: 10px;
}

.sidebar .profile-details .profile-name,
.sidebar .profile-details .role {
    color: #fff;
    font-size: 18px;
    font-weight: 500;
    white-space: nowrap;
}

.sidebar.close .profile-details i,
.sidebar.close .profile-details .profile-name,
.sidebar.close .profile-details .role {
    display: none;
}

.sidebar .profile-details .role {
    font-size: 15px;
}

/*#endregion */
</style>
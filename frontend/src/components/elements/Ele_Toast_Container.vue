<template>
    <transition-group tag="ul" name="list">
        <template v-for="(notification, index) in notifications" :key="notification.id">
            <Toast_Notification :message="notification.message" :toast_type="notification.toast_type"
                @close="notification.close" />
        </template>
    </transition-group>
</template>

<script setup>
import { ref, onMounted, watch, defineProps, defineExpose, getCurrentInstance } from 'vue';
import Toast_Notification from './Ele_Toast_Notification.vue';
import { EventBus } from '~common/common'
import { devKeyCode } from '~common/config'



const notifications = ref([]);


// notifications.value.push({
//     id: Date.now(),
//     message: 'Welcome to the app! 🎉',
//     toast_type: 'info'
// });

const addNotification = (message, toast_type) => {
    const newNotification = {
        id: Date.now(), // Unique ID for each notification
        message,
        toast_type,
        close: () => removeNotification(newNotification.id)
    };
    notifications.value.push(newNotification);
    // Remove the notification after 5 seconds
    setTimeout(() => {
        removeNotification(newNotification.id);
    }, 5000);
};

const removeNotification = (id) => {
    const index = notifications.value.findIndex(notification => notification.id === id);
    if (index !== -1) {
        notifications.value.splice(index, 1);
    }
};

defineExpose({
    addNotification,
    removeNotification
});

onMounted(() => {
    EventBus.on('addNotification', (payload) => {
        addNotification(payload.message, payload.toast_type);
    });
});


</script>

<style scoped>
ul {
    list-style: none;
    position: fixed;
    bottom: 20px;
    right: 20px;
    z-index: 100;
    margin: 0;
    padding: 0;
    width: 300px;
    pointer-events: none;
}

.list-move {
    transition: ease 0.5s all;
}

.list-enter-from {
    opacity: 0;
    transform: translateY(30px);
}
.list-enter-to {
    opacity: 1;
    transform: translateY(0);
}
.list-enter-active {
    transition: ease 0.5s all;
}

.list-leave-from {
    opacity: 1;
    transform: translateY(0);
}

.list-leave-to {
    opacity: 0;
    transform: translateY(30px);
}

.list-leave-active {
    transition: ease 0.5s all;
    position: absolute;
}
</style>
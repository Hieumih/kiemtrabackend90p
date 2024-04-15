<template>
    <div v-if="showDebugButton" class="testBtn">
        <button @click="addNotificationTest">Test toast</button>
    </div>
    <ul>
        <template v-for="(notification, index) in notifications" :key="notification.id">
            <Toast_Notification :message="notification.message" :toast_type="notification.toast_type" />
        </template>
    </ul>
</template>

<script setup>
import { ref, onMounted, watch, defineProps, defineExpose, getCurrentInstance } from 'vue';
import Toast_Notification from './Ele_Toast_Notification.vue';
import { EventBus } from '@/common'
import { devKeyCode } from '@/config'

const isDev = ref(import.meta.env.DEV);
const showDebugButton = ref(false);

const notifications = ref([]);

const addNotificationTest = () => {
    const newNotification = {
        id: Date.now(), // Unique ID for each notification
        message: 'This is a test notification! 😍',
        toast_type: 'success'
    };
    notifications.value.push(newNotification);
    // Remove the notification after 5 seconds
    setTimeout(() => {
        removeNotification(newNotification.id);
    }, 5000);
};

const addNotification = (message, toast_type) => {
    const newNotification = {
        id: Date.now(), // Unique ID for each notification
        message,
        toast_type
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

if (isDev.value) {
    window.addEventListener('keydown', (e) => {
        if (e.key === devKeyCode) {
            showDebugButton.value = !showDebugButton.value;
        }
    });
}

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
}

.testBtn {
    position: fixed;
    bottom: 20px;
    right: 20px;
    z-index: 1000000000;
}
</style>
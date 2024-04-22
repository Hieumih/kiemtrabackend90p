<template>
    <li :class="{ 'toast-notification': true, 'mb-2': true }" ref="toastRef">
        <div class="toast-notification__container">
            <div class="toast-notification__content">
                <div :class="{
        'toast-notification__content__icon': true,
        'error': toast_type === 'error',
        'warning': toast_type === 'warning',
        'info': toast_type === 'info',
    }">
                    <i class="fa-solid fa-circle-check" v-if="toast_type === 'success'"></i>
                    <i class="fa-solid fa-circle-xmark" v-if="toast_type === 'error'"></i>
                    <i class="fa-solid fa-circle-exclamation" v-if="toast_type === 'warning'"></i>
                    <i class="fa-solid fa-circle-info" v-if="toast_type === 'info'"></i>
                </div>
                <div class="toast-notification__content__message">
                    <p>{{ message }}</p>
                </div>
                <!-- close -->
                <div class="toast-notification__content__close ms-3" @click="closeToast">
                    <i class="bi bi-x-lg"></i>
                </div>
            </div>
        </div>
        <div :class="{
        'toast-notification__cooldown': true, 'error': toast_type === 'error',
        'warning': toast_type === 'warning',
        'info': toast_type === 'info',
    }">
        </div>
    </li>
</template>

<script setup>
import { ref, onMounted, watch, defineProps, defineEmits } from 'vue';
import '@fortawesome/fontawesome-free/css/all.css';
import 'bootstrap-icons/font/bootstrap-icons.css';

const emit = defineEmits(['close']);

const toastRef = ref(null);

const props = defineProps({
    message: {
        type: String,
        default: 'Success! Your data has been saved.'
    },
    toast_type: {
        type: String,
        default: 'info'
    }
});


const closeToast = () => {
    emit('close');
};

</script>

<style scoped lang="scss">
.toast-notification {
    display: all;
    list-style: none;
    border-radius: 5px;
    background-color: #11101d;
    /* animation: appear 0.5s; */
    overflow: hidden;
    width: 300px;
    pointer-events: all;
}

.toast-notification.close {
    /* animation: disappear 0.5s; */
    display: none;
}

.toast-notification__container {
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    padding: 10px 20px;
    display: grid;

    .toast-notification__content__message {
        width: 100%;

        p {
            color: #fff;
            font-size: 14px;
            margin: 0;
            width: 200px;
            word-wrap: break-word;
        }
    }
}



.toast-notification__content__icon {
    color: #28a745;
    font-size: 20px;
}

.toast-notification__content__icon.error {
    color: #dc3545;
}

.toast-notification__content__icon.warning {
    color: #ffc107;
}

.toast-notification__content__icon.info {
    color: #17a2b8;
}

.toast-notification__content__message {
    margin-left: 10px;
    display: flex;
}

.toast-notification__content {
    display: flex;
    align-items: center;
}

.toast-notification__content__close {
    cursor: pointer;
    color: #fff;
    font-size: 20px;
}

.toast-notification__cooldown {
    height: 5px;
    background-color: #28a745;
    animation: cooldown 5s linear;
    width: 0;
}

.toast-notification__cooldown.error {
    background-color: #dc3545;
}

.toast-notification__cooldown.warning {
    background-color: #ffc107;
}

.toast-notification__cooldown.info {
    background-color: #17a2b8;
}

@keyframes cooldown {
    0% {
        width: 100%;
    }

    100% {
        width: 0;
    }
}

/* @keyframes appear {
    0% {
        transform: translateX(100%);
    }

    100% {
        transform: translateX(0);
    }
} */
</style>
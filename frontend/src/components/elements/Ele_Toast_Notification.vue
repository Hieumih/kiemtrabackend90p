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
                    <i class="bi bi-check-circle" v-if="toast_type === 'success'"></i>
                    <i class="bi bi-x-circle" v-if="toast_type === 'error'"></i>
                    <i class="bi bi-exclamation-circle" v-if="toast_type === 'warning'"></i>
                    <i class="bi bi-info-circle" v-if="toast_type === 'info'"></i>
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
    }"></div>
    </li>
</template>

<script setup>
import { ref, onMounted, watch, defineProps } from 'vue';
import 'bootstrap-icons/font/bootstrap-icons.css';

const toastRef = ref(null);

const props = defineProps({
    message: {
        type: String,
        default: 'Success! Your data has been saved.'
    },
    toast_type: {
        type: String,
        default: 'error'
    }
});

const closeToast = () => {
    toastRef.value.classList.remove('show');
};

onMounted(() => {
    toastRef.value.classList.add('show');
});

</script>

<style scoped>
.toast-notification {
    display: block;
    list-style: none;
    border-radius: 5px;
    background-color: #11101d;
    animation: appear 0.5s;
}

.toast-notification__container {
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    padding: 10px 20px;
    display: grid;
}

.toast-notification__container .toast-notification__content__message {
    width: 100%;
}

.toast-notification__container .toast-notification__content__message p {
    color: #fff;
    font-size: 14px;
    margin: 0;
    width: 200px;
    word-wrap: break-word;
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

@keyframes appear {
    0% {
        transform: translateX(100%);
    }

    100% {
        transform: translateX(0);
    }
}

</style>
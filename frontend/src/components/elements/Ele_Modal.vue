<template>
    <div>
        <div class="modal zoom-in" ref="modalRef" tabindex="-1"  aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5">
                            <slot name="header">Modal Header</slot>
                        </h1>
                        <button type="button" class="btn-close" aria-label="Close" @click="toggle"></button>
                    </div>

                    <div class="modal-body">
                        <slot name="body">
                            <p>Modal Body</p>
                        </slot>
                    </div>
                    <div class="modal-footer">
                        <slot name="footer" :close="toggle">

                            <button type="button" class="btn btn-secondary" @click="toggle">
                                Close
                            </button>

                        </slot>
                    </div>
                </div>
            </div>
        </div>

        <slot name="button-open" :click="toggle">
            <button class="modal-buton-show btn btn-primary me-1" @click="toggle">
                Open Modal
            </button>
        </slot>

    </div>
</template>

<script setup>
import { ref, defineExpose, onMounted, defineProps } from 'vue';
import { Modal } from 'bootstrap';

const modalRef = ref(null);
let modal = null;

const props = defineProps({
    modal_class: {
        type: String,
        required: false,
        default: ''
    }
});

const toggle = () => {
    modal.toggle();
}

onMounted(() => {
    modal = new Modal(modalRef.value);
    if (props.modal_class) {
        modalRef.value.classList.add(props.modal_class);
    }
});

defineExpose({
    toggle
})

</script>

<style scoped>
.modal.zoom-in {
    animation: zoom_out 0.3s;
    transform: scale(0);
}

.modal.zoom-in.show {
    animation: zoom_in 0.3s;
    transform: scale(1);
}

@keyframes zoom_in {
    from {
        transform: scale(0);
        opacity: 0;
    }

    to {
        transform: scale(1);
        opacity: 1;
    }
}

@keyframes zoom_out {
    from {
        transform: scale(1);
        display: block;
        opacity: 1;
    }

    to {
        transform: scale(0);
        display: none;
        opacity: 0;
    }

}
</style>
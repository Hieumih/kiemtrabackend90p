<template>
    <Ele_Modal ref="modalRef">
        <template #header>
            <slot name="header">Export Modal</slot>
        </template>

        <template #body>
            <form action="" ref="formRef">
                <template v-for="(column, index) in columns" :key="index">
                    <slot :name="`input-${column.value}`" :column
                         :disabled="(column?.disabled ?? false) || disabled" :data="data">
                        <div class="mb-3 row" v-if="!(column?.hidden ?? false)">
                            <label :for="column.value" class="col-sm-4 col-form-label">
                                {{ column.text }}
                            </label>
                            <div class="col-sm-8">
                                <input class="form-control" :type="column?.type ?? 'text'" :placeholder="column.text"
                                    :required="column?.required ?? false" :id="column.value"
                                    :disabled="(column?.disabled ?? false) || disabled" v-model="data[column.value]" />

                            </div>

                        </div>
                    </slot>
                </template>
                <slot name="addtion"></slot>
            </form>
        </template>

        <template #footer>
            <slot name="footer">
                <button type="button" class="btn btn-secondary" @click="close">
                    Close
                </button>
                <button type="submit" class="btn btn-primary" @click="save" :disabled="disabled">
                    Save changes
                </button>
            </slot>
        </template>

        <template #button-open="{ click }">
            <slot name="button-open" v-if="$slots['button-open']" :click>
        
            </slot>
        </template>
    </Ele_Modal>

</template>

<script setup>
import { ref, defineExpose, onMounted, defineProps, defineEmits, defineAsyncComponent } from 'vue';
const Ele_Modal = defineAsyncComponent(() => import('@/components/elements/Ele_Modal.vue'));


const formRef = ref(null);
const modalRef = ref(null);
const data = ref({});

const toggle = () => {
    modalRef.value.toggle();
};

const close = () => {
    modalRef.value.toggle();
    formRef.value.reset();
};



const save = () => {
    import.meta.env.DEV && console.log('save', data.value);
    emit('save', data.value, close);
};

const props = defineProps({
    columns: {
        type: Array,
        required: false,
        default: () => []
    },
    disabled: {
        type: Boolean,
        required: false,
        default: false
    }
});

const emit = defineEmits(['save']);

defineExpose({
    toggle,
    data
});


</script>
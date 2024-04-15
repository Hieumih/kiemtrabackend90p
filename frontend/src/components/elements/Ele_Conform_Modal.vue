<template>
    <div class="modal fade custom" ref="modalRef" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="modalLabel">{{ title }}</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <form action="" @submit.prevent="saveData">
                    <div class="modal-body">


                        <!-- <input class="input-text mb-3" v-for="(column, index) in columns" :key="index" type="text" :placeholder="column.title"> -->
                        <slot></slot>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" @click="clearForm"
                            data-bs-dismiss="modal">Close</button>
                        <button ref="sumbitBtnRef" type="submit" class="btn btn-primary" :disabled="formDisabled">Save
                            changes</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup>
import { Modal } from 'bootstrap';
import { ref, defineProps, defineEmits, onMounted } from 'vue';

const props = defineProps({
    columns: {
        type: Array,
        required: false,
        default: () => []
    },
    // formData: {
    //     type: Object,
    //     required: false,
    //     default: () => ({})
    // },
    title: {
        type: String,
        required: true,
    }
});

const emit = defineEmits(['save']);

const formDisabled = ref(false);

const saveFunction = ref( () => {} );

const sumbitBtnRef = ref(null);
const modalRef = ref(null);
let modal = null;

const formData = ref({});

const clearForm = () => {
    if (props.columns.length === 0) return;
    for (const key in formData.value) {
        formData.value[key] = "";
    }
};

const modalToggle = (save_function) => {
    console.log('Showing', props.title, 'modal');
    modal.toggle();
    saveFunction.value = save_function;
};

const saveData = async () => {
    sumbitBtnRef.value.disabled = true;
    await saveFunction.value();
    modal.toggle();
    sumbitBtnRef.value.disabled = false;
};

onMounted(() => {
    modal = new Modal(modalRef.value);
    //console.log(props.formData)
});

defineExpose(
    {
        modalToggle,
    }
)
</script>

<style scoped>

</style>
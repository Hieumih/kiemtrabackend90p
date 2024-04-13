<template>
    <div class="modal fade" id="modal" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="modalLabel">Thêm</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <form action="" @submit.prevent="saveItem">
                    <div class="modal-body">
                        <template v-for="(column, index) in columns" :key="index">
                            <div class="mb-3">
                                <template v-if="!(column?.hidden ?? false)">
                                    <!-- <label :for="column.key" class="form-label">
                                        {{ column.title }}
                                    </label> -->
                                    <input :type="column?.type ?? 'text'" :placeholder="column.text"
                                        class="form-control" :required="column?.required ?? false" :id="column.value"
                                        v-model="formData[column.value]">
                                </template>

                            </div>
                        </template>
                        <!-- <input class="input-text mb-3" v-for="(column, index) in columns" :key="index" type="text" :placeholder="column.title"> -->
                        <p id="addNotice"></p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" @click="clearForm"
                            data-bs-dismiss="modal">Close</button>
                        <button type="submit" class="btn btn-primary">Save changes</button>
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
        required: true,
    },
    formData: {
        type: Object,
        required: false,
        default: () => ({})
    },
});

const emit = defineEmits(['save']);

const modal = ref(null);

const clearForm = () => {
    for (const key in props.formData) {
        props.formData[key] = "";
    }
};

const modalToggle = () => {
    modal.value.toggle();
};

const saveItem = () => {
    emit('save', props.formData, () => {
        clearForm();
        modalToggle();
    });
};

onMounted(() => {
    modal.value = new Modal(document.getElementById('modal'));
    console.log(props.formData)
});

defineExpose(
    {
        modalToggle,
    }
)
</script>
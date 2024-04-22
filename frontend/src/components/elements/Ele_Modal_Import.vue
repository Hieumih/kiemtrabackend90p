<template>
    <Ele_Modal ref="modalRef" :modal_class="`modal-xl`">
        <template #header>
            <slot name="header">Modal Input Form</slot>
        </template>

        <template #body>
            <div :class="{ dragover: dragover, }" @drop.prevent="drop" @dragenter.prevent="toggleDragOver"
                @dragleave.prevent="toggleDragOver" @dragover.prevent>
                <div class="legecy-import">
                    <div class="mb-3">
                        <!-- <label for="file" class="form-label">File</label> -->
                        <input type="file" class="form-control" ref="fileRef" accept=".csv, .xlsx"
                            v-on:change="readFile" required>
                        <small>Accepted file types: CSV, XLSX</small>
                    </div>
                    <div class="mb-3">
                        <EasyDataTable :headers="columns" :items="data" :rows-items="[5, 10, 20, 50, 100]"
                            :rows-per-page="10" :loading="false" :server-items-length="0" border-cell buttons-pagination
                            show-index v-model:items-selected="selected_data" />
                    </div>
                    <slot name="addtion"></slot>
                </div>
                <div class="dropzone position-absolute top-50 start-50 translate-middle">
                    <span>Drop file here</span>


                </div>
            </div>

        </template>

        <template #footer>
            <slot name="footer" :close="close" :save="save" :save_disabled="disabled"
                :delete_disabled="selected_data.length === 0" :deleteSelected="deleteSelected">
                <button type="button" class="btn btn-danger" :disabled="selected_data.length === 0"
                    @click="deleteSelected">
                    Delete Selected
                </button>
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
import 'vue3-easy-data-table/dist/style.css';
import { ref, defineProps, defineEmits, onMounted, defineAsyncComponent, defineExpose, watch } from 'vue';
import Ele_Modal from '@/components/elements/Ele_Modal.vue';
import XLSX from 'xlsx'; // Excel parser
const EasyDataTable = defineAsyncComponent(() => import('vue3-easy-data-table'));

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

const emit = defineEmits(['save', 'fileError']);

const modalRef = ref(null);
const fileRef = ref(null);

const data = ref([]); // array of proxy objects
const selected_data = ref([]); // array of objects
const dragover = ref(false);

const toggle = () => {
    modalRef.value.toggle();
};

const close = () => {
    modalRef.value.toggle();
    fileRef.value.value = '';
    data.value = [];
};

const readFile = async () => {
    data.value = [];
    const file = fileRef.value.files[0];

    if (!file) {
        return;
    }

    const result = await file.arrayBuffer()
    const wb = XLSX.read(result, { type: 'array' });
    const ws = wb.Sheets[wb.SheetNames[0]];

    const header = XLSX.utils.sheet_to_json(ws, { header: 1 })[0]; // get the header

    if (!props.columns.every(column => header.map(col => col.toLowerCase()).includes(column.value))) {
        return fileError();
    }

    const jsonData = XLSX.utils.sheet_to_json(ws);
    // lowercase onject keys
    data.value = jsonData.map(row => {
        const record = {};
        Object.keys(row).forEach(key => {
            record[key.toLowerCase()] = row[key];
        });
        return record;
    });
};

const save = () => {
    import.meta.env.DEV && console.log('save', data.value);
    emit('save', data.value, close);
};

const deleteSelected = () => {
    console.log('deleteSelected', selected_data.value);
    data.value = data.value.filter(row => {
        return !selected_data.value.some(selected_row => {
            return Object.keys(selected_row).every(key => selected_row[key] === row[key]); // compare all keys
        });
    });
    selected_data.value = [];
};

const toggleDragOver = (e) => {
    e.preventDefault();
    dragover.value = !dragover.value;
};

const drop = (e) => {
    e.preventDefault();
    dragover.value = false;
    fileRef.value.files = e.dataTransfer.files;
    readFile();
};

const fileError = () => {
    emit('fileError');
};

watch(data, () => {
    import.meta.env.DEV && console.log('data', data.value);
    selected_data.value = [];
});

defineExpose({
    toggle,
    close,
    readFile,
    data,
});

</script>

<style scoped>
.dropzone {
    width: calc(100% - 20px);
    height: calc(100% - 20px);
    border: 2px dashed #ccc;
    border-radius: 5px;
    text-align: center;
    line-height: 100px;
    font-size: 20px;
    color: #ccc;
    opacity: 0;
    pointer-events: none;
}

.dragover .dropzone {
    background-color: #f0f0f0;
    opacity: 1;
    z-index: 5;
    pointer-events: auto;
}
</style>
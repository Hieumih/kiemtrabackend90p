<template>
    <div class="modal fade" ref="modalRef" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="modalLabel">{{ title }}</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <form ref="formRef" @submit.prevent="saveData">
                    <div class="modal-body">

                        <!-- file -->
                        <div class="mb-3">
                            <!-- <label for="file" class="form-label">File</label> -->
                            <input type="file" class="form-control" ref="fileRef" accept=".csv, .xlsx" v-on:change="readFile"
                                required>
                            <small>Accepted file types: CSV, XLSX</small>
                        </div>

                        <div class="mb-3">
                            <EasyDataTable :headers="columns" :items="data" :rows-items="[5, 10, 20, 50, 100]"
                                :rows-per-page="10" :loading="false" :server-items-length="0" border-cell
                                buttons-pagination show-index />
                        </div>

                        <!-- <input class="input-text mb-3" v-for="(column, index) in columns" :key="index" type="text" :placeholder="column.title"> -->
                        <slot></slot>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" @click="clearFile"
                            data-bs-dismiss="modal">Close</button>
                        <button ref="sumbitBtnRef" type="submit" class="btn btn-primary" disabled>Save changes</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>


<script setup>
import { Modal } from 'bootstrap';
import { ref, defineProps, defineEmits, onMounted, defineAsyncComponent } from 'vue';
import XLSX from 'xlsx'; // Excel parser
const EasyDataTable = defineAsyncComponent(() => import('vue3-easy-data-table'));

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

const emit = defineEmits(['save', 'fileError']);

const modalRef = ref(null);
const fileRef = ref(null);
const sumbitBtnRef = ref(null);

const data = ref([]);
let modal = null;

const modalToggle = () => {
    console.log('Showing', props.title, 'modal');
    modal.toggle();
};

const clearFile = () => {
    fileRef.value.value = '';
    data.value = [];
    sumbitBtnRef.value.disabled = true;
};

const readFile = async () => {
    sumbitBtnRef.value.disabled = true;
    data.value = [];
    const file = fileRef.value.files[0]; // get the file
    if (!file) return fileError(); // if no file return
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

    sumbitBtnRef.value.disabled = false;
};

const saveData = () => {
    sumbitBtnRef.value.disabled = true;
    emit('save', data.value, () => {
        clearFile();
        modalToggle();
    }, () => {
        sumbitBtnRef.value.disabled = false;
    });
};

const fileError = () => {
    console.error('Invalid file format');
    emit('fileError');
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
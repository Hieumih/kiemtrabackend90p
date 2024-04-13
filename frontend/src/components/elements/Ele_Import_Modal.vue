<template>
    <div class="modal fade" ref="modalRef" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
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
                            <input type="file" 
                                class="form-control" 
                                ref="fileRef" 
                                accept=".csv"
                                v-on:change="readFile" 
                                required>
                            <small>Accepted file types: CSV</small>
                        </div>

                        <div class="mb-3">
                            <EasyDataTable :headers="columns" :items="data" :rows-items="[5, 10, 20, 50, 100]" :rows-per-page="10"
                                :loading="false" :server-items-length="0" border-cell
                                buttons-pagination show-index/>
                        </div>
                        
                        <!-- <input class="input-text mb-3" v-for="(column, index) in columns" :key="index" type="text" :placeholder="column.title"> -->
                        <slot></slot>
                        
                    </div>
                    <div class="modal-footer">
                        <p ref="noticeRef" hidden></p>
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
import { ref, defineProps, defineEmits, onMounted } from 'vue';
import EasyDataTable from 'vue3-easy-data-table';
import Papa from 'papaparse'; // CSV parser

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

const modalRef = ref(null);
const fileRef = ref(null);
const noticeRef = ref(null);
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
    noticeRef.value.hidden = true;
};

const readFile = () => {
    sumbitBtnRef.value.disabled = true;
    const file = fileRef.value.files[0]; // get the file
    const reader = new FileReader(); // create a new FileReader
    reader.onload = (e) => { // once the file is read...
        let text = e.target.result; // get the file content
        const results = Papa.parse(text, { header: true, transformHeader: header => header.toLowerCase() }); // parse the CSV
        const header = results.meta.fields; // get the header
        // if props.column not in header return
        if (!props.columns.every(column => header.includes(column.value))) {
            console.error('Invalid file format');
            noticeRef.value.hidden = false;
            noticeRef.value.innerText = 'Invalid file format';
            return;
        }

        results.data.pop(); // remove the last row (empty row)
        // set the data
        data.value = results.data.map(row => { // map the rows
            const record = {};
            props.columns.forEach(column => { // for each column...
                record[column.value] = row[column.value]; // set the value
            });
            return record;
        });
        sumbitBtnRef.value.disabled = false;
    };
    reader.readAsText(file);

};

const saveData = () => {
    sumbitBtnRef.value.disabled = true;
    emit('save', data.value, () => {
        clearFile();
        modalToggle();
    }, () => {
        sumbitBtnRef.value.disabled = false;
        noticeRef.value.hidden = false;
        noticeRef.value.innerText = 'Connection error';
    });
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
<template>
    <Modal_Form :columns="columns" @save="save" ref="modalRef">
        <template #header>
            <slot name="header">Export</slot>
        </template>
        <template #input-fileType="{ column, data }">
            <div class="mb-3 row">
                <label :for="column.value" class="col-sm-4 col-form-label">
                    {{ column.text }}
                </label>
                <div class="col-sm-8">
                    <select class="form-select" :id="column.value" v-model="data[column.value]">
                        <option v-for="option in column.options" :value="option">{{ option }}</option>
                    </select>
                </div>
            </div>
        </template>
        <template #input-saveRange="{ column, data }">
            <div class="mb-3 row">
                <label :for="column.value" class="col-sm-4 col-form-label">
                    {{ column.text }}
                </label>
                <div class="col-sm-8">
                    <select class="form-select" :id="column.value" v-model="data[column.value]" value="current page">
                        <option v-for="option in column.options" :value="option">{{ option }}</option>
                    </select>
                </div>
            </div>
        </template>
        <template #button-open="{ click }">
            <slot name="button-open" v-if="$slots['button-open']" :click="click">
                <button @click="click" class="btn">Export</button>
            </slot>
        </template>
    </Modal_Form>
</template>

<script setup>
import { ref, watch, onMounted, defineEmits, defineProps, defineExpose } from "vue";
import { Modal_Form } from '@/components/elements/index.js';
import XLSX from 'xlsx'; // Excel parser
import { jsPDF } from 'jspdf'; // PDF parser
import "jspdf-autotable"; // PDF table parser

const modalRef = ref(null);

const props = defineProps({
    items: Array,
    default: () => []
})

const emits = defineEmits(['exportAll']);

const columns = [
    { text: 'File type', value: 'fileType', required: true, options: ['xlsx', 'csv', 'pdf'] },
    { text: 'File name', value: 'fileName', required: true },
    { text: 'Save Range', value: 'saveRange', options: ['all', 'current page'] }
]

const save = (setting) => {
    const { fileType, fileName, saveRange } = setting;
    if (saveRange === 'all') {
        alert('Not support yet')
    }
    else if (saveRange === 'current page') {
        if (fileType === 'xlsx' || fileType === 'csv') {
            exportExcel(props.items, setting)
        }
        else if (fileType === 'pdf') {
            exportPDF(props.items, setting)
        }
    }
}

const exportExcel = (data, setting) => {
    const { fileType, fileName, saveRange } = setting;
    const ws = XLSX.utils.json_to_sheet(data);
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    XLSX.writeFile(wb, `${fileName}.${fileType}`);
}

const exportPDF = (data, setting) => {
    const { fileType, fileName, saveRange } = setting;
    // new table
    const table = document.createElement('table');
    table.style.width = '100%';
    table.style.border = '1px solid black';
    // using json keys as headers
    const headers = Object.keys(data[0]);
    // create header row
    const header = document.createElement('tr');
    headers.forEach((headerText) => {
        const th = document.createElement('th');
        th.style.border = '1px solid black';
        th.appendChild(document.createTextNode(headerText));
        header.appendChild(th);
    });
    table.appendChild(header);
    // create data rows
    data.forEach((rowData) => {
        const row = document.createElement('tr');
        headers.forEach((headerText) => {
            const cell = document.createElement('td');
            cell.style.border = '1px solid black';
            cell.appendChild(document.createTextNode(rowData[headerText]));
            row.appendChild(cell);
        });
        table.appendChild(row);
    });
    // create pdf
    const doc = new jsPDF();
    doc.text(fileName, 10, 10);
    doc.autoTable({
        html: table,
        startY: 20
    });
    doc.save(`${fileName}.pdf`);  
}

onMounted(() => {
    // modalRef.value.data = {
    //     fileType: 'xlsx',
    //     fileName: 'export',
    //     saveRange: 'current page'
    // }
})

</script>
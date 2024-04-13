<template>
    <div class="modal fade" ref="modalRef" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable ">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="modalLabel">{{ title }}</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <form ref="formRef" @submit.prevent="saveData">
                    <div class="modal-body">
                        <div class="mb-3 row">
                            <label for="fileFormatSelect" class="col-sm-3 col-form-label">Export</label>
                            <div class="col-sm-7">
                                <select id="fileFormatSelect" ref="selectRef" name="" class="form-select" v-model="saveType">
                                <option value="csv">csv</option>
                                <option value="xlsx">xlsx</option>

                            </select>
                            </div>

                        </div>
                        <div class="mb-3 row" >
                            <label for="fileNameInput" class="col-sm-3 col-form-label">File name</label>
                            <div class="col-sm-7">
                                <input id="fileNameInput" type="text" class="form-control" v-model="fileName" required>
                            </div>
                            <label for="fileNameInput" class="col-sm-1 col-form-label">{{ `.${saveType}` }}</label>

                        </div>


                        <slot></slot>

                    </div>
                    <div class="modal-footer">
                        <p ref="noticeRef" hidden></p>
                        <button type="button" class="btn btn-secondary" @click="clearFile"
                            data-bs-dismiss="modal">Close</button>
                        <button ref="sumbitBtnRef" type="submit" class="btn btn-primary">Save changes</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>


<script setup>
import { Modal } from 'bootstrap';
import { ref, defineProps, defineEmits, onMounted } from 'vue';
import XLSX from 'xlsx'; // Excel parser

const props = defineProps({
    // columns: {
    //     type: Array,
    //     required: false,
    //     default: () => []
    // },
    title: {
        type: String,
        required: true,
    },
});



const modalRef = ref(null);
const noticeRef = ref(null);
const saveType = ref('csv');
const fileName = ref('exported_data');

const data = ref([]);
let modal = null;

const modalToggle = (input_data) => {
    console.log('Showing', props.title, 'modal');
    data.value = input_data;
    console.log(data.value);
    modal.toggle();
};

const saveData = () => {
    const name = fileName.value + '.' + saveType.value;
    const ws = XLSX.utils.json_to_sheet(data.value);
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, "Sheet1");
    XLSX.writeFile(wb, name);
};

onMounted(() => {
    modal = new Modal(modalRef.value);
    console.log(props.data);
});

defineExpose(
    {
        modalToggle,
        data
    }
)
</script>
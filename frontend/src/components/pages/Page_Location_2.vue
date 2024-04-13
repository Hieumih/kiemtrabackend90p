<template>
    <div class="cus-table">
        <DataTable :options="options" class="display" />
    </div>
</template>

<script>
import DataTable from 'datatables.net-vue3';
import DataTablesCore from 'datatables.net';
// import DataTable from 'datatables.net-bs5';
import 'datatables.net-bs5'
import 'datatables.net-buttons-bs5';
import 'datatables.net-fixedcolumns-bs5';
import 'datatables.net-responsive-bs5';
import 'datatables.net-scroller-bs5';

DataTable.use(DataTablesCore);

export default {
    name: 'Page_Location',
    components: {
        DataTable
    },
    methods: {
        edit(code) {
            console.log('Edit', code);
        },
        remove(code) {
            // Implement your remove logic here
        }
    },
    data() {
        return {
            options: {
                serverSide: true,
                ajax: {
                    url: 'http://localhost:5139/api/Locations',
                    type: 'GET',
                    dataSrc: 'data',
                    data: function (d) {
                        d.start = d.start;
                        d.length = d.length;
                        d.search = d.search.value;
                    },
                },
                columns: [
                    { title: 'Mã', data: 'code' },
                    { title: 'Tên Khu Vực', data: 'name' },
                    { title: 'Mô tả', data: 'description' },
                    {
                        title: 'Actions',
                        data: null,
                        render: function (data, type, row) {
                            return `
                            <button class="btn btn-primary edit-button" data-location='${JSON.stringify(data)}'>Edit</button>
                            <button class="btn btn-danger remove-button" data-location='${JSON.stringify(data)}'>Remove</button>
                        `;
                        }
                    },
                ],
                drawCallback: () => {
                    this.addEventListeners();
                },
            },
        }
    },
    methods: {
        addEventListeners() {
            const editButtons = document.querySelectorAll('.edit-button');
            editButtons.forEach(button => {
                button.addEventListener('click', (event) => {
                    this.edit(JSON.parse(event.target.dataset.location));
                });
            });

            const removeButtons = document.querySelectorAll('.remove-button');
            removeButtons.forEach(button => {
                button.addEventListener('click', (event) => {
                    this.remove(JSON.parse(event.target.dataset.location));
                });
            });
        },
        edit(data) {
            console.log('Edit', data);
        },
        remove(data) {
            console.log('Remove', data);
        }
    }

}
</script>

<style>
.cus-table .datatable div{
    margin-right:  0 ;
    margin-left:  0 ;
}

</style>
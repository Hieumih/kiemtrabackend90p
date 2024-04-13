<template>
    
    <div class="table-responsive">
        <table class="table table-hover mb-3 table-custom" id="table">
            <thead>
                <tr>
                    <th v-for="(column, index) in columns" :key="index">{{ column.title }}</th>
                    <slot name="additionHeader"></slot>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(item, index) in data" :key="index">
                    <template v-for="(column, colIndex) in columns" :key="colIndex">
                        <td>
                            {{ item[column.data] }}
                        </td>
                    </template>
                    <slot></slot>
                </tr>
            </tbody>
        </table>

    </div>

</template>

<script>
import $ from 'jquery';
import 'datatables.net';

export default {
    name: "Ele_Table",
    props: {
        columns: {
            type: Array,
            required: true
        },
        data: {
            type: Array,
            required: true
        }
    },
    watch: {
        data: {
            deep: true,
            handler(newData) {
                this.$nextTick(() => {
                    // Destroy DataTable instance
                    const dataTable = $(this.$el).find('table').DataTable();
                    if (dataTable) {
                        dataTable.destroy();
                    }
                    // Reinitialize DataTable with new data
                    $(this.$el).find('table').DataTable({
                        data: newData,
                    });
                });
            },
        },
    },
    mounted() {
        this.$nextTick(() => {
            $(this.$el).find('table').DataTable();
        });
    },
}
</script>
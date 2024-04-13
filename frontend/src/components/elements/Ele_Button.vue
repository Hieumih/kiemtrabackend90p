<template>
    <button class="btn btn-primary" @click="handleClick">
        <slot></slot>
    </button>
</template>


<script>
export default {
    emits: ['click'],
    methods: {
        handleClick() {
            this.$emit('click');
        }
    }
}
</script>
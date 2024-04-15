<template>
    <div>
        <h1>Router Loading...</h1>
    </div>
</template>

<script setup>
import { onMounted } from 'vue';

onMounted(() => {
    console.log('LoadingComponent mounted');
});

</script>
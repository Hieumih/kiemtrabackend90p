<template>
    <div class="main">
        <Loading_Spinner></Loading_Spinner>
        <p>Loading...</p>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import Loading_Spinner from '@/components/elements/Ele_Loading_Spinner.vue';

</script>

<style scoped>
.main {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: 100vh;
    width: 100vw;
}
</style>
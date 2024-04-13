<!-- ChildComponent.vue -->
<template>

  <p>
    <router-link to="/customer">Customer</router-link>
    /
    <router-link to="/manager">Manager</router-link>
  </p>
</template>
  
  <script>
  export default {
    methods: {
        closeSidebar() {
        console.log('Child method called from parent');
        // Do something here
      }
    }
  }
  </script>
  
import { defineAsyncComponent, defineComponent } from 'vue'
// import Page_Customer from "./Page_Customer.vue";
// import Page_Dashboard from "./Page_Dashboard.vue";
// import Page_Location from "./Page_Location.vue";
// import Page_Location_1 from "./Page_Location_1.vue";
// import Page_Location_2 from "./Page_Location_2.vue";
// import Page_Location_Detail from "./Page_Location_Detail.vue";


// export const Dashboard = defineAsyncComponent(() => import('./Page_Dashboard.vue'));
export const Dashboard = () => import('./Page_Dashboard.vue');
export const HangHoa = () => import('./Page_HangHoa.vue');
export const NhomHangHoa = () => import('./Page_NhomHangHoa.vue');

export default {
    Dashboard,
    HangHoa,
    NhomHangHoa,
}
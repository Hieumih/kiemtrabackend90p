import { defineAsyncComponent, defineComponent } from 'vue'
// import Page_Customer from "./Page_Customer.vue";
// import Page_Dashboard from "./Page_Dashboard.vue";
// import Page_Location from "./Page_Location.vue";
// import Page_Location_1 from "./Page_Location_1.vue";
// import Page_Location_2 from "./Page_Location_2.vue";
// import Page_Location_Detail from "./Page_Location_Detail.vue";


// export const Dashboard = defineAsyncComponent(() => import('./Page_Dashboard.vue'));
export const Dashboard = () => import('./Page_Dashboard.vue');
export const Customer = () => import('./Page_Customer.vue');
export const Location = () => import('./Page_Location.vue');
export const Schedule = () => import('./Page_Calendar.vue');
export const Location_Detail = defineAsyncComponent(() => import('./Page_Location_Detail.vue'));


export default {
    Dashboard,
    Customer,
    Location,
    Location_Detail,
    Schedule
}
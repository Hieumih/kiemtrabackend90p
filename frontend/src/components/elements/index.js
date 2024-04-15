// import Ele_HomeSection from "./Ele_HomeSection.vue";
// import Ele_LeftSideBar from "./Ele_LeftSideBar.vue";
// import Ele_SideBarDropDown from "./Ele_SideBarDropDown.vue";
// import Ele_Table from "./Ele_Table.vue";
// import Ele_Table_AddEditRemove from "./Ele_Table_AddEditRemove.vue";
// import Ele_Input_Modal from "./Ele_Input_Modal.vue";
// import Ele_Import_Modal from "./Ele_Import_Modal.vue";
// import Ele_Export_Modal from "./Ele_Export_Modal.vue";
import { defineAsyncComponent } from 'vue'


export const HomeSection = defineAsyncComponent(() => import('./Ele_HomeSection.vue'));
export const LeftSideBar = defineAsyncComponent(() => import('./Ele_LeftSideBar.vue'));
export const SideBarDropDown = defineAsyncComponent(() => import('./Ele_SideBarDropDown.vue'));
export const Table = defineAsyncComponent(() => import('./Ele_Table.vue'));
export const Table_AddEditRemove = defineAsyncComponent(() => import('./Ele_Table_AddEditRemove.vue'));
export const Input_Modal = defineAsyncComponent(() => import('./Ele_Input_Modal.vue'));
export const Import_Modal = defineAsyncComponent(() => import('./Ele_Import_Modal.vue'));
export const Export_Modal = defineAsyncComponent(() => import('./Ele_Export_Modal.vue'));
export const Toast_Notification = defineAsyncComponent(() => import('./Ele_Toast_Notification.vue'));
export const Toast_Container = defineAsyncComponent(() => import('./Ele_Toast_Container.vue'));
export const Conform_Modal = defineAsyncComponent(() => import('./Ele_Conform_Modal.vue'));

export default {
    HomeSection,
    LeftSideBar,
    SideBarDropDown,
    Table,
    Table_AddEditRemove,
    Input_Modal,
    Import_Modal,
    Export_Modal,
    Toast_Notification,
    Toast_Container,
    Conform_Modal
}
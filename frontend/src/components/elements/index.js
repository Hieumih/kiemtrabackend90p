import { defineAsyncComponent } from 'vue'
import Ele_Loading_Page from './Ele_Loading_Page.vue';

// ansyc components
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
export const Time_Date_Panel = defineAsyncComponent(() => import('./Ele_Time_Date_Panel.vue'));
export const Login_Panel = defineAsyncComponent(() => import('./Ele_Login_Panel.vue'));
export const Loading_Spinner = defineAsyncComponent(() => import('./Ele_Loading_Spinner.vue'));
// export const Loading_Page = defineAsyncComponent(() => import('./Ele_Loading_Page.vue'));
export const Loading_Page = Ele_Loading_Page;

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
    Conform_Modal,
    Time_Date_Panel,
    Login_Panel,
    Loading_Spinner,
    Loading_Page,
}
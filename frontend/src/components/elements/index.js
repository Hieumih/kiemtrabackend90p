import { defineAsyncComponent } from 'vue'
import Ele_Loading_Page from './Ele_Loading_Page.vue';

// ansyc components
export const HomeSection = defineAsyncComponent(() => import('./Ele_HomeSection.vue'));
export const LeftSideBar = defineAsyncComponent(() => import('./Ele_LeftSideBar.vue'));
export const SideBarDropDown = defineAsyncComponent(() => import('./Ele_SideBarDropDown.vue'));
export const Table = defineAsyncComponent(() => import('./Ele_Table.vue'));
export const Table_Manager = defineAsyncComponent(() => import('./Ele_Table_Manager.vue'));
export const Toast_Notification = defineAsyncComponent(() => import('./Ele_Toast_Notification.vue'));
export const Toast_Container = defineAsyncComponent(() => import('./Ele_Toast_Container.vue'));
export const Time_Date_Panel = defineAsyncComponent(() => import('./Ele_Time_Date_Panel.vue'));
export const Login_Panel = defineAsyncComponent(() => import('./Ele_Login_Panel.vue'));
export const Loading_Spinner = defineAsyncComponent(() => import('./Ele_Loading_Spinner.vue'));
export const Dashboard_Card = defineAsyncComponent(() => import('./Ele_Dashboard_Card.vue'));
export const Modal = defineAsyncComponent(() => import('./Ele_Modal.vue'));
export const Modal_Form = defineAsyncComponent(() => import('./Ele_Modal_Form.vue'));
export const Modal_Import = defineAsyncComponent(() => import('./Ele_Modal_Import.vue'));
export const Modal_Export = defineAsyncComponent(() => import('./Ele_Modal_Export.vue'));
// export const Loading_Page = defineAsyncComponent(() => import('./Ele_Loading_Page.vue'));
export const Loading_Page = Ele_Loading_Page;

export default {
    HomeSection,
    LeftSideBar,
    SideBarDropDown,
    Table,
    Table_Manager,
    Toast_Notification,
    Toast_Container,
    Time_Date_Panel,
    Login_Panel,
    Loading_Spinner,
    Loading_Page,
    Dashboard_Card,
    Modal,
    Modal_Form,
    Modal_Import,
    Modal_Export
}